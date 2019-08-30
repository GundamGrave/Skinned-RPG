using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomInfo
{
    [System.Serializable]
    public struct rowData
    {
        public int[] row;
    }

    public rowData[] rows = new rowData[10];
}
