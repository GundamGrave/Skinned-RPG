using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    public List<CompleteCard> Deck;
    public int maxCards;

    public void AddCard(CompleteCard cc)
    {
        if(Deck.Count < maxCards)
        {
            Deck.Add(cc);
        }
    }
}
