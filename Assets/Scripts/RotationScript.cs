using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    private float rotSpeed = 180;

    void Update()
    {
        float rotY = rotSpeed * Time.deltaTime;

        transform.Rotate(0, rotY, 0);
    }
}
