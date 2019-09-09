using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

    public GameObject DummyCardPrefab;
    public GameObject VisualCardPrefab;

    public GameObject VisualCardHand;
    public int NumOfCards;

	public void AddCardToHand(Card card)
    {
        var VisualCard = Instantiate(VisualCardPrefab, VisualCardHand.transform);
        var DummyCard = Instantiate(DummyCardPrefab, this.transform);

        VisualCard.GetComponent<CardDisplay>().card = card;
        VisualCard.GetComponent<LerpCardMovement>().SetDummy(DummyCard);
        DummyCard.GetComponent<HoverOverCard>().SetCanvas(VisualCard.GetComponent<Canvas>());
        DummyCard.GetComponent<PointerToVisualCard>().visualCard = VisualCard;
    }

    private void Update()
    {
        NumOfCards = this.transform.childCount;
    }
}
