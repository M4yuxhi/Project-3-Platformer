using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButtonScript : ButtonScript
{
    [SerializeField] private ButtonsMenu buttonsMenu;
    [SerializeField] private int optionAsigned;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        buttonsMenu.SetOption(optionAsigned);
        buttonsMenu.mouseOver = true;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        buttonsMenu.mouseOver = false;

    }
}
