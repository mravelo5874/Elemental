using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardCard : MonoBehaviour
{
    public void Discard()
    {
        var card = GetComponent<PointerToVisualCard>().visualCard.GetComponent<CardDisplay>().card;
        GameObject.Find("Discard Pile").GetComponent<DiscardPile>().AddCard(card);
        Destroy(this.gameObject);
    }
}
