using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckUI : MonoBehaviour
{
    public PlayerHand player;
    public PlayerStats ps;
    public CardUI prefab;
    public int NumberOfShownCards;

    public List<CardUI> Cards = new List<CardUI>();

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
        foreach (PlayHand ph in player.usableCard)
        {
            counter++;
            if (counter >= NumberOfShownCards)
            {
                // make a new UI object to show this card
                CardUI ui = Instantiate(prefab, transform);
                Cards.Add(ui);
                // set the new UI objects data to display this card
                ui.SetCompleteCard(ph.GetCard());
                ui.index = counter;
                ui.dUI = this;
                NumberOfShownCards++;
            }
        }
    }

    public void RemoveCard(CardUI cardUI)
    {
        Cards.Remove(cardUI);
        NumberOfShownCards--;
        int index = 0;
        foreach(CardUI cUI in Cards)
        {
            cUI.index = index;
            index++;
        }
    }
}
