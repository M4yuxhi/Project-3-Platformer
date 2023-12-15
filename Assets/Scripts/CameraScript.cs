using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 50;
    [SerializeField] private Transform playerTransform;

    private Vector3 distToPlayer;

    private int zoom = 0;

    void Start()
    {
        distToPlayer = playerTransform.position - transform.position;
        Vector3 newPos = playerTransform.position;
        newPos.z += zoom - 20;
        transform.position = Vector3.MoveTowards(transform.position, newPos + distToPlayer, maxSpeed * Time.deltaTime);
        
    }

    void LateUpdate()
    {
        Vector3 newPos = playerTransform.position;
        newPos.z += zoom - 20;

        transform.parent.position = Vector3.MoveTowards(transform.parent.position, newPos + distToPlayer, maxSpeed * Time.deltaTime);
    }
}
