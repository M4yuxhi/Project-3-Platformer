using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : ButtonsMenu
{
    [SerializeField] private TMP_Text maxGoldCoinsTMP;
    [SerializeField] private AudioMixer mixer;

    private const string MIXER_MASTER = "MasterVol";
    private const string MIXER_MUSIC = "MusicVol";
    private const string MIXER_FX = "FXVol";

    protected override void Start()
    {
        base.Start();
        Globals.Saves.LoadData(Globals.Saves.SelectedSavesSlot);
        maxGoldCoinsTMP.text = Globals.MaxGoldCoinCount.ToString();

        mixer.SetFloat(MIXER_MASTER, Mathf.Log10(Globals.Sound.VolMaster) * 20);
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(Globals.Sound.VolMusic) * 20);
        mixer.SetFloat(MIXER_FX, Mathf.Log10(Globals.Sound.VolFX) * 20);
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
