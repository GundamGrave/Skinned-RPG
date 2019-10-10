using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DragAndDrop;

public class CardItemUI : Draggable
{
    public Image icon;

    public override void UpdateObject()
    {
        CompleteCard card = obj as CompleteCard;
        if (card != null)
        {
            if (icon)
                icon.sprite = card.cardArt;
        }
        else
        {
            if (icon)
                icon.sprite = null;
        }

        gameObject.SetActive(card != null);
    }
}
