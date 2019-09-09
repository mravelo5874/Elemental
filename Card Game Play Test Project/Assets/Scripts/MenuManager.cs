using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameManager gameManager;
    public Animator FadeObject;

    private void Start()
    {
        FadeObject.SetBool("isBlackScreen", false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void StartGame()
    {
        FadeObject.SetBool("isBlackScreen", true);
        StartCoroutine(WaitAndExit(1.5f));
    }

    private IEnumerator WaitAndExit(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("CardField");
    }

    public void WaterButton()
    {
        gameManager.DeckType = GameManager.Type.WATER;
        StartGame();
    }

    public void FireButton()
    {
        gameManager.DeckType = GameManager.Type.FIRE;
        StartGame();
    }

    public void EarthButton()
    {
        gameManager.DeckType = GameManager.Type.EARTH;
        StartGame();
    }

    public void AirButton()
    {
        gameManager.DeckType = GameManager.Type.AIR;
        StartGame();
    }
}
