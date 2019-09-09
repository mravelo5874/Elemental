using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int PlayerMaxHealth;
    public int PlayerCurrHealth;
    public int PlayerCurrBlock;
    public int MaxEnergy;
    public int CurrEnergy;
    public int TotalCards;

    [Range(0.0f, 1.0f)]
    public float CritChance;
    public int CritMultiplier;

    public bool PlayerIsDead = false;

    public Transform TextPopupLocation;
    public Transform HealTextPopupLocation;
    private TextManager TextManager;

    public Animator BlockAnim;

    public void Start()
    {
        TextManager = GameObject.Find("TextPopupArea").GetComponent<TextManager>();
        PlayerCurrHealth = PlayerMaxHealth;
    }

    public void DamagePlayer(int num)
    {
        if (PlayerCurrBlock >= num)
        {
            PlayerCurrBlock -= num;
            BlockAnim.Play("HitBlockAnimation");
        }
        else
        {
            num -= PlayerCurrBlock;
            PlayerCurrBlock = 0;

            PlayerCurrHealth -= num;
            if (PlayerCurrHealth <= 0)
            {
                PlayerCurrHealth = 0;
            }
            GetComponentInParent<PlayerUI>().PlayerTakeDamage();
            TextManager.CreateDamageText(TextPopupLocation, num);
        }
    }

    public void DamagePlayerIgnoreBlock(int num)
    {
        PlayerCurrHealth -= num;
        if (PlayerCurrHealth <= 0)
        {
            PlayerCurrHealth = 0;
        }
        GetComponentInParent<PlayerUI>().PlayerTakeDamage();
        TextManager.CreateDamageText(TextPopupLocation, num);
    }

    public void HealPlayer(int num)
    {
        PlayerCurrHealth += num;

        if (PlayerCurrHealth >= PlayerMaxHealth)
        {
            PlayerCurrHealth = PlayerMaxHealth;
        }
        GetComponentInParent<PlayerUI>().PlayerHeal();
        TextManager.CreateHealText(TextPopupLocation, num);
    }

    public void HealToMax()
    {
        StartCoroutine(HealToMaxCoroutine());
    }

    IEnumerator HealToMaxCoroutine()
    {
        var healAmount = PlayerMaxHealth - PlayerCurrHealth;
        for (int i = 0; i < healAmount; i++)
        {
            PlayerCurrHealth++;

            if (PlayerCurrHealth >= PlayerMaxHealth)
            {
                PlayerCurrHealth = PlayerMaxHealth;
            }
            GetComponentInParent<PlayerUI>().PlayerHeal();
            TextManager.CreateHealText(HealTextPopupLocation, 1);
            yield return new WaitForSeconds(0.03f);
        }
    }

    public void KillPlayer()
    {
        while (PlayerCurrHealth != 0)
        {
            PlayerMaxHealth--;
        }
    }

    public void IncreaseMaxHealth(int num)
    {
        PlayerMaxHealth += num;
        PlayerCurrHealth += num;
    }

    public void DecreaseMaxHealth(int num)
    {
        PlayerMaxHealth -= num;
        if (PlayerCurrHealth >= PlayerMaxHealth)
        {
            PlayerCurrHealth = PlayerMaxHealth;
        }
    }

    public float HealthPercent()
    {
        return (float)PlayerCurrHealth / PlayerMaxHealth;
    }

    public void IncreaseBlock(int num)
    {
        PlayerCurrBlock += num;
        BlockAnim.Play("GainBlockAnimation");
    }

    public void IncreaseEnergy(int num)
    {
        CurrEnergy += num;
    }

    public void DecreaseEnergy(int num)
    {
        CurrEnergy -= num;
        if (CurrEnergy <= 0)
        {
            CurrEnergy = 0;
        }
    }

    public void IncreaseMaxEnergy(int num)
    {
        MaxEnergy += num;
        CurrEnergy += num;
    }

    public void DecreaseMaxEnergy(int num)
    {
        MaxEnergy -= num;
        CurrEnergy -= num;
    }

    public void ResetPlayerForNewTurn()
    {
        CurrEnergy = MaxEnergy;
        PlayerCurrBlock = 0;
    }

    public void ResetPlayerForNewCombat()
    {
        GetComponent<PlayerBuffManager>().ResetBuffs();

    }



    private void Update()
    {
        if (PlayerCurrHealth <= 0)
        {
            PlayerIsDead = true;
        }
    }
}
