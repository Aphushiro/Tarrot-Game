using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    // 0 --> Bottom door
    // 1 --> Top door
    // 2 --> Left door
    // 3 --> Right door
    public int openingDirection;
    private RoomTemplates templates;

    GameObject templateToThis;
    private bool spawned = false;

    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }
    void Spawn()
    {
        if (spawned)
        {
            return;
        }
        int roomID = 0;
        switch (openingDirection)
        {
            case 0:
                roomID = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[roomID], transform.position, Quaternion.identity);
                spawned = true;
                break;
            case 1:
                roomID = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[roomID], transform.position, Quaternion.identity);
                spawned = true;
                break;
            case 2:
                roomID = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[roomID], transform.position, Quaternion.identity);
                spawned = true;
                break;
            case 3:
                roomID = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[roomID], transform.position, Quaternion.identity);
                spawned = true;
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint") && other.GetComponent<RoomSpawner>().spawned == true)
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("OriginSpawn"))
        {
            Destroy(gameObject);
        }
    }
}
