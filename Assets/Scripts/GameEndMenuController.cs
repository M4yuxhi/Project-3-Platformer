using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndMenuController : ButtonsMenu
{
    [SerializeField] private TMP_Text goldCoinsRecord;
    [SerializeField] private TMP_Text goldCoinsCollected;

    protected override void Start()
    {
        base.Start();

        goldCoinsRecord.text = Globals.MaxGoldCoinCount.ToString();
        goldCoinsCollected.text = Globals.GoldCoinsCollected.ToString();
    }

    protected override void Update() => base.Update();
    

    protected override void DoAction(int selectedOption)
    {
        switch (selectedOption)
        {
            case 0 :

                SceneManager.LoadScene("GameScene");

                break;

            case 1 :

                SceneManager.LoadScene("MainScene");

                break;

            case 2 :

                Application.Quit();

                break;
        }
    }
}
