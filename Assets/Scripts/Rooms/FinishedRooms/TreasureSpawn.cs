using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureSpawn : MonoBehaviour
{
    bool treasureSpawned = false;

    GameMng gameMng;
    DoorInteract door;

    private void Start()
    {
        gameMng = GameMng.Instance;
        door = transform.GetComponentInChildren<DoorInteract>();
        StartCoroutine(SetupTreasureRoom());
    }

    IEnumerator SetupTreasureRoom ()
    {
        yield return new WaitForSeconds(0.1f);
        door.requiredTokens = gameMng.GetPentacleForDoor();
        treasureSpawned = true;
    }
}
