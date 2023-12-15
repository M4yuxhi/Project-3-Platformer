using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreferencesController : Menu
{
    [SerializeField] private TMP_Text selectedSaveSlotText;
    [SerializeField] private GameObject[] selectors;
    
    private int selectedSlot;

    protected override void Start() 
    {
        selectedSaveSlotText.text = Globals.SelectedSavesSlot.ToString();
    }

    protected override void Update() 
    { 
        base.Update();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainScene");
        }
    }

    protected override void DoAction(int selectedOption)
    {
        switch (selectedOption)
        {
            case 0 :
                Globals.LoadData(selectedSlot);
                break;
            case 1 :
                Globals.EraseData(selectedSlot);
                break;
        }
    }

    public override void Navegate(int min, int max)
    {
        base.Navegate(min, max);

        bool left = Input.GetKeyDown(KeyCode.A);
        bool left2 = Input.GetKeyDown(KeyCode.LeftArrow);
        bool right = Input.GetKeyDown(KeyCode.D);
        bool right2 = Input.GetKeyDown(KeyCode.RightArrow);

        if (left || left2)
        {
            if (selectedSlot > 1) selectedSlot--;
            else selectedSlot = Globals.MaxSavesSlotNumber;
            selectedSaveSlotText.text = selectedSlot.ToString();
        }
        if (right || right2)
        {
            if (selectedSlot < Globals.MaxSavesSlotNumber) selectedSlot++;
            else selectedSlot = 1;
            selectedSaveSlotText.text = selectedSlot.ToString();
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < selectors.Length; i++)
        {
            selectors[i].SetActive(false);
        }

        print(selectedOption);
        selectors[selectedOption].SetActive(true);
    }
}
