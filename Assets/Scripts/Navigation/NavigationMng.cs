using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NavigationMng : MonoBehaviour
{
    

    public void UpdateGraphs ()
    {
        var gg = AstarPath.active.data.gridGraph;

        // Calculate size
        // +20 = +80 dimension
        gg.SetDimensions(200, 200, 0.5f);
        AstarPath.active.Scan();
    }
}
