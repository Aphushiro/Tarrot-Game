using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossToRoom : MonoBehaviour
{
    public void DeathListener()
    {
        DoorInteract door = FindObjectOfType<BossSpawn>().transform.GetComponentInChildren<DoorInteract>();

        if (door != null)
        {
            door.ForceOpen();
        }
    }
}
