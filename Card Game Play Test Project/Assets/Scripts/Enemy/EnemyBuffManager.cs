using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyBuffManager : MonoBehaviour
{
    public int vigor = 0;
    public int defence = 0;
    public int bramble = 0;
    public int parasite = 0;

    public GameObject vigorIcon;
    public GameObject defenceIcon;
    public GameObject brambleIcon;
    public GameObject parasiteIcon;

    private void Start()
    {
        vigorIcon.SetActive(false);
        defenceIcon.SetActive(false);
        brambleIcon.SetActive(false);
        parasiteIcon.SetActive(false);
    }

    private void Update()
    {
        // vigor
        if (vigor != 0)
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
