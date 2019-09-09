using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards")]
public class Card : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite artwork;
    public int cost;
    public GameManager.Type type;

    private StatsUI gameStats;
    private GameCardTracker cardTracker;
    private EnemyPlayArea EnemyArea;
    private PlayerBuffManager BuffManager;
    private PlayerManager player;
    private GameTestManager gameManager;

    public bool isSkill;
    public bool randomTarget;
    public bool allEnemies;
    public enum Duration { single_turn, multiple_turns, entire_combat, permanent };
    public enum Dependency { none, previous_card_water, previous_card_fire, previous_card_earth, previous_card_air, card_drawn_cost };

    // player:

    public bool GainBlock;
    public int blockAmount;
    public Dependency blockDependence;

    public bool DrawCards;
    public int cardAmount;
    public Dependency drawDependence;

    public bool HurtPlayer;
    public int hurtPlayerAmount;
    public Dependency hurtPlayerDependence;

    public bool HealPlayer;
    public int healPlayerAmount;
    public bool healPlayerToMax;
    public Dependency healDependence;

    public bool ChangePlayerVigor;
    public int VigorChange;
    public Duration playerVigorDuration;
    public Dependency vigorDependence;

    public bool ChangePlayerDefence;
    public int DefenceChange;
    public Duration playerDefenceDuration;
    public Dependency defenceDependence;

    public bool ChangePlayerBramble;
    public int BrambleChange;
    public Duration playerBrambleDuration;
    public Dependency brambleDependence;

    public bool ChangePlayerParasite;
    public int ParasiteChange;
    public Duration playerParasiteDuration;
    public Dependency parasiteDependence;

    // enemy:

    public bool DealDamageToEnemy;
    public int damageAmountToEnemy;
    public Dependency damageDependence;

    public bool ChangeEnemyVigor;
    public int EnemyVigorChange;
    public Duration enemyVigorDuration;

    public bool ChangeEnemyDefence;
    public int EnemyDefenceChange;
    public Duration enemyDefenceDuration;

    public bool ChangeEnemyBramble;
    public int EnemyBrambleChange;
    public Duration enemyBrambleDuration;

    public bool ChangeEnemyParasite;
    public int EnemyParasiteChange;
    public Duration enemyParasiteDuration;

    public bool freezeTarget;
    public int freezeTurns;


    private void FindObjects()
    {
        player = GameObject.Find("Player").GetComponent<PlayerManager>();
        BuffManager = player.GetComponentInParent<PlayerBuffManager>();
        EnemyArea = GameObject.Find("EnemyPlayArea").GetComponent<EnemyPlayArea>();
        cardTracker = GameObject.Find("GameTestManager").GetComponent<GameCardTracker>();
        gameStats = GameObject.Find("GameStatsUI").GetComponent<StatsUI>();
        gameManager = GameObject.Find("GameTestManager").GetComponent<GameTestManager>();
    }

    public bool CanPlayCard()
    {
        FindObjects();
        return player.CurrEnergy >= cost;
    }

    public void ParasiteBuff()
    {
        FindObjects();
        if (BuffManager.parasite > 0)
        {
            player.HealPlayer(BuffManager.parasite);
        }
    }

    public int DamageToEnemyWithBuff()
    {
        FindObjects();
        return damageAmountToEnemy + BuffManager.vigor;
    }

    public void PlayCard_Player()
    {
        FindObjects();

        player.DecreaseEnergy(cost);
        gameStats.CardsPlayed++;

        
        
        // Block
        if (GainBlock)
        {
            var block = blockAmount + BuffManager.defence;
            gameStats.BlockGained += block;
            player.IncreaseBlock(block);

        }

        // Hurt Player
        if (HurtPlayer)
        {
            player.DamagePlayer(hurtPlayerAmount);
        }

        // Draw Cards
        int totalCostofCardsDrawn = 0;
        if (DrawCards)
        {
            for (int i = 0; i < cardAmount; i++)
            {
                var card = gameManager.DrawCardToHand();
                totalCostofCardsDrawn += card.cost;
            }
        }

        // Heal Player
        if (HealPlayer)
        {
            if (healDependence == Dependency.card_drawn_cost)
            {
                player.HealPlayer(healPlayerAmount * totalCostofCardsDrawn);
                gameStats.AmountHealed += healPlayerAmount * totalCostofCardsDrawn;
            }
            else
            {
                gameStats.AmountHealed += healPlayerAmount;
                player.HealPlayer(healPlayerAmount);
            }
        }

        // Heal Player to Max
        if (healPlayerToMax)
        {
            player.HealToMax();
        }

        // player vigor
        if (ChangePlayerVigor)
        {
            if (playerVigorDuration == Duration.entire_combat)
            {
                BuffManager.vigor += VigorChange;
            }
        }

        // player defence
        if (ChangePlayerDefence)
        {
            if (playerDefenceDuration == Duration.entire_combat)
            {
                BuffManager.defence += DefenceChange;
            }
        }

        // player bramble
        if (ChangePlayerBramble)
        {
            if (playerBrambleDuration == Duration.entire_combat)
            {
                BuffManager.bramble += BrambleChange;
            }
        }

        // player parasite
        if (ChangePlayerParasite)
        {
            if (playerParasiteDuration == Duration.entire_combat)
            {
                BuffManager.parasite += ParasiteChange;
            }
        }

        // affects all enemies
        if (allEnemies)
        {
            EnemyArea.ChangeAllEnemyBuffs(EnemyVigorChange, EnemyDefenceChange, EnemyBrambleChange, EnemyParasiteChange);
        }


        // Freeze target
        if (freezeTarget)
        {
            if (EnemyArea.enemyList.Count > 0)
            {
                if (randomTarget)
                {
                    EnemyArea.FreezeRandomEnemy(freezeTurns);
                }
            }
            else
            {
                Debug.Log("No enemies found.");
            }  
        }







        // update card tracker
        if (type == GameManager.Type.WATER)
        {
            cardTracker.WaterCardsPlayedThisTurn++;
        }
        else if (type == GameManager.Type.FIRE)
        {
            cardTracker.FireCardsPlayedThisTurn++;
        }
        else if (type == GameManager.Type.EARTH)
        {
            cardTracker.EarthCardsPlayedThisTurn++;
        }
        else if (type == GameManager.Type.AIR)
        {
            cardTracker.AirCardsPlayedThisTurn++;
        }

        cardTracker.prevCardPlayed = this;
    }
}
