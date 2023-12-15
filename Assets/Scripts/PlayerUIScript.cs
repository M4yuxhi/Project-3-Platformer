using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIScript : MonoBehaviour
{ 
    [Header("Gold Coin")]
    [SerializeField] private TMP_Text goldCoinText;
    [SerializeField] private GameObject goldCoinParent;

    [Header("Green Coin")]
    [SerializeField] private TMP_Text greenCoinText;
    [SerializeField] private GameObject greenCoinParent;

    [Header("HP")]
    [SerializeField] private TMP_Text hpCountText;

    [Header("Pause Elements")]
    public PauseController pauseController;

    public void UpdateGoldCoinCounter(int value)
    {
        goldCoinText.text = value + " / " + LevelControllerScript.goldCoinsToCollect;
    }

    public void UpdateGreenCoinCounter(int value) 
    {
        greenCoinParent.SetActive(value != 0);

        greenCoinText.text = value + " / " + LevelControllerScript.greenCoinsToCollect;
    }

    public void UpdateHPCounter(int value)
    {
        hpCountText.text = value.ToString();
    }
}
