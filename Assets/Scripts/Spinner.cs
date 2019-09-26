using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public float deltaX, deltaY, deltaZ;

    public float deltaVertY;

    public float floatTime;
    public float spinTime;

    private float OriginalY;

    private bool up = true;

    // Start is called before the first frame update
    void Start()
    {
        OriginalY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(deltaX, deltaY, deltaZ) * spinTime);

        if (up)
        {
            transform.position += new Vector3(0, deltaVertY, 0) * floatTime;
            if (transform.position.y >= OriginalY + deltaVertY)
                up = false;
        }
        else
        {
            transform.position -= new Vector3(0, deltaVertY, 0) * floatTime;
            if (transform.position.y <= OriginalY)
                up = true;
        }
    }
}
