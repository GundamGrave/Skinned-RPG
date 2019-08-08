using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isOccupied = false;
    public bool isAdjacent = false;
    public bool isSelected = false;
    public bool withinRange = false;

    Material defaultMaterial;
    [SerializeField] Material rangeMaterial;
    [SerializeField] Material selectedMaterial;

    LayerMask mask;


    // Start is called before the first frame update
    void Start()
    {
        defaultMaterial = GetComponent<MeshRenderer>().material;

        mask = LayerMask.GetMask("Tiles");
    }

    // Update is called once per frame
    void Update()
    {
        if (isAdjacent)
            gameObject.GetComponent<MeshRenderer>().material = rangeMaterial;
    }

    public void AdjacentChecking(int maxDistance, int currentDistance)
    {
        if (currentDistance == maxDistance)
            return;

        int localMax = maxDistance;
        int newDistance = currentDistance++;

        Tile tile;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 2, mask))
        {
            tile = hit.transform.gameObject.GetComponent<Tile>();

            tile.isAdjacent = true;
            tile.AdjacentChecking(localMax, newDistance);
        }

        if (Physics.Raycast(transform.position, -transform.forward, out hit, 2, mask))
        {
            tile = hit.transform.gameObject.GetComponent<Tile>();

            tile.isAdjacent = true;
            tile.AdjacentChecking(localMax, newDistance);
        }

        if (Physics.Raycast(transform.position, transform.right, out hit, 2, mask))
        {
            tile = hit.transform.gameObject.GetComponent<Tile>();

            tile.isAdjacent = true;
            tile.AdjacentChecking(localMax, newDistance);
        }

        if (Physics.Raycast(transform.position, -transform.right, out hit, 2, mask))
        {
            tile = hit.transform.gameObject.GetComponent<Tile>();

            tile.isAdjacent = true;
            tile.AdjacentChecking(localMax, newDistance);
        }
    }
}
