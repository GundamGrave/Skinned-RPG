using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGeneration : MonoBehaviour
{
    [SerializeField] GameObject[] rooms;

    [SerializeField] GameObject Player;

    int[,] roomArr;

    public int roomWidth, roomLength;



    Random rand;

    // Start is called before the first frame update
    void Start()
    {
        roomArr = new int[roomWidth, roomLength];

        for(int i = 0; i < roomWidth; i++)
        {
            for(int j = 0; j < roomLength; j++)
            {
                roomArr[i, j] = (int)Random.Range(0, rooms.Length - 1);
            }
        }

        GenerateRoom();
    }

    private void GenerateRoom()
    {
        roomArr[0, 0] = rooms.Length - 1;

        for (int i = 0; i < roomWidth; i++)
        {
            for (int j = 0; j < roomLength; j++)
            {
                Vector3 location = new Vector3(i * 10, 0, j * 10);
                Instantiate(rooms[roomArr[i, j]], location, transform.rotation);
            }
        }

        //Instantiate(Player, new Vector3(0, 0, 0), transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
