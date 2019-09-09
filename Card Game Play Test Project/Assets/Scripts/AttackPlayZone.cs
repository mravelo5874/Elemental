using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackPlayZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public void OnDrop(PointerEventData eventData)
    {
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

        if (d != null)
        {
            Card card = d.GetComponent<PointerToVisualCard>().visualCard.GetComponent<CardDisplay>().card;
            if (card.CanPlayCard() && !card.isSkill)
            {
                if (GetComponentInParent<EnemyManager>() != null)
                {
                    // deal damage to enemy
                    if (card.DealDamageToEnemy)
                    {
                        GetComponentInParent<EnemyManager>().Damage(card.DamageToEnemyWithBuff());
                        card.ParasiteBuff();
                    }

                    // enemy vigor
                    if (card.ChangeEnemyVigor)
                    {
                        GetComponentInParent<EnemyBuffManager>().vigor += card.EnemyVigorChange;
                    }

                    // enemy defence
                    if (card.ChangeEnemyDefence)
                    {
                        GetComponentInParent<EnemyBuffManager>().defence += card.EnemyDefenceChange;
                    }

                    // enemy bramble
                    if (card.ChangeEnemyBramble)
                    {
                        GetComponentInParent<EnemyBuffManager>().bramble += card.EnemyBrambleChange;
                    }

                    // enemy parasite
                    if (card.ChangeEnemyParasite)
                    {
                        GetComponentInParent<EnemyBuffManager>().parasite += card.EnemyParasiteChange;
                    }


                }
                card.PlayCard_Player();




                d.GetComponent<DiscardCard>().Discard();
            }
            else
            {
                Debug.Log("You do not have enough energy to play this card.");
            }
        }
    }
}
