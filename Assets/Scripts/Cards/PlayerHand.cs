using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{

    [System.Serializable]
    public struct PlayHand
    {
        public CompleteCard card;
        public KeyCode key;
    }

    public List<PlayHand> usableCard;
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
        for (int i = 0; i < usableCard.Count; i++)
        {
            if (Input.GetKeyDown(usableCard[i].key))
            {
                if(ps.SelectedSkill == usableCard[i].card.Spell)
                {
                    ps.SelectedSkill = null;
                    pr.targeting = false;
                    pr.radiusMode = false;
                }
                else
                {
                    ps.SelectedSkill = usableCard[i].card.Spell;
                    pr.targeting = true;
                    if(usableCard[i].card.Spell.Radius == 0)
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
                        hand.card = pd.Deck[i];
                        hand.key = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Alpha" + (usableCard.Count + 1));
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

        if(usableCard.Count > MaxCards)
        {
            for(int i = usableCard.Count - 1; i > MaxCards; i--)
            {
                usableCard.RemoveAt(i);
            }
        }
    }
}
