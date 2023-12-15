using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : ButtonsMenu
{
    [SerializeField] private TMP_Text maxGoldCoinsTMP;


    void Start()
    {
        Globals.LoadData();
        maxGoldCoinsTMP.text = Globals.MaxGoldCoinCount.ToString();

        SuperStart();
    }

    void Update() => SuperUpdate();

    protected override void DoAction(int selectedOption)
    {
        switch (selectedOption)
        {
            case 0 : 
                
                SceneManager.LoadScene("GameScene");
                
                break;

            case 1 :

                SceneManager.LoadScene("CreditsScene");

                break;

            case 2 :

                Globals.EraseData();
                Globals.LoadData();
                maxGoldCoinsTMP.text = Globals.MaxGoldCoinCount.ToString();

                break;

            case 3 :

                Application.Quit();

                break;
        }
    }
}
