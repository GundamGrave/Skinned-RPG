using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragAndDrop;

public class CharacterUI : ObjectContainer
{
    public PlayerEquipment player;

    public Slot head;
    public Slot torso;
    public Slot legs;
    public Slot weapon;

    [HideInInspector]
    public Slot[] slots;

    void Start()
    {
        slots = new Slot[4];

        // base these on existing slots set up in UI editor
        // we grab the return value so you could not specify values in the editor and rely on a layout group instead
        slots[0] = head = MakeSlot(player.head, head);
        slots[1] = torso = MakeSlot(player.torso, torso);
        slots[2] = legs = MakeSlot(player.legs, legs);
        slots[3] = weapon = MakeSlot(player.weapon, weapon);
    }

    public override bool CanDrop(Draggable dragged, Slot slot)
    {
        CompleteCard completeCard = dragged.obj as CompleteCard;

        // if we're dropping into empty space?
        if (completeCard == null || slot == null)
            return true;
        
        // only let the right type of item be dropped in this slot
        return (completeCard.cardType == CompleteCard.CardType.HeadArmourCard && slot == head)
            || (completeCard.cardType == CompleteCard.CardType.TorsoArmourCard && slot == torso)
            || (completeCard.cardType == CompleteCard.CardType.LegsArmourCard && slot == legs)
            || (completeCard.cardType == CompleteCard.CardType.WeaponCard && slot == weapon);
    }


    public override void Drop(Slot slot, ObjectContainer fromContainer)
    {
        player.head = head.item.obj as CompleteCard;
        player.torso = torso.item.obj as CompleteCard;
        player.legs = legs.item.obj as CompleteCard;
        player.weapon = weapon.item.obj as CompleteCard;
    }
}
