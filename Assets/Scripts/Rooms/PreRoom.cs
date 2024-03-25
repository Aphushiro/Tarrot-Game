using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreRoom : MonoBehaviour
{
    public string roomType;
    public string newName;
    RoomTemplates templates;

    private void Start()
    {
        roomType = gameObject.name;
        roomType = roomType.Replace("(Clone)", "");
        newName = roomType;

        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("CheckToKeepThisRoom", 0.5f);
    }

    public void RemoveDoor (char blocked)
    {
        newName = newName.Trim(blocked);
    }

    private void CheckToKeepThisRoom ()
    {
        if (newName == roomType)
        {
            templates.currentRooms.Add(this.gameObject);
        } else
        {
            //CreateNewRoom();
        }
    }

    private void CreateNewRoom ()
    {
        GameObject newRoom = Resources.Load(newName) as GameObject;
        Destroy(gameObject);
        Instantiate(newRoom, transform.position, Quaternion.identity);
    }
}
