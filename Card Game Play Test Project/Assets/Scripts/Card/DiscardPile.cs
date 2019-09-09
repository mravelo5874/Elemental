using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiscardPile : MonoBehaviour {

    public List<GameObject> Discard;
    public TextMeshProUGUI CountText;
    public GameObject CardNodePrefab;

    private void Awake()
    {
        Discard = new List<GameObject>();
        UpdateCountText();
    }

    private void UpdateCountText()
    {
        CountText.text = Discard.Count.ToString();
    }

    public void AddCard(Card card)
    {
        GameObject cardNode = Instantiate(CardNodePrefab, this.transform);
        cardNode.GetComponent<CardNode>().card = card;
        Discard.Add(cardNode);
        UpdateCountText();
    }

    public void AddCardNode(GameObject node)
    {
        node.transform.SetParent(this.transform);
        Discard.Add(node);
    }

    public GameObject RemoveCard()
    {
        if (Discard.Count > 0)
        {
            // find, remove, and return the last element in the list
            GameObject cardNode = Discard[Discard.Count - 1];
            Discard.RemoveAt(Discard.Count - 1);
            UpdateCountText();
            return cardNode;
        }
        else
        {
            return null;
        }
    }

    public bool isEmpty()
    {
        if (Discard.Count > 0)
            return false;
        else return true;
    }
}
