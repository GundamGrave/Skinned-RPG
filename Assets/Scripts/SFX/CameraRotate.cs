using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{

    public float speed = 10;

    float lastMouseX;
    float lastMouseY;


    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float deltaX = Input.mousePosition.x - lastMouseX;
            float deltaY = Input.mousePosition.y - lastMouseY;

            Vector3 angles = transform.eulerAngles + (Vector3.right * deltaY + Vector3.up * deltaX) * Time.deltaTime * speed;
            if (angles.x > 180)
                angles.x -= 360;
            angles.x = Mathf.Clamp(angles.x, -70, 70);
            transform.eulerAngles = angles;
        }

        lastMouseX = Input.mousePosition.x;
        lastMouseY = Input.mousePosition.y;
    }
}
