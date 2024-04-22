using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CupManaBar : MonoBehaviour
{

    public Slider slider;

    public void SetMaxMana(float newMaxMana)
    {
        if (newMaxMana < slider.value)
        {
            slider.value = newMaxMana;
        }

        slider.maxValue = newMaxMana;

    }

    public void SetMana(float newMana)
    {
        slider.value = newMana;
    }
}
