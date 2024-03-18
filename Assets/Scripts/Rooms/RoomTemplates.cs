using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public List<GameObject> currentRooms;
    public bool roomLimitReached = false;

    public void CheckRoomSize ()
    {
        if (currentRooms.Count >= GameMng.maxRooms)
        {
            roomLimitReached = true;
        }
    }
    
}
