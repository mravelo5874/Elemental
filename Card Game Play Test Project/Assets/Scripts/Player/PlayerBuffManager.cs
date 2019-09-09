using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBuffManager : MonoBehaviour
{
    public int vigor;
    public int defence;
    public int bramble;
    public int parasite;

    public int ResetVigor;
    public int ResetDefence;
    public int ResetBramble;
    public int ResetParasite;

    public GameObject vigorIcon;
    public GameObject defenceIcon;
    public GameObject brambleIcon;
    public GameObject parasiteIcon;

    private void Start()
    {
        vigor = 0;
        defence = 0;
        bramble = 0;
        parasite = 0;

        ResetVigor = 0;
        ResetDefence = 0;
        ResetBramble = 0;
        ResetParasite = 0;

        vigorIcon.SetActive(false);
        defenceIcon.SetActive(false);
        brambleIcon.SetActive(false);
        parasiteIcon.SetActive(false);
    }

    public void ResetBuffs()
    {
        vigor = ResetVigor;
        defence = ResetDefence;
        bramble = ResetBramble;
        parasite = ResetParasite;
    }

    private void Update()
    {
        // vigor
        if(vigor != 0)
        {
            vigorIcon.GetComponentInChildren<TextMeshProUGUI>().text = vigor.ToString();
            vigorIcon.SetActive(true);
        }
        else
        {
            vigorIcon.SetActive(false);
        }


        // defence
        if (defence != 0)
        {
            defenceIcon.GetComponentInChildren<TextMeshProUGUI>().text = defence.ToString();
            defenceIcon.SetActive(true);
        }
        else
        {
            defenceIcon.SetActive(false);
        }

        // bramble
        if (bramble != 0)
        {
            brambleIcon.GetComponentInChildren<TextMeshProUGUI>().text = bramble.ToString();
            brambleIcon.SetActive(true);
        }
        else
        {
            brambleIcon.SetActive(false);
        }

        // parasite
        if (parasite != 0)
        {
            parasiteIcon.GetComponentInChildren<TextMeshProUGUI>().text = parasite.ToString();
            parasiteIcon.SetActive(true);
        }
        else
        {
            parasiteIcon.SetActive(false);
        }
    }
}
