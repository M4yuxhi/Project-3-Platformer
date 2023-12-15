using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{   
    [Header("Special Features")]
    [SerializeField] private bool needPlayer = false;
    [SerializeField] private char axis;
    private bool isPlayerOverMe = false;

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float distance;
    [SerializeField] private int dir;
    private float distanceTraveled = 0;
    private float delta;

    private bool stopMoment = false;
    private int waitTimeStop = 2;
    private float waitTimeStopAux = 0;

    private PlayerScript player;
    private Vector3 deltaVec = Vector3.zero;

    void Update()
    {
        if (needPlayer && !isPlayerOverMe) return;

        if (isPlayerOverMe) 
        {
            player.Move(deltaVec);
        }

        if (!needPlayer && stopMoment)
        {
            waitTimeStopAux += Time.deltaTime;

            if (waitTimeStopAux >= waitTimeStop)
            {
                stopMoment = false;
                waitTimeStopAux = 0;
            }

            return;
        }

        delta = speed * dir * Time.deltaTime;

        if (axis == 'x') 
        {
            deltaVec = new Vector3(delta, 0, 0);
            transform.position += deltaVec;
        }
        else if(axis == 'z')
        {
            deltaVec = new Vector3(0, 0, delta);
            transform.position += deltaVec;
        }

        distanceTraveled += Mathf.Abs(speed * dir * Time.deltaTime);

        if (distanceTraveled >= distance)
        {
            dir *= -1;
            distanceTraveled = 0;
            stopMoment = true;
            deltaVec = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject otherGO = other.gameObject;

        if (otherGO.CompareTag("Player"))
        {
            isPlayerOverMe = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject otherGO = other.gameObject;

        if (otherGO.CompareTag("Player"))
        {
            player = otherGO.GetComponent<PlayerScript>();
            isPlayerOverMe = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject otherGO = other.gameObject;

        if (otherGO.CompareTag("Player"))
        {
            isPlayerOverMe = false;
        }
    }
}
