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

    void Update()
    {
        for (int i = 0; i < usableCard.Length; i++)
        {
            if (Input.GetKeyDown(usableCard[i].key))
            {
            }
                
        }

    }
}
