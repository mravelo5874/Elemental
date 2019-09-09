using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsUI : MonoBehaviour
{
    public TextMeshProUGUI CardsPlayedText;
    public TextMeshProUGUI AttackDealtText;
    public TextMeshProUGUI BlockGainedText;
    public TextMeshProUGUI EnemiesSlainText;
    public TextMeshProUGUI AmountHealedText;

    public int CardsPlayed = 0;
    public int AttackDealt = 0;
    public int BlockGained = 0;
    public int EnemiesSlain = 0;
    public int AmountHealed = 0;

    private void Update()
    {
        CardsPlayedText.text = "Cards Played: " + CardsPlayed.ToString();
        AttackDealtText.text = "Attack Dealt: " + AttackDealt.ToString();
        BlockGainedText.text = "Block Gained: " + BlockGained.ToString();
        EnemiesSlainText.text = "Enemies Slain: " + EnemiesSlain.ToString();
        AmountHealedText.text = "Amount Healed: " + AmountHealed.ToString();
    }
}
