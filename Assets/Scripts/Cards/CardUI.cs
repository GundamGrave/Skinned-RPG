using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CardUI : MonoBehaviour
{
    PlayerHand ph;
    PlayerStats ps;
    PlayerRaycast pr;
    public PlayHand play;

    public DeckUI dUI;
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

    public int index;


    // Start is called before the first frame update
    void Start()
    {
        SetCompleteCard(completeCard);
        ph = FindObjectOfType<PlayerHand>();
        ps = ph.gameObject.GetComponent<PlayerStats>();
        pr = ps.gameObject.GetComponent<PlayerRaycast>();
    }

    private void Update()
    {

        if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), "Alpha" + (index + 1) )))
        {
            if (ps.SelectedSkill == completeCard.Spell)
            {
                // cancel the card on a second click
                ps.SelectedSkill = null;
                pr.targeting = false;
                pr.radiusMode = false;
            }
            else
            {
                ps.SelectedSkill = completeCard.Spell;
                pr.targeting = true;
                if (completeCard.Spell.Radius == 0)
                {
                    pr.radiusMode = false;
                }
                else
                {
                    pr.radiusMode = true;
                }
            }

            pr.cUI = this;
            pr.dUI = dUI;
            pr.index = index;
        }

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

    public void Press()
    {
        if (ps.GetStat(UnitInformation.Stats.ActionPoints) >= completeCard.Spell.Cost)
        {

            ps.SelectedSkill = completeCard.Spell;
            pr.targeting = true;
            if (completeCard.Spell.Radius == 0)
            {
                pr.radiusMode = false;
            }
            else
            {
                pr.radiusMode = true;
            }
            pr.cUI = this;
            pr.dUI = dUI;
            pr.index = index;

            /*
            ps.ModifyStat(UnitInformation.Stats.ActionPoints, -completeCard.Spell.Cost);
            dUI.RemoveCard(this);
            ph.RemoveCard(index);
            Destroy(gameObject);
            */
        }
    }
}
