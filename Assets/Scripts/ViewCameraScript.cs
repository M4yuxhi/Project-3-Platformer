using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCameraScript : MonoBehaviour
{
    public void Rotate(int axis, float angle, Vector3 pointOfRotation)
    {
        switch (axis)
        {
            case 0:

                transform.RotateAround(pointOfRotation, Vector3.right, angle);

                break;

            case 1:

                transform.RotateAround(pointOfRotation, Vector3.up, angle);

                break;
        }
    }
}
