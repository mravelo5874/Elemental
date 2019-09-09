using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCardTracker : MonoBehaviour
{
    public Card prevCardPlayed;

    public int WaterCardsPlayedTotal;
    public int WaterCardsPlayedThisTurn;

    public int FireCardsPlayedTotal;
    public int FireCardsPlayedThisTurn;

    public int EarthCardsPlayedTotal;
    public int EarthCardsPlayedThisTurn;

    public int AirCardsPlayedTotal;
    public int AirCardsPlayedThisTurn;

    public void ResetForNewTurn()
    {
        WaterCardsPlayedThisTurn = 0;
        FireCardsPlayedThisTurn = 0;
        EarthCardsPlayedThisTurn = 0;
        AirCardsPlayedThisTurn = 0;
    }

    public void ResetForNewCombat()
    {
        ResetForNewTurn();
        WaterCardsPlayedTotal = 0;
        FireCardsPlayedTotal = 0;
        EarthCardsPlayedTotal = 0;
        AirCardsPlayedTotal = 0;
    }

    public int NumCardsPlayedThisTurn(string type)
    {
        if (type == "WATER")
        {
            return WaterCardsPlayedThisTurn;
        }
        else if (type == "FIRE")
        {
            return FireCardsPlayedThisTurn;
        }
        else if (type == "EARTH")
        {
            return EarthCardsPlayedThisTurn;
        }
        else if (type == "AIR")
        {
            return AirCardsPlayedThisTurn;
        }

        return 0;
    }

    public int NumCardsPlayedThisCombat(string type)
    {
        if (type == "WATER")
        {
            return WaterCardsPlayedTotal;
        }
        else if (type == "FIRE")
        {
            return FireCardsPlayedTotal;
        }
        else if (type == "EARTH")
        {
            return EarthCardsPlayedTotal;
        }
        else if (type == "AIR")
        {
            return AirCardsPlayedTotal;
        }

        return 0;
    }
}
