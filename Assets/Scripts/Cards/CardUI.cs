using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CardUI : MonoBehaviour
{
    public CompleteCard completeCard;

    public TextMeshProUGUI cardNameTag;
    public TextMeshProUGUI cardDescTag;
    public TextMeshProUGUI cardFlavTag;

    public TextMeshProUGUI strModTag;
    public TextMeshProUGUI intModTag;
    public TextMeshProUGUI spdModTag;

    public Image cardArt;
    public Image strArt;
    public Image intArt;
    public Image spdArt;


    // Start is called before the first frame update
    void Start()
    {
        SetCompleteCard(completeCard);
    }

    // Update is called once per frame
    public void SetCompleteCard(CompleteCard cc)
    {
        completeCard = cc;

        if (completeCard != null)
        {
            if (cardNameTag)
                cardNameTag.text = completeCard.cardName;
            if (cardFlavTag)
                cardFlavTag.text = completeCard.flavText;
            if (cardDescTag)
                cardDescTag.text = completeCard.GetDescription();
            if (cardArt)
                cardArt.sprite = completeCard.cardArt;
            if (strModTag)
                strModTag.text = completeCard.strMod.ToString();
            if (intModTag)
                intModTag.text = completeCard.intMod.ToString();
            if (spdModTag)
                spdModTag.text = completeCard.spdMod.ToString();
        }
        else
        {
            if (cardNameTag)
                cardNameTag.text = "";
            if (cardFlavTag)
                cardFlavTag.text = "";
            if (cardDescTag)
                cardDescTag.text = "";
            if (cardArt)
                cardArt.sprite = null;
            if (strModTag)
                strModTag.text = "";
            if (intModTag)
                intModTag.text = "";
            if (spdModTag)
                spdModTag.text = "";

        }
    }
}
