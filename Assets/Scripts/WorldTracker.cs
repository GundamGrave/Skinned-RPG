using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTracker : MonoBehaviour
{
    public static WorldTracker instance;

    public int numOfRooms;
    public int completedRooms;

    private RoomGeneration rg;
    public GameObject grids;

    void Start()
    {
        instance = this;
        rg = GetComponent<RoomGeneration>();
        grids = rg.grids;
    }

    // Update is called once per frame
    void Update()
    {
        grids = rg.grids;
    }

    public void Teleport()
    {
        if (completedRooms == numOfRooms)
        {
            Destroy(grids);
            completedRooms = 0;
            numOfRooms = 0;
            rg.GenerateRoom();
        }
    }
}
