using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] tiles;

    int[,] roomArr;

    public int roomWidth, roomLength;



    Random rand;

    // Start is called before the first frame update
    void Start()
    {
        roomArr = new int[roomWidth, roomLength];

        for (int i = 0; i < roomWidth; i++)
        {
            for (int j = 0; j < roomLength; j++)
            {
                roomArr[i, j] = (int)Random.Range(0, tiles.Length);
            }
        }

        GenerateRoom();
    }

    private void GenerateRoom()
    {
        for (int i = 0; i < roomWidth; i++)
        {
            for (int j = 0; j < roomLength; j++)
            {
                Vector3 location = new Vector3(-13.5f + 3*i, 0.2f, -13.5f + 3*j);
                Instantiate(tiles[roomArr[i, j]], location, transform.rotation);
            }
        }
    }
}
