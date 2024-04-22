using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PentacleVis : MonoBehaviour
{
    public List<Image> pentacles;

    public void UpdatePentacles (int curPentacles)
    {
        for (int i = 0; i < pentacles.Count; i++)
        {
            pentacles[i].enabled = false;
        }

        for (int i = 0;i < curPentacles;i++)
        {
            pentacles[i].enabled = true;
        }
    }
}
