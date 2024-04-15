using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public string message;

    private void OnMouseEnter()
    {
        TooltipManager._instance.SetAndShowToolTip(message);
        Debug.Log("Mouse is over");
    }

    private void OnMouseExit()
    {
        TooltipManager._instance.HideToolTip();
    }
}
