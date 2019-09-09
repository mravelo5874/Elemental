using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyUI : MonoBehaviour
{
    private EnemyManager enemyManager;

    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI BlockText;

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

    public CanvasGroup BlockImage;
    public CanvasGroup FrozenLayer;
    public CanvasGroup IntentLayer;

    public GameObject BlockIntent;
    public GameObject AttackIntent;

    public Animator BlockAnim;

    private void Start()
    {
        BlockAnim.SetBool("isBlock", false);
        BlockAnim.SetBool("isPlayer", false);
        enemyManager = this.GetComponentInParent<EnemyManager>();
        BlockImage.alpha = 0f;
        FrozenLayer.alpha = 0f;
        IntentLayer.alpha = 0.75f;
        PrevHealth = enemyManager.HealthPercent();
    }

    public void UpdateIntentAlphas()
    {
        if (enemyManager.AttackIntentAmount <= 0)
        {
            AttackIntent.GetComponent<CanvasGroup>().alpha = 0;
        }
        else
        {
            AttackIntent.GetComponentInChildren<TextMeshProUGUI>().text = enemyManager.AttackIntentAmount.ToString();
            AttackIntent.GetComponent<CanvasGroup>().alpha = 1;
        }

        if (enemyManager.BlockIntentAmount <= 0)
        {
            BlockIntent.GetComponent<CanvasGroup>().alpha = 0;
        }
        else
        {
            BlockIntent.GetComponentInChildren<TextMeshProUGUI>().text = enemyManager.BlockIntentAmount.ToString();
            BlockIntent.GetComponent<CanvasGroup>().alpha = 1;
        }
    }

    public void EnemyTakeDamage()
    {
        PrevHealth = HealBar.localScale.x;
        HealBar.localScale = new Vector3(enemyManager.HealthPercent(), 1f, 1f);
        isDamaged = true;
        DamageBarTimer = 0f;
        DamageBarLerpTimer = 0f;
    }

    public void EnemyHeal()
    {
        PrevHealth = HealBar.localScale.x;
        DamageBar.localScale = new Vector3(HealthBar.localScale.x, 1f, 1f);
        HealBar.localScale = new Vector3(enemyManager.HealthPercent(), 1f, 1f);
        isHeal = true;
        HealBarTimer = 0f;
        HealBarLerpTimer = 0f;
    }



    private void Update()
    {
        if (enemyManager.isFrozen)
        {
            FrozenLayer.alpha = 1f;
            IntentLayer.alpha = 0f;
        }
        else
        {
            FrozenLayer.alpha = 0f;
            IntentLayer.alpha = 0.75f;
        }

        UpdateIntentAlphas();

        if (enemyManager.EnemyCurrBlock > 0)
        {
            BlockAnim.SetBool("isBlock", true);
        }
        else
        {
            BlockAnim.SetBool("isBlock", false);
        }

        HealthText.text = enemyManager.EnemyCurrHealth.ToString() + "/" + enemyManager.EnemyMaxHealth.ToString();
        BlockText.text = enemyManager.EnemyCurrBlock.ToString();

        if (!isHeal)
        {
            HealthBar.localScale = new Vector3(enemyManager.HealthPercent(), 1f, 1f);
        }
        else
        {
            HealBarTimer += Time.deltaTime;
            if (HealBarTimer >= HealBarWaitTime)
            {
                HealBarLerpTimer += Time.deltaTime;
                if (HealBarLerpTimer <= HealBarSpeed)
                {
                    HealthBar.localScale = new Vector3(Mathf.Lerp(PrevHealth, enemyManager.HealthPercent(), HealBarLerpTimer / HealBarSpeed), 1f, 1f);
                    PrevHealth = HealthBar.localScale.x;
                }
                else
                {
                    PrevHealth = enemyManager.HealthPercent();
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
                    DamageBar.localScale = new Vector3(Mathf.Lerp(PrevHealth, enemyManager.HealthPercent(), DamageBarLerpTimer / DamageBarSpeed), 1f, 1f);
                    PrevHealth = DamageBar.localScale.x;
                }
                else
                {
                    PrevHealth = enemyManager.HealthPercent();
                    DamageBar.localScale = new Vector3(PrevHealth, 1f, 1f);
                    DamageBarTimer = 0f;
                    DamageBarLerpTimer = 0f;
                    isDamaged = false;
                }
            }
        }
    }
}
