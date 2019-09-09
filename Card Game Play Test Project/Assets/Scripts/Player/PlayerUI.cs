using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    private PlayerManager playerManager;

    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI BlockText;
    public TextMeshProUGUI EnergyText;
    public TextMeshProUGUI CritText;

    public Transform HealthBar;
    public Transform DamageBar;
    public float DamageBarWaitTime;
    public float DamageBarSpeed;
    private float DamageBarTimer = 0f;
    private float DamageBarLerpTimer = 0f;
    private float PrevHealth;
    private bool isDamaged = false;

    public Transform HealBar;
    public float HealBarWaitTime;
    public float HealBarSpeed;
    private float HealBarTimer = 0f;
    private float HealBarLerpTimer = 0f;
    private bool isHeal = false;

    public Animator BlockAnim;

    private void Start()
    {
        BlockAnim.SetBool("isBlock", false);
        BlockAnim.SetBool("isPlayer", true);
        playerManager = this.GetComponentInParent<PlayerManager>();
        PrevHealth = playerManager.HealthPercent();
    }

    public void PlayerTakeDamage()
    {
        PrevHealth = HealBar.localScale.x;
        HealBar.localScale = new Vector3(playerManager.HealthPercent(), 1f, 1f);
        isDamaged = true;
        DamageBarTimer = 0f;
        DamageBarLerpTimer = 0f;
    }

    public void PlayerHeal()
    {
        PrevHealth = HealBar.localScale.x;
        DamageBar.localScale = new Vector3(HealthBar.localScale.x, 1f, 1f);
        HealBar.localScale = new Vector3(playerManager.HealthPercent(), 1f, 1f);
        isHeal = true;
        HealBarTimer = 0f;
        HealBarLerpTimer = 0f;
    }

    private void Update()
    {
        if (playerManager.PlayerCurrBlock > 0)
        {
            BlockAnim.SetBool("isBlock", true);
        }
        else
        {
            BlockAnim.SetBool("isBlock", false);
        }

        HealthText.text = playerManager.PlayerCurrHealth.ToString() + "/" + playerManager.PlayerMaxHealth.ToString();
        BlockText.text = playerManager.PlayerCurrBlock.ToString();
        EnergyText.text = playerManager.CurrEnergy.ToString() + "/" + playerManager.MaxEnergy.ToString();

        CritText.text = "Crit Chance: " + (playerManager.CritChance * 100).ToString() + "%";

        if (!isHeal)
        {
            HealthBar.localScale = new Vector3(playerManager.HealthPercent(), 1f, 1f);
        }    
        else
        {
            HealBarTimer += Time.deltaTime;
            if (HealBarTimer >= HealBarWaitTime)
            {
                HealBarLerpTimer += Time.deltaTime;
                if (HealBarLerpTimer <= HealBarSpeed)
                {
                    HealthBar.localScale = new Vector3(Mathf.Lerp(PrevHealth, playerManager.HealthPercent(), HealBarLerpTimer / HealBarSpeed), 1f, 1f);
                    PrevHealth = HealthBar.localScale.x;
                }
                else
                {
                    PrevHealth = playerManager.HealthPercent();
                    HealthBar.localScale = new Vector3(PrevHealth, 1f, 1f);
                    DamageBar.localScale = new Vector3(PrevHealth, 1f, 1f);
                    HealBarTimer = 0f;
                    HealBarLerpTimer = 0f;
                    isHeal = false;    
                }
            }
        }

        if (isDamaged)
        {
            DamageBarTimer += Time.deltaTime;
            if (DamageBarTimer >= DamageBarWaitTime)
            {
                DamageBarLerpTimer += Time.deltaTime;
                if (DamageBarLerpTimer <= DamageBarSpeed)
                {
                    DamageBar.localScale = new Vector3(Mathf.Lerp(PrevHealth, playerManager.HealthPercent(), DamageBarLerpTimer / DamageBarSpeed), 1f, 1f);
                    PrevHealth = DamageBar.localScale.x;
                }
                else
                {
                    PrevHealth = playerManager.HealthPercent();
                    DamageBar.localScale = new Vector3(PrevHealth, 1f, 1f);
                    DamageBarTimer = 0f;
                    DamageBarLerpTimer = 0f;
                    isDamaged = false;   
                }
            }
        }
    }
}
