using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardDeck : MonoBehaviour {

    public List<GameObject> Deck;
    public TextMeshProUGUI CountText;
    public GameObject CardNodePrefab;


    private void Awake()
    {
        Deck = new List<GameObject>();
        UpdateCountText();
    }

    private void UpdateCountText()
    {
        CountText.text = Deck.Count.ToString();
    }

    public void AddCard(Card card)
    {
        GameObject NewNode = Instantiate(CardNodePrefab, this.transform);
        NewNode.GetComponent<CardNode>().card = card;
        Deck.Add(NewNode);

        UpdateCountText();
    }

    public void AddCardNode(GameObject node)
    {
        node.transform.SetParent(this.transform);
        Deck.Add(node);
    }

    public void ShuffleDeck()
    {
       for (int i = 0; i < Deck.Count; i++)
       {
            GameObject temp = Deck[i];
            int randNum = Random.Range(i, Deck.Count);
            Deck[i] = Deck[randNum];
            Deck[randNum] = temp;
       }
    }

    public GameObject DrawCard()
    {
        if (Deck.Count > 0)
        { 
            // find, remove, and return the last element in the list
            GameObject card = Deck[Deck.Count - 1];
            Deck.RemoveAt(Deck.Count - 1);
            UpdateCountText();
            return card;
        }
        else
        {
            return null;
        }
    }
}
