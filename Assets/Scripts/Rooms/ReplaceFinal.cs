using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceFinal : MonoBehaviour
{
    private RoomTemplates roomTemplates;

    public List<GameObject> oneRooms;
    public List<GameObject> twoRooms;
    public List<GameObject> threeRooms;

    int bossCount = 1;
    int treasureCount = 1;
    int enemyCOunt = 1;

    public List<GameObject> finRooms;

    // Start is called before the first frame update
    void Start()
    {
        roomTemplates = GetComponent<RoomTemplates>();

    }

    void GenBossRoom ()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
