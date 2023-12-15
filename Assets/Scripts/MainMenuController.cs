using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : ButtonsMenu
{
    [SerializeField] private TMP_Text maxGoldCoinsTMP;


    protected override void Start()
    {
        base.Start();
        Globals.LoadData(Globals.SelectedSavesSlot);
        maxGoldCoinsTMP.text = Globals.MaxGoldCoinCount.ToString();
    }

    protected override void Update() => base.Update();

    protected override void DoAction(int selectedOption)
    {
        switch (selectedOption)
        {
            case 0 : SceneManager.LoadScene("GameScene");
                break;

            case 1 : SceneManager.LoadScene("CreditsScene");
                break;

            case 2 : SceneManager.LoadScene("PreferencesScene");
                break;

            case 3 :
                Application.Quit();
                break;
        }
    }
}
