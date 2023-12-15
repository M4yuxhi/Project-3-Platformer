using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnlyViewScene : MonoBehaviour
{
    [SerializeField] private string targetSceneName;

    void Update()
    {
        bool exit = Input.GetKeyDown(KeyCode.Escape);

        if (exit) SceneManager.LoadScene(targetSceneName);
    }
}
