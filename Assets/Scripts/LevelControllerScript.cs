using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControllerScript : MonoBehaviour
{
    [Header("Y position limit")]
    [SerializeField] private Transform playerTrans;
    
    [Header("Scenes")]
    [SerializeField] private string nextSceneName;

    [Header("Minigame")]
    [Header("Player")]
    [SerializeField] private Vector3 posMinigame;
    [SerializeField] private Vector3 posPostMinigame;
    [Header("Camera")]
    [SerializeField] private float dx;
    [SerializeField] private float dy;
    [SerializeField] private float dz;

    [HideInInspector] public static int goldCoinsToCollect;
    [HideInInspector] public static int greenCoinsToCollect;
    [HideInInspector] public static int currentGoldCoins;
    [HideInInspector] public static int currentGreenCoins;

    private GameObject cameraParent; 
    private bool isBarrelCollected = false;
    public static bool isMinigameActive = false;
    private const float minigameTime = 20f;
    private float minigameTimer = 0f;
    private bool playerPosSetted = false;

    public float MinigameTimer { get => minigameTimer; }

    // Start is called before the first frame update
    void Start()
    {
        goldCoinsToCollect = GameObject.FindGameObjectsWithTag("GoldCoin").Length;
        greenCoinsToCollect = GameObject.FindGameObjectsWithTag("GreenCoin").Length;

        cameraParent = GameObject.FindGameObjectWithTag("CameraParent");
    }

    void Update()
    {
        currentGoldCoins = GameObject.FindGameObjectsWithTag("GoldCoin").Length;
        currentGreenCoins = GameObject.FindGameObjectsWithTag("GreenCoin").Length;
        Vector3 playerPos = playerTrans.position; //Yup, trans joke. TRANS RIGHTS!!!

        if (currentGreenCoins == 0) 
        {
            Globals.GoldCoinsCollected = goldCoinsToCollect - currentGoldCoins;

            if (Globals.GoldCoinsCollected > Globals.MaxGoldCoinCount)
            {
                Globals.MaxGoldCoinCount = Globals.GoldCoinsCollected;
            }
            
            Globals.Saves.SaveData(Globals.Saves.SelectedSavesSlot);
            SceneManager.LoadScene(nextSceneName);
        }
        if (playerPos.y < -8) 
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }

        if (SceneManager.GetActiveScene().name == "Level4") Minigame();
    }

    private void Minigame()
    {
        isBarrelCollected = GameObject.FindGameObjectsWithTag("MinigameBarrel").Length == 0;

        if (minigameTimer < minigameTime && isBarrelCollected)
        {
            isMinigameActive = true;
            if (!playerPosSetted) 
            { 
                playerTrans.position = posMinigame;
                cameraParent.transform.position = posMinigame + new Vector3(dx, dy, dz);
            }
            playerPosSetted = true;
        }
        if (isMinigameActive)
        {
            minigameTimer += Time.deltaTime;

            if (minigameTimer >= minigameTime)
            {
                isMinigameActive = false;
                playerTrans.position = posPostMinigame;
                cameraParent.transform.position = posPostMinigame + new Vector3(dx, dy, dz);
            }
        }
    }
}
