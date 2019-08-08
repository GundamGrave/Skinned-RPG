using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour
{
    public int movementRange;

    public Tile tile;

    public int number;

    LayerMask mask;

    void Start()
    {

        mask = LayerMask.GetMask("Tiles");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.up, out hit, 1, mask))
        {
            tile = hit.transform.gameObject.GetComponent<Tile>();

            tile.isAdjacent = true;
            tile.AdjacentChecking(movementRange, 0);
        }
    }
}
