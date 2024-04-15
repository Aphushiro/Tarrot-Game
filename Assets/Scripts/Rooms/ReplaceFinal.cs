using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceFinal : MonoBehaviour
{
    private RoomTemplates roomTemplates;

    public GameObject startRoom;
    public List<GameObject> oneRooms;
    public List<GameObject> twoRooms;
    public List<GameObject> threeRooms;

    int bossCount = 1;
    int treasureCount = 1;
    int enemyCount = 1;

    public List<GameObject> finRooms;

    // Start is called before the first frame update
    void Start()
    {
        roomTemplates = GetComponent<RoomTemplates>();
        int amount = roomTemplates.currentRooms.Count;

        treasureCount += Mathf.FloorToInt(amount/10);
    }

    public void StartRoomReplacement ()
    {
        StartCoroutine(ConstructRoom());
    }

    IEnumerator ConstructRoom()
    {
        OrderRooms();
        yield return new WaitForSeconds(1);
        GenStartRoom();
        // Generate boss rooms
        for (int i = 0; i < bossCount; i++)
        {
            GenBossRoom();
        }
        yield return new WaitForSeconds(.5f);

        // Generate treasure rooms
        for (int i = 0; i < treasureCount; i++)
        {
            GenTreasureRoom();
        }
        yield return new WaitForSeconds(.5f);

        FillDeadendEnemy();
        // Generate enemy rooms
        for (int i = 0; i < enemyCount; i++)
        {
            GenEnemyRoom();
        }

        // Fill out empty rooms
        foreach (GameObject room in oneRooms)
        {
            if (room == null) continue;

            GenEmptyRoom(room);
        }
        oneRooms.Clear();

        foreach (GameObject room in twoRooms)
        {
            if (room == null) continue;

            GenEmptyRoom(room);
        }
        twoRooms.Clear();

        foreach (GameObject room in threeRooms)
        {
            if (room == null) continue;

            GenEmptyRoom(room);
        }
        threeRooms.Clear();
    }

    public void OrderRooms ()
    {
        int unassigned = 0;
        var rooms = roomTemplates.currentRooms;
        for (int i = 0; i < rooms.Count; i++)
        {
            string roomType = rooms[i].transform.name;
            roomType = roomType.Replace("(Clone)", "");
            roomType.Trim();
            if (roomType.Contains(")Start"))
            {
                startRoom = rooms[i];
            }
            else if (roomType.Length == 1)
            {
                oneRooms.Add(rooms[i]);
            } else if (roomType.Length == 2)
            {
                twoRooms.Add(rooms[i]);
            } else if (roomType.Length == 3)
            {
                threeRooms.Add(rooms[i]);
            } else
            {
                Debug.Log(rooms[i] + "wasn't assigned");
                unassigned++;
            }
        }
        Debug.Log("Rooms ordered. Unassigned: " + unassigned);
    }

    void GenStartRoom ()
    {
        string id = "start";

        // Assuming we only use dead ends
        //int toReplace = Random.Range(0, oneRooms.Count);

        // Redo tempName if we want "start room variation".
        string tempName = "TBLR";
        string findName = tempName + id;

        Vector2 pos = startRoom.transform.position;

        GameObject newRoom = Resources.Load("Rooms/FinishedRooms/Start/" + findName) as GameObject;
        Destroy(startRoom);

        // Instantiate new room
        GameObject newStart = Instantiate(newRoom, pos, Quaternion.identity);
        startRoom = newStart;
    }

    void GenBossRoom ()
    {
        string id = "boss";

        // Assuming we only use dead ends
        int toReplace = Random.Range(0, oneRooms.Count);
        GameObject tempRoom = oneRooms[toReplace];

        Vector2 pos = tempRoom.transform.position;
        string tempName = tempRoom.GetComponent<PreRoom>().newName;
        string findName = tempName + id;

        GameObject newRoom = Resources.Load("Rooms/FinishedRooms/Boss/" + findName) as GameObject;

        // Remove template room from list
        Destroy(tempRoom);
        oneRooms.Remove(oneRooms[toReplace]);

        // Instantiate new room
        GameObject newBoss = Instantiate(newRoom, pos, Quaternion.identity);
        finRooms.Add(newBoss);

    }

    void GenTreasureRoom ()
    {
        string id = "treasure";

        // Assuming we only use dead ends
        int toReplace = Random.Range(0, oneRooms.Count);
        GameObject tempRoom = oneRooms[toReplace];

        Vector2 pos = tempRoom.transform.position;
        string tempName = tempRoom.GetComponent<PreRoom>().newName;
        string findName = tempName + id;
        //Debug.Log(findName);

        GameObject newRoom = Resources.Load("Rooms/FinishedRooms/Treasure/" + findName) as GameObject;

        // Remove template room from list
        Destroy(tempRoom);
        oneRooms.Remove(oneRooms[toReplace]);

        // Instantiate new room
        GameObject newTreasure = Instantiate(newRoom, pos, Quaternion.identity);
        finRooms.Add(newTreasure);
    }

    void FillDeadendEnemy ()
    {
        foreach (var enemy in oneRooms)
        {
            string id = "enemy";

            Vector2 pos = enemy.transform.position;
            string tempName = enemy.GetComponent<PreRoom>().newName;
            string findName = tempName + id;
            //Debug.Log(findName);

            GameObject newRoom = Resources.Load("Rooms/FinishedRooms/Enemy/" + findName) as GameObject;
            GameObject newEnemy = Instantiate(newRoom, pos, Quaternion.identity);
            finRooms.Add(newEnemy);
            Destroy(enemy);
        }
        oneRooms.Clear();
    }

    void GenEnemyRoom ()
    {
        //int roomType = Random.Range()
    }

    void GenEmptyRoom (GameObject tempRoom)
    {
        string id = "empty";

        // Assuming we only use dead ends

        Vector2 pos = tempRoom.transform.position;
        string tempName = tempRoom.GetComponent<PreRoom>().newName;
        string findName = tempName + id;

        GameObject newRoom = Resources.Load("Rooms/FinishedRooms/Empty/" + findName) as GameObject;

        // Replace the template room
        Destroy(tempRoom);

        // Instantiate new room
        GameObject newEmpty = Instantiate(newRoom, pos, Quaternion.identity);
        finRooms.Add(newEmpty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
