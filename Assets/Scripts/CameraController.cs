using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CameraController : MonoBehaviour
{
    ParentConstraint pc;
    FreeFlyCamera ffc;
    public Quaternion q;
    public Vector3 p;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<ParentConstraint>();
        ffc = GetComponent<FreeFlyCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pc.constraintActive = !pc.constraintActive;
        }

        if (!pc.constraintActive)
        {
            ffc._active = true;
        }

        if (pc.constraintActive)
        {
            ffc._active = false;
            transform.localPosition = p;
            transform.localRotation = q;
        }
    }
}
