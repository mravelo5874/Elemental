using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    public GameObject DamageTextPrefab;
    public GameObject HealTextPrefab;
    public GameObject CritTextPrefab;

    public void CreateDamageText(Transform location, int amount)
    {
        var TextObject = Instantiate(DamageTextPrefab, this.transform);
        TextObject.transform.position = new Vector3(location.position.x + Random.Range(-10f, 10f), location.position.y + Random.Range(-10f, 10f), 0f);
        TextObject.GetComponentInChildren<TextMeshProUGUI>().text = amount.ToString();

        var DamageAnim = TextObject.GetComponentInChildren<Animator>();
        var clipInfo = DamageAnim.GetCurrentAnimatorClipInfo(0);
        Destroy(TextObject, clipInfo[0].clip.length - 0.1f);
        DamageAnim.Play("DamageTextAnimation");
    }

    public void CreateHealText(Transform location, int amount)
    {
        var TextObject = Instantiate(HealTextPrefab, this.transform);
        TextObject.transform.position = new Vector3(location.position.x + Random.Range(-10f, 10f), location.position.y + Random.Range(-10f, 10f), 0f);
        TextObject.GetComponentInChildren<TextMeshProUGUI>().text = amount.ToString();

        var HealAnim = TextObject.GetComponentInChildren<Animator>();
        var clipInfo = HealAnim.GetCurrentAnimatorClipInfo(0);
        Destroy(TextObject, clipInfo[0].clip.length - 0.1f);
        HealAnim.Play("HealTextAnimation");
    }

    public void CreateCritText(Transform location)
    {
        var TextObject = Instantiate(CritTextPrefab, this.transform);
        TextObject.transform.position = new Vector3(location.position.x + Random.Range(-10f, 10f), location.position.y + Random.Range(-10f, 10f), 0f);

        var DamageAnim = TextObject.GetComponentInChildren<Animator>();
        var clipInfo = DamageAnim.GetCurrentAnimatorClipInfo(0);
        Destroy(TextObject, clipInfo[0].clip.length);
        DamageAnim.Play("CritTextAnimation");
    }
}
