using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControllerScript : MonoBehaviour
{
    [SerializeField] private Transform playerTrans;

    [HideInInspector] public static int goldCoinsToCollect;
    [HideInInspector] public static int greenCoinsToCollect;
    [HideInInspector] public static int currentGoldCoins;
    [HideInInspector] public static int currentGreenCoins;

    // Start is called before the first frame update
    void Start()
    {
        goldCoinsToCollect = GameObject.FindGameObjectsWithTag("GoldCoin").Length;
        greenCoinsToCollect = GameObject.FindGameObjectsWithTag("GreenCoin").Length;
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
            
            Globals.SaveData();
            SceneManager.LoadScene("GameEndScene");
        }
        if (playerPos.y < -8) 
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }
}
