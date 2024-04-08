using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PreRoom : MonoBehaviour
{
    public string roomType;

    public List<string> blockList = new List<string>();

    public string newName;
    RoomTemplates templates;

    private void Start()
    {
        roomType = gameObject.name;
        roomType = roomType.Replace("(Clone)", "");
        newName = roomType;

        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        templates.currentRooms.Add(this.gameObject);
    }

    private void Update()
    {

    }

    public void RemoveDoors ()
    {
        blockList = blockList.Distinct().ToList();
        string[] removeList = blockList.ToArray();
        foreach (string c in removeList)
        {
            newName = newName.Replace(c, string.Empty);
        }
        CreateNewRoom();
    }

    private void CreateNewRoom ()
    {
        GameObject newRoom = Resources.Load("Rooms/" + newName) as GameObject;
        Destroy(gameObject);
        Instantiate(newRoom, transform.position, Quaternion.identity);
    }
}
