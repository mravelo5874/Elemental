using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameTestManager : MonoBehaviour
{
    public bool includeSpecialCards = true;
    public int DefaultCardDraw = 5;

    public TestStartingDeck StartingDeck;
    public GameCardTracker cardTracker;

    public CardDeck Deck;
    public DiscardPile Discard;
    public GameObject Hand;
    public Image Background;
    public bool isDiscarding = false;


    public EnemyPlayArea EnemyArea;
    public PlayerManager playerManager;
    public GameObject EnemyPrefab;
    public EnergyManagerUI energyManager;


    public Animator FadeObject;
    public GameObject EndTurnButton;
    public bool isEnemyTurn = false;

    private void Start()
    {
        StartGame();
        FadeObject.SetBool("isBlackScreen", false);
        ChangeBackgroundColor();
    }

    public void ChangeBackgroundColor()
    {
        GameManager.Type DeckType = StartingDeck.gameManager.DeckType;

        if (DeckType == GameManager.Type.WATER)
        {
            Background.color = new Color(38f / 255f, 191f / 255f, 215f / 255f);
        }
        else if (DeckType == GameManager.Type.FIRE)
        {
            Background.color = new Color(222f / 255f, 70f / 255f, 70f / 255f);
        }
        else if (DeckType == GameManager.Type.EARTH)
        {
            Background.color = new Color(104f / 255f, 81f / 255f, 39f / 255f);
        }
        else if (DeckType == GameManager.Type.AIR)
        {
            Background.color = new Color(150f / 255f, 150f / 255f, 150f / 255f);
        }
    }

    public void StartGame()
    {
        //Add cards to deck
        for (int i = 0; i < 5; i++)
        {
            Deck.AddCard(StartingDeck.AttackCard());
        }
        for (int i = 0; i < 5; i++)
        {
            Deck.AddCard(StartingDeck.BlockCard());
        }
        if (includeSpecialCards)
        {
            Deck.AddCard(StartingDeck.SpecialCard1());
            Deck.AddCard(StartingDeck.SpecialCard2());
        }
        // Shuffle Deck
        Deck.ShuffleDeck();

        StartCoroutine(DrawCardsForNewTurn());

        // Ready player for game
        playerManager.ResetPlayerForNewTurn();
    }

    public void SpawnEnemy()
    {
        EnemyArea.AddEnemy(EnemyPrefab);
    }

    public Card DrawCardToHand()
    {
        GameObject cardNode = Deck.DrawCard();

        if (cardNode == null)
        {
            if (Discard.isEmpty())
            {
                Debug.Log("No cards in Game.");
            }
            else
            {
                while (!Discard.isEmpty())
                {
                    GameObject node = Discard.RemoveCard();
                    Deck.AddCardNode(node);
                }

                Deck.ShuffleDeck();
                cardNode = Deck.DrawCard();
            }
        }

        if (cardNode != null)
        {
            if (Hand.transform.childCount >= 10)
            {
                Debug.Log("Hand is full.");
                Discard.AddCardNode(cardNode);
            }
            else
            {
                Hand.GetComponent<Hand>().AddCardToHand(cardNode.GetComponent<CardNode>().card);
                Destroy(cardNode);
            }

            return cardNode.GetComponent<CardNode>().card;
        }

        return null;
    }

    public void EndTurn()
    {
        // Disable end turn button
        UpdateEndTurnButton();

        // add extra energy to charge
        if (playerManager.CurrEnergy > 0)
        {
            energyManager.AddCharge(playerManager.CurrEnergy);
        }

        // Discard all cards in hand
        StartCoroutine(DiscardAllCards());

        StartCoroutine(EnemyTurnDelay(1.0f));
    }

    private IEnumerator EnemyTurnDelay(float secs)
    {
        yield return new WaitForSeconds(0.5f);


        // Enemy turn(s)
        for (int i = 0; i < EnemyArea.enemyList.Count; i++)
        {
            EnemyArea.enemyList[i].GetComponent<EnemyManager>().EnemyTurn();
            yield return new WaitForSeconds(secs);
        } 

        // Update enemy intents for new turn
        for (int i = 0; i < EnemyArea.enemyList.Count; i++)
        {
            EnemyArea.enemyList[i].GetComponent<EnemyManager>().UpdateEnemyIntents();
            yield return new WaitForSeconds(0.2f);
        }
        // reset turn button
        UpdateEndTurnButton();
        // draw cards for new turn
        StartCoroutine(DrawCardsForNewTurn());
        // reset card tracker stats
        cardTracker.ResetForNewTurn();
        // Reset player for new turn
        playerManager.ResetPlayerForNewTurn();
    }

    private void UpdateEndTurnButton()
    {
        if (!isEnemyTurn)
        {
            isEnemyTurn = true;
            EndTurnButton.GetComponentInChildren<TextMeshProUGUI>().text = "Enemy Turn";
            EndTurnButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            isEnemyTurn = false;
            EndTurnButton.GetComponentInChildren<TextMeshProUGUI>().text = "End Turn";
            EndTurnButton.GetComponent<Button>().interactable = true;
        }
    }

    private IEnumerator DrawCardsForNewTurn()
    {
        while (isDiscarding)
        {
            yield return null;
        }

        for (int i = 0; i < this.DefaultCardDraw; i++)
        {
            DrawCardToHand();
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator DiscardAllCards()
    {
        isDiscarding = true;
        int num = Hand.transform.childCount;

        for (int i = 0; i < num; i++)
        {
            Hand.transform.GetChild(num - 1 - i).GetComponent<DiscardCard>().Discard();
            yield return new WaitForSeconds(0.1f);
        }

        isDiscarding = false;
    }
}
