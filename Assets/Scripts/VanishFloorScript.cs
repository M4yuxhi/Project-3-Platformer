using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishFloorScript : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private float fallSpeed;
    [SerializeField] private float shakeIntensity;

    private int action = 0;

    private const int shakeTime = 2;
    private float shakeTimeAux = 0;
    
    private const int vanishTime = 8;
    private float vanishTimeAux = 0;
    private bool platformWasStep = false;
    
    private Vector3 initialPos;
    private Quaternion initialRot;

    void Start()
    {
        initialPos = platform.transform.position;
        initialRot = platform.transform.rotation;
    }

    void Update() 
    {
        Vector3 pos = platform.transform.position;

        if (platformWasStep)
        {
            switch (action) 
            {
                case 0 :

                    shakeTimeAux += Time.deltaTime;
                    Shake();

                    if (shakeTimeAux >= shakeTime) 
                    {
                        shakeTimeAux = 0;
                        platform.transform.rotation = initialRot;
                        action++;
                    }

                    break;

                case 1 :

                    vanishTimeAux += Time.deltaTime;
                    
                    if (pos.y > -8) Fall();
                    else platform.SetActive(false);

                    if (vanishTimeAux >= vanishTime) 
                    {
                        vanishTimeAux = 0;
                        platform.transform.position = initialPos;
                        platform.SetActive(true);
                        action = 0;
                        platformWasStep = false;
                    }

                    break;
            }
        }
    }

    private void Shake() 
    {
        float rotX = Random.Range(-shakeIntensity, shakeIntensity);
        float rotY = Random.Range(-shakeIntensity, shakeIntensity);
        float rotZ = Random.Range(-shakeIntensity, shakeIntensity);

        // Aplica las rotaciones al objeto.
        platform.transform.Rotate(rotX, rotY, rotZ);
    }

    private void Fall() 
    {
        platform.transform.Translate(new Vector3(0, -fallSpeed * Time.deltaTime, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject otherGO = other.gameObject;

        if (otherGO.CompareTag("Player"))
        {
            platformWasStep = true;
        }
    }
}
