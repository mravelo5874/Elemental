using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStartingDeck : MonoBehaviour
{
    public GameManager gameManager;
    public GameManager.Type DeckType;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        this.DeckType = gameManager.DeckType;
    }

    public Card WATER_AttackCard;
    public Card WATER_BlockCard;
    public Card WATER_SpecialCard1;
    public Card WATER_SpecialCard2;

    public Card FIRE_AttackCard;
    public Card FIRE_BlockCard;
    public Card FIRE_SpecialCard1;
    public Card FIRE_SpecialCard2;

    public Card EARTH_AttackCard;
    public Card EARTH_BlockCard;
    public Card EARTH_SpecialCard1;
    public Card EARTH_SpecialCard2;

    public Card AIR_AttackCard;
    public Card AIR_BlockCard;
    public Card AIR_SpecialCard1;
    public Card AIR_SpecialCard2;

    public Card AttackCard()
    {
        if (DeckType == GameManager.Type.WATER)
            return WATER_AttackCard;
        else if (DeckType == GameManager.Type.FIRE)
            return FIRE_AttackCard;
        else if (DeckType == GameManager.Type.EARTH)
            return EARTH_AttackCard;
        else return AIR_AttackCard;
    }

    public Card BlockCard()
    {
        if (DeckType == GameManager.Type.WATER)
            return WATER_BlockCard;
        else if (DeckType == GameManager.Type.FIRE)
            return FIRE_BlockCard;
        else if (DeckType == GameManager.Type.EARTH)
            return EARTH_BlockCard;
        else return AIR_BlockCard;
    }

    public Card SpecialCard1()
    {
        if (DeckType == GameManager.Type.WATER)
            return WATER_SpecialCard1;
        else if (DeckType == GameManager.Type.FIRE)
            return FIRE_SpecialCard1;
        else if (DeckType == GameManager.Type.EARTH)
            return EARTH_SpecialCard1;
        else return AIR_SpecialCard1;
    }

    public Card SpecialCard2()
    {
        if (DeckType == GameManager.Type.WATER)
            return WATER_SpecialCard2;
        else if (DeckType == GameManager.Type.FIRE)
            return FIRE_SpecialCard2;
        else if (DeckType == GameManager.Type.EARTH)
            return EARTH_SpecialCard2;
        else return AIR_SpecialCard2;
    }
}
