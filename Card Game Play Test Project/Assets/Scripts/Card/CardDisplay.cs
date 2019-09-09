using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI typeText;
    public Image artwork;

    public Image background;
    public Image costBackground;

    public Sprite water_cost_image;
    public Sprite fire_cost_image;
    public Sprite earth_cost_image;
    public Sprite air_cost_image;

    private Color mainColor;
    private Color secondaryColor;
    private Color tertiaryColor;

    private void Start()
    {
        nameText.text = card.name;
        descriptionText.text = card.description;
        costText.text = card.cost.ToString();
        typeText.text = card.type.ToString();
        artwork.sprite = card.artwork;

        if (card.type == GameManager.Type.WATER)
        {
            //Debug.Log("WATER_COLOR_PALETTE");
            mainColor = new Color(153.0f / 255.0f, 248.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
            secondaryColor = new Color(105.0f / 255.0f, 111.0f / 255.0f, 212.0f / 255.0f, 255.0f / 255.0f);
            tertiaryColor = new Color(96.0f / 255.0f, 229.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
            costBackground.sprite = water_cost_image;
        }
        else if (card.type == GameManager.Type.FIRE)
        {
            //Debug.Log("FIRE_COLOR_PALETTE");
            mainColor = new Color(250.0f / 255.0f, 173.0f / 255.0f, 55.0f / 255.0f, 255.0f / 255.0f);
            secondaryColor = new Color(245.0f / 255.0f, 83.0f / 255.0f, 73.0f / 255.0f, 255.0f / 255.0f);
            tertiaryColor = new Color(255.0f / 255.0f, 135.0f / 255.0f, 0.0f / 255.0f, 255.0f / 255.0f);
            costBackground.sprite = fire_cost_image;
        }
        else if (card.type == GameManager.Type.EARTH)
        {
            //Debug.Log("EARTH_COLOR_PALETTE");
            mainColor = new Color(154.0f / 255.0f, 112.0f / 255.0f, 49.0f / 255.0f, 255.0f / 255.0f);
            secondaryColor = new Color(147.0f / 255.0f, 88.0f / 255.0f, 20.0f / 255.0f, 255.0f / 255.0f);
            tertiaryColor = new Color(132.0f / 255.0f, 243.0f / 255.0f, 123.0f / 255.0f, 255.0f / 255.0f);
            costBackground.sprite = earth_cost_image;
        }
        else if (card.type == GameManager.Type.AIR)
        {
            //Debug.Log("AIR_COLOR_PALETTE");
            mainColor = new Color(203.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
            secondaryColor = new Color(127.0f / 255.0f, 203.0f / 255.0f, 212.0f / 255.0f, 255.0f / 255.0f);
            tertiaryColor = new Color(136.0f / 255.0f, 251.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
            costBackground.sprite = air_cost_image;
        }

        background.color = mainColor;
    }
}
