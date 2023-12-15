using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonsMenu : MonoBehaviour
{
    [SerializeField] protected Image[] buttons;
    [HideInInspector] public bool mouseOver = false;

    protected int maximunValueOption;
    protected int selectedOption = 0;

    protected Color initialColor;

    // Start is called before the first frame update
    protected void SuperStart()
    {
        initialColor = buttons[0].color;
        maximunValueOption = buttons.Length - 1;
        UpdateButtons();
    }

    // Update is called once per frame
    protected void SuperUpdate()
    {
        Navegate(0, maximunValueOption);

        bool select = Input.GetKeyDown(KeyCode.Return);
        bool select2 = Input.GetMouseButtonDown(0);

        if (select || (select2 && mouseOver)) DoAction(selectedOption);
    }

    protected abstract void DoAction(int selectedOption);

    public virtual void Navegate(int min, int max)
    {
        bool up = Input.GetKeyDown(KeyCode.W);
        bool up2 = Input.GetKeyDown(KeyCode.UpArrow);
        bool down = Input.GetKeyDown(KeyCode.S);
        bool down2 = Input.GetKeyDown(KeyCode.DownArrow);

        if (up || up2)
        {
            if (selectedOption > min) selectedOption--;
            else selectedOption = max;
            UpdateButtons();
        }
        if (down || down2)
        {
            if (selectedOption < max) selectedOption++;
            else selectedOption = min;
            UpdateButtons();
        }
    }

    public void SetOption(int option)
    {
        selectedOption = option;
        UpdateButtons();
    }

    protected void UpdateButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].color = initialColor;
        }

        buttons[selectedOption].color = Color.red;
    }
}
