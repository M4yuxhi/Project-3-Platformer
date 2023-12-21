using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButtonScript : ButtonScript
{
    [SerializeField] private ButtonsMenu buttonsMenu;
    [SerializeField] private int optionAsigned;
    [SerializeField] private int type = 0;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        buttonsMenu.SetOption(optionAsigned, type);
        buttonsMenu.mouseOver = true;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        buttonsMenu.mouseOver = false;
    }
}
