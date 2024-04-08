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

    public float timeToUpdate = 2f;
    private int curRooms = 0;
    public bool floorIsLoading = true;

    private ReplaceFinal replaceFinal;

    private void Start()
    {
        replaceFinal = GetComponent<ReplaceFinal>();
    }

    private void Update()
    {

        // Determine when map has loaded
        int l = currentRooms.Count;

        if (curRooms != l && floorIsLoading == true)
        {
            timeToUpdate = 2f;
            curRooms = l;
        }

        if (timeToUpdate > 0f && floorIsLoading == true)
        {
            timeToUpdate -= Time.deltaTime;
        }
        else if (floorIsLoading == true)
        {
            // Reload floor when it has loaded, and initiate room replacement
            
            StartCoroutine(FinishTemplates());
        }
    }

    IEnumerator FinishTemplates ()
    {
        floorIsLoading = false;
        ReloadRooms();
        yield return new WaitForSeconds(1);
        ReloadRooms();
        //Invoke("ReloadRooms", 1f);
        yield return new WaitForSeconds(1);
        replaceFinal.StartRoomReplacement();
    }

    public void ReloadRooms ()
    {
        currentRooms.Clear();
        PreRoom[] roomSripts = FindObjectsOfType<PreRoom>();
        foreach (PreRoom room in roomSripts)
        {
            room.RemoveDoors();
        }
        Debug.Log("Rooms were reloaded");
    }

    public void CheckRoomSize ()
    {
        if (currentRooms.Count >= GameMng.maxRooms)
        {
            roomLimitReached = true;
        }
    }
    
}
