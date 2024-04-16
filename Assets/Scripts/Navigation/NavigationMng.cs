using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Unity.Mathematics;
using System;

public class NavigationMng : MonoBehaviour
{

    public void UpdateGraphs(float[] gridSize)
    {
        var gg = AstarPath.active.data.gridGraph;
        Debug.Log(gridSize[0] + " - " + gridSize[1]);

        float nodeSize = 0.5f;
        int[] gSizes = GetGridSizeValues(gridSize[0], gridSize[1], nodeSize);

        gg.SetDimensions(gSizes[0], gSizes[0], nodeSize);
        AstarPath.active.Scan();
    }

    private int[] GetGridSizeValues (float x, float y, float ns)
    {
        float rs = RoomTemplates.roomSize;
        float roomScaleNode = rs * 10 * (1/ ns);

        int scaleX = Mathf.RoundToInt((x * (1 / ns) * rs) + roomScaleNode);
        int scaleY = Mathf.RoundToInt((y * (1 / ns) * rs) + roomScaleNode);

        int[] sizes = new int[2] { scaleX, scaleY};
        return sizes;
    }
}
