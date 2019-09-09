using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int EnemyMaxHealth;
    public int EnemyCurrHealth;
    public int EnemyCurrBlock;

    public int AttackIntentAmount;
    public int BlockIntentAmount;

    public bool isFrozen = false;
    public int FreezeTurnCount = 0;

    private bool isDead = false;

    public Animator anim;
    public Animator BlockAnim;
    public Animator IntenAnim;
    public AnimationClip deathAnimation;

    public Transform TextPopupLocation;
    public Transform HealTextPopupLocation;
    private TextManager TextManager;
    private PlayerManager playerManager;
    private EnemyBuffManager BuffManager;
    private EnemyUI UIManager;

    private int vigor;
    private int defence;
    private int bramble;
    private int parasite;

    public void Start()
    {
        UIManager = this.GetComponent<EnemyUI>();
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
        TextManager = GameObject.Find("TextPopupArea").GetComponent<TextManager>();
        BuffManager = this.GetComponent<EnemyBuffManager>();
        vigor = BuffManager.vigor;
        defence = BuffManager.defence;

        EnemyCurrHealth = EnemyMaxHealth;
        ChooseIntent();
    }

    public void ChooseIntent()
    {
        var rand = Random.Range(0.0f, 1.0f);
        if (rand < 0.4f)
        {
            BlockIntentAmount = Random.Range(6, 18) + this.defence;
            AttackIntentAmount = 0;

            IntenAnim.Play("NewBlockIntent");
        }
        else if (rand < 0.8f)
        {
            BlockIntentAmount = 0;
            AttackIntentAmount = Random.Range(8, 12) + this.vigor;

            IntenAnim.Play("NewAttackIntent");
        }
        else
        {
            BlockIntentAmount = Random.Range(4, 8) + this.defence;
            AttackIntentAmount = Random.Range(4, 8) + this.vigor;

            IntenAnim.Play("NewBlockIntent");
            IntenAnim.Play("NewAttackIntent");
        }
    }

    public void Damage(int num)
    {
        if (DetermineCrit())
        {
            num = num * playerManager.CritMultiplier;
            TextManager.CreateCritText(this.transform);
        }

        // enemy bramble damage
        if (BuffManager.bramble > 0)
        {
            playerManager.DamagePlayer(BuffManager.bramble);
        }

        if (EnemyCurrBlock >= num)
        {
            EnemyCurrBlock -= num;
            BlockAnim.Play("HitBlockAnimation");
        }
        else
        {
            num -= EnemyCurrBlock;
            EnemyCurrBlock = 0;
            EnemyCurrHealth -= num;

            anim.Play("HitAnimation");
            GameObject.Find("GameStatsUI").GetComponent<StatsUI>().AttackDealt += num;

            if (EnemyCurrHealth <= 0)
            {
                EnemyCurrHealth = 0;
            }
            GetComponentInParent<EnemyUI>().EnemyTakeDamage();
            TextManager.CreateDamageText(TextPopupLocation, num);
        }
    }

    public void DamageIgnoreBlock(int num)
    {
        if (DetermineCrit())
        {
            num = num * playerManager.CritMultiplier;
            TextManager.CreateCritText(this.transform);
        }

        EnemyCurrHealth -= num;

        anim.Play("HitAnimation");
        GameObject.Find("GameStatsUI").GetComponent<StatsUI>().AttackDealt += num;

        if (EnemyCurrHealth <= 0)
        {
            EnemyCurrHealth = 0;
        }
        GetComponentInParent<EnemyUI>().EnemyTakeDamage();
        TextManager.CreateDamageText(TextPopupLocation, num);
    }

    public float HealthPercent()
    {
        return (float)EnemyCurrHealth / EnemyMaxHealth;
    }

    public void Heal(int num)
    {
        EnemyCurrHealth += num;

        if (EnemyCurrHealth >= EnemyMaxHealth)
        {
            EnemyCurrHealth = EnemyMaxHealth;
        }
        GetComponentInParent<EnemyUI>().EnemyHeal();
        TextManager.CreateHealText(HealTextPopupLocation, num);
    }

    public void IncreaseBlock(int num)
    {
        EnemyCurrBlock += num;
        BlockAnim.Play("GainBlockAnimation");
    }

    private IEnumerator AttackPlayer()
    {
        IntenAnim.Play("AttackingAnimation");
        yield return new WaitForSeconds(0.5f);
        playerManager.DamagePlayer(AttackIntentAmount);

        // reduce attack intent amount
        AttackIntentAmount = 0;

        // enemy parasite heal
        if (BuffManager.parasite > 0)
        {
            Heal(BuffManager.parasite);
        }

        // player bramble damage
        var bramble = playerManager.GetComponentInParent<PlayerBuffManager>().bramble;
        if (bramble > 0)
        {
            this.Damage(bramble);
        }
        UIManager.UpdateIntentAlphas();
    }

    private IEnumerator GainBlock()
    {
        IntenAnim.Play("BlockingAnimation");
        yield return new WaitForSeconds(0.5f);
        IncreaseBlock(BlockIntentAmount);

        // reduce block intent amount
        BlockIntentAmount = 0;
        UIManager.UpdateIntentAlphas();
    }

    private IEnumerator EnemyTurnDelays()
    {
        EnemyCurrBlock = 0;
        int EnemyActions = 0;

        if (!isFrozen)
        {
            if (BlockIntentAmount > 0)
            {
                EnemyActions++;
                StartCoroutine(GainBlock());
                yield return new WaitForSeconds(0.5f);
            }

            if (AttackIntentAmount > 0)
            {
                EnemyActions++;
                StartCoroutine(AttackPlayer());
                yield return new WaitForSeconds(0.5f);
            }
        }
        else
        {
            FreezeTurnCount--;
            if (FreezeTurnCount <= 0)
            {
                anim.SetBool("isFrozen", false);
                isFrozen = false;
            }
        }
    }

    public void EnemyTurn()
    {
        StartCoroutine(EnemyTurnDelays());
    }

    private bool DetermineCrit()
    {
        var num = Random.Range(0f, 1f);
        //Debug.Log(num);

        if (num < playerManager.CritChance)
        {
            return true;
        }
        return false;
    }

    public void UpdateEnemyIntents()
    {
        ChooseIntent();
        UIManager.UpdateIntentAlphas();
    }
   

    private void Update()
    {
        if (EnemyCurrHealth == 0 && !isDead)
        {
            anim.Play("DeathAnimation");
            GameObject.Find("GameStatsUI").GetComponent<StatsUI>().EnemiesSlain++;
            GameObject.Find("EnemyPlayArea").GetComponent<EnemyPlayArea>().DestroyEnemy(this.gameObject, deathAnimation.length);
            isDead = true;
        }

        if (BuffManager.vigor != this.vigor)
        {
            if (BuffManager.vigor < this.vigor)
            {
                var num = this.vigor - BuffManager.vigor;
                this.AttackIntentAmount -= num;
            }
            else if (BuffManager.vigor > this.vigor)
            {
                var num = BuffManager.vigor - this.vigor;
                this.AttackIntentAmount += num;
            }

            this.vigor = BuffManager.vigor;
        }

        if (BuffManager.defence != this.defence)
        {
            if (BuffManager.defence < this.defence)
            {
                var num = this.defence - BuffManager.defence;
                this.BlockIntentAmount -= num;
            }
            else if (BuffManager.defence > this.defence)
            {
                var num = BuffManager.defence - this.defence;
                this.BlockIntentAmount += num;
            }

            this.defence = BuffManager.defence;
        }
    }

    public void FreezeEnemy(int turns)
    {
        anim.SetBool("isFrozen", true);
        isFrozen = true;
        FreezeTurnCount = turns;
    }
}
