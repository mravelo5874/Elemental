using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour
{
    public Animator FadeObject;

    public void MenuScene()
    {
        FadeObject.SetBool("isBlackScreen", true);
        StartCoroutine(WaitAndExit(1.5f));
    }

    private IEnumerator WaitAndExit(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Menu");
    }
}
