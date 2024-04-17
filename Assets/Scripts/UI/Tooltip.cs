using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string message;

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipManager._instance.SetAndShowToolTip(message);
        Debug.Log("Mouse is over");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager._instance.HideToolTip();
    }
    /*
    private void OnMouseEnter()
    {
        TooltipManager._instance.SetAndShowToolTip(message);
        Debug.Log("Mouse is over");
    }

    private void OnMouseExit()
    {
        TooltipManager._instance.HideToolTip();
    }*/
}
