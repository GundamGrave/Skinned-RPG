using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] public List<PlayHand> usableCard = new List<PlayHand>();
    public int MaxCards;
    PlayerStats ps;
    PlayerRaycast pr;
    PlayerDeck pd;

    public bool drawCards = false;

    private void Start()
    {
        ps = GetComponent<PlayerStats>();
        pr = GetComponent<PlayerRaycast>();
        pd = GetComponent<PlayerDeck>();
    }

    void Update()
    {
        //  press keys to select/deselect cards
        for (int i = 0; i < usableCard.Count; i++)
        {
            if (Input.GetKeyDown(usableCard[i].GetKey()))
            {
                if(ps.SelectedSkill == usableCard[i].GetCard().Spell)
                {
                    // cancel the card on a second click
                    ps.SelectedSkill = null;
                    pr.targeting = false;
                    pr.radiusMode = false;
                }
                else
                {
                    ps.SelectedSkill = usableCard[i].GetCard().Spell;
                    pr.targeting = true;
                    if(usableCard[i].GetCard().Spell.Radius == 0)
                    {
                        pr.radiusMode = false;
                    }
                    else
                    {
                        pr.radiusMode = true;
                    }
                }
                
                                             
            }
                
        }

        if (ps.myTurn)
        {
            if (drawCards)
            {
                if (usableCard.Count < MaxCards)
                {
                    int NumOfNewCards = MaxCards - usableCard.Count;
                    int[] NewCards = new int[NumOfNewCards];
                    for (int i = 0; i < NewCards.Length; i++)
                    {
                        NewCards[i] = Random.Range(0, pd.Deck.Count - 1);
                    }

                    foreach (int i in NewCards)
                    {
                        PlayHand hand = new PlayHand();
                        hand.ChangeCard(pd.Deck[i]);
                        hand.ChangeKey((KeyCode)System.Enum.Parse(typeof(KeyCode), "Alpha" + (usableCard.Count + 1)));
                        usableCard.Add(hand);
                    }
                }
                drawCards = false;
            }
        }
        else
        {
            drawCards = true;
        }

        // clean up excess cards in case of bugs
        if(usableCard.Count > MaxCards)
        {
            for(int i = usableCard.Count - 1; i > MaxCards; i--)
            {
                usableCard.RemoveAt(i);
            }
        }
    }

    public void RemoveCard(PlayHand ph)
    {
        usableCard.Remove(ph);
        int counter = 0;
        for(int i = 0; i < usableCard.Count; i++)
        {
            usableCard[i].ChangeKey((KeyCode)System.Enum.Parse(typeof(KeyCode), "Alpha" + i));
            counter++;
        }
    }
}

[System.Serializable]
public class PlayHand
{
    [SerializeField] private CompleteCard cc;
    [SerializeField] private KeyCode key;

    public PlayHand()
    {
        cc = null;
    }

    public PlayHand(CompleteCard card, KeyCode code)
    {
        cc = card;
        key = code;
    }

    public CompleteCard GetCard()
    {
        return cc;
    }

    public KeyCode GetKey()
    {
        return key;
    }

    public void ChangeKey(KeyCode code)
    {
        key = code;
    }

    public void ChangeCard(CompleteCard card)
    {
        cc = card;
    }
}
