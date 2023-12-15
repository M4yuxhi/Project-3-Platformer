using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Menu : MonoBehaviour
{
    [SerializeField] protected int maximunValueOption;
    protected int selectedOption = 0;

    protected abstract void Start();

    protected virtual void Update()
    {
        Navegate(0, maximunValueOption);

        bool select = Input.GetKeyDown(KeyCode.Return);

        if (select) DoAction(selectedOption);
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
        }
        if (down || down2)
        {
            if (selectedOption < max) selectedOption++;
            else selectedOption = min;
        }
    }
}

public abstract class ButtonsMenu : Menu
{
    [SerializeField] protected Image[] buttons;
    [HideInInspector] public bool mouseOver = false;

    protected Color initialColor;

    protected override void Start()
    {
        initialColor = buttons[0].color;
        maximunValueOption = buttons.Length - 1;
        UpdateButtons();
    }

    protected override void Update()
    {
        base.Update();
        bool select2 = Input.GetMouseButtonDown(0);

        if (select2 && mouseOver) DoAction(selectedOption);
    }

    public override void Navegate(int min, int max)
    {
        base.Navegate(min, max);
        UpdateButtons();
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
