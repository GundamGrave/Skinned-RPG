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

    public PlayHand[] usableCard;
    PlayerStats ps;
    PlayerRaycast pr;

    private void Start()
    {
        ps = GetComponent<PlayerStats>();
        pr = GetComponent<PlayerRaycast>();
    }

    void Update()
    {
        for (int i = 0; i < usableCard.Length; i++)
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

    }
}
