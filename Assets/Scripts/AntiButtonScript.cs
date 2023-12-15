using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AntiButtonScript : ButtonScript
{
    [SerializeField] private ButtonsMenu buttonsMenu;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        buttonsMenu.mouseOver = false;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
    }
}
