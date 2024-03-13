using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    private RoomTemplates templates;

    GameObject templateToThis;

    // 0 --> Bottom door
    // 1 --> Top door
    // 2 --> Left door
    // 3 --> Right door

    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    void SelectTemplate()
    {
        
    }

    void Spawn()
    {
        switch (openingDirection)
        {
            case 0:

                break;

            default:
                break;

        }
    }
}
