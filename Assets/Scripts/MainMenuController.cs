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
    [SerializeField] private GameObject[] menuParents;
    [SerializeField] private SoundController fxController;
    private int menuMode = 0;
    private int selectedOption2 = 0;
    private int maximunValueOption2 = 0;

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

        maximunValueOption = 3;
        maximunValueOption2 = 1;
    }

    protected override void Update() => base.Update();

    protected override void DoAction(int selectedOption)
    {
        if(menuMode == 0)
        {
            switch (selectedOption)
            {
                case 0:
                    menuParents[menuMode].SetActive(false);
                    menuMode = 1;
                    UpdateButtons2();
                    menuParents[menuMode].SetActive(true);
                    break;

                case 1:
                    SceneManager.LoadScene("CreditsScene");
                    break;

                case 2:
                    SceneManager.LoadScene("PreferencesScene");
                    break;

                case 3:
                    Application.Quit();
                    break;
            }
        }
        else
        {
            switch (selectedOption2)
            {
                case 0 : SceneManager.LoadScene("Level1");
                    break;
                case 1 : SceneManager.LoadScene(Globals.Saves.LastLevelIndex + 1);
                    break;
            }
        }

        fxController.PlayAudioClip("select", false, 0.4f, 1);
    }

    public override void Navegate(int min, int max)
    {
        max = (menuMode == 0) ? maximunValueOption : maximunValueOption2;

        if(menuMode == 0) base.Navegate(min, max);
        else
        {
            bool up = Input.GetKeyDown(KeyCode.W);
            bool up2 = Input.GetKeyDown(KeyCode.UpArrow);
            bool down = Input.GetKeyDown(KeyCode.S);
            bool down2 = Input.GetKeyDown(KeyCode.DownArrow);
            bool back = Input.GetKeyDown(KeyCode.Escape);

            if (up || up2)
            {
                if (selectedOption2 > min) selectedOption2--;
                else selectedOption2 = max;
                UpdateButtons2();
            }
            if (down || down2)
            {
                if (selectedOption2 < max) selectedOption2++;
                else selectedOption2 = min;
                UpdateButtons2();
            }
            if (back)
            {
                menuParents[menuMode].SetActive(false);
                menuMode = 0;
                menuParents[menuMode].SetActive(true);
                fxController.PlayAudioClip("back", false, 0.6f, 2);
            }
        }
    }

    public override void SetOption(int option, int type)
    {
        if(type == 0)
        {
            SetOption(option);
        }
        else
        {
            selectedOption2 = option;
            UpdateButtons2();
        }
    }

    private void UpdateButtons2()
    {

        for (int i = maximunValueOption; i < buttons.Length; i++)
        {
            buttons[i].color = initialColor;
        }

        buttons[selectedOption2 + maximunValueOption + 1].color = Color.red;
    }
}
