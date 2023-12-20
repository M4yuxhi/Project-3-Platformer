using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundControllerPersistent : SoundController
{
    [Header("Persistent")]
    [SerializeField] private string mySceneKillerName;

    void Awake()
    {
        // Verifica si ya existe otro objeto de la misma clase en la escena
        SoundControllerPersistent[] soundControllers = FindObjectsOfType<SoundControllerPersistent>();

        // Si ya existe otro objeto, destruye el objeto actual y sale del Awake
        if (soundControllers.Length > 1)
        {
            Destroy(gameObject.transform.parent.gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject.transform.parent);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName(mySceneKillerName))
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
