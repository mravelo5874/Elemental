using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class SkillPlayArea : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

        if (d != null)
        {
            Card card = d.GetComponent<PointerToVisualCard>().visualCard.GetComponent<CardDisplay>().card;
            if (card.CanPlayCard())
            {
                if (card.isSkill)
                {
                    card.PlayCard_Player();




                    d.GetComponent<DiscardCard>().Discard();
                }
                else
                {
                    Debug.Log("This is not a skill card.");
                }

            }
            else
            {
                Debug.Log("You do not have enough energy to play this card.");
            }
        }
    }
}
