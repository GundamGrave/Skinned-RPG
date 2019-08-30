using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckUI : MonoBehaviour
{
    public PlayerHand player;
    public PlayerStats ps;
    public CardUI prefab;
    public int NumberOfShownCards;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerHand>();
        ps = player.gameObject.GetComponent<PlayerStats>();
        // create a cardUI as our child for every card in the players hand
        ShowCards();
    }

    private void Update()
    {
        if (ps.myTurn)
        {
            ShowCards();
        }
    }

    private void ShowCards()
    {
        int counter = -1;
        foreach (PlayerHand.PlayHand ph in player.usableCard)
        {
            counter++;
            if (counter >= NumberOfShownCards)
            {
                // make a new UI object to show this card
                CardUI ui = Instantiate(prefab, transform);
                // set the new UI objects data to display this card
                ui.SetCompleteCard(ph.card);
                ui.index = counter;
                ui.dUI = this;
                NumberOfShownCards++;
            }
        }
    }
}
