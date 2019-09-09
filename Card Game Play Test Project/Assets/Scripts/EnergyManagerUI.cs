using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnergyManagerUI : MonoBehaviour
{
    public Transform PrimaryBar;
    public Transform SecondaryBar;
    public TextMeshProUGUI ChargeText;

    public int CurrEnergyCharge;
    public int MaxEnergyCharge;

    public float BarWaitTime;
    public float BarSpeed;
    private float BarTimer = 0f;
    private float BarLerpTimer = 0f;
    private bool isIncrease = false;
    private float PrevCharge;

    private void Start()
    {
        CurrEnergyCharge = 0;
        PrevCharge = ChargePercent();
    }

    public float ChargePercent()
    {
        return (float)CurrEnergyCharge / MaxEnergyCharge;
    }

    public void AddCharge(int num)
    {
        BarTimer = 0f;
        BarLerpTimer = 0f;
        PrevCharge = ChargePercent();
        CurrEnergyCharge += num;
        if (CurrEnergyCharge >= MaxEnergyCharge)
        {
            CurrEnergyCharge = MaxEnergyCharge;
        }
        SecondaryBar.localScale = new Vector3(ChargePercent(), 1f, 1f);
        isIncrease = true;
    }

    private void Update()
    {
        ChargeText.text = CurrEnergyCharge.ToString() + "/" + MaxEnergyCharge.ToString();

        if (isIncrease)
        {
            BarTimer += Time.deltaTime;
            if (BarTimer >= BarWaitTime)
            {
                BarLerpTimer += Time.deltaTime;
                if (BarLerpTimer <= BarSpeed)
                {
                    PrimaryBar.localScale = new Vector3(Mathf.Lerp(PrevCharge, ChargePercent(), BarLerpTimer / BarSpeed), 1f, 1f);
                    PrevCharge = PrimaryBar.localScale.x;
                }
                else
                {
                    PrevCharge = ChargePercent();
                    PrimaryBar.localScale = new Vector3(PrevCharge, 1f, 1f);
                    SecondaryBar.localScale = new Vector3(PrevCharge, 1f, 1f);
                    BarTimer = 0f;
                    BarLerpTimer = 0f;
                    isIncrease = false;
                }
            }
        }
    }
}
