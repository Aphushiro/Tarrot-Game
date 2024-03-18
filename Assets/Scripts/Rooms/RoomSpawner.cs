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

    public float spawnTime;

    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        float spawnDelay = Random.Range(0.1f, 0.4f);
        spawnTime = Time.time;
        Debug.Log(spawnTime);
        Invoke("Spawn", spawnDelay);
    }
    void Spawn()
    {
        GameObject thisRoom;
        if (spawned)
        {
            return;
        }
        int roomID = 0;
        switch (openingDirection)
        {
            case 0:
                roomID = Random.Range(0, templates.bottomRooms.Length);
                thisRoom = Instantiate(templates.bottomRooms[roomID], transform.position, Quaternion.identity);
                spawned = true;
                break;
            case 1:
                roomID = Random.Range(0, templates.topRooms.Length);
                thisRoom = Instantiate(templates.topRooms[roomID], transform.position, Quaternion.identity);
                spawned = true;
                break;
            case 2:
                roomID = Random.Range(0, templates.leftRooms.Length);
                thisRoom = Instantiate(templates.leftRooms[roomID], transform.position, Quaternion.identity);
                spawned = true;
                break;
            case 3:
                roomID = Random.Range(0, templates.rightRooms.Length);
                thisRoom = Instantiate(templates.rightRooms[roomID], transform.position, Quaternion.identity);
                spawned = true;
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint") && !transform.CompareTag("OriginSpawn"))
        {
            if (other.GetComponent<RoomSpawner>().spawnTime > spawnTime)
            {
                Destroy(gameObject);
            }
        }
        if (other.CompareTag("OriginSpawn") && !transform.CompareTag("OriginSpawn"))
        {
            Destroy(gameObject);
        }
    }
}
