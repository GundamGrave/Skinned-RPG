using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckUI : MonoBehaviour
{
    public PlayerHand player;
    public CardUI prefab;

    // Start is called before the first frame update
    void Start()
    {
        // create a cardUI as our child for every card in the players hand
        foreach (PlayerHand.PlayHand ph in player.usableCard)
        {
            // make a new UI object to show this card
            CardUI ui = Instantiate(prefab, transform);
            // set the new UI objects data to display this card
            ui.SetCompleteCard(ph.card);
        }
        
    }
}
