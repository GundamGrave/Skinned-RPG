using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragAndDrop;

public class InventoryUI : ObjectContainerArray
{
    public Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        CreateSlots(inventory.items);
    }
}
