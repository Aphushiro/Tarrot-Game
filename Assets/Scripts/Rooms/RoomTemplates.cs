using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class OnTemplateOver : UnityEvent<float[]> { }

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public static float roomSize;
    public List<GameObject> currentRooms;
    public bool roomLimitReached = false;

    public float timeToUpdate = 2f;
    private int curRooms = 0;
    public bool floorIsLoading = true;

    private ReplaceFinal replaceFinal;
    public OnTemplateOver onTemplateOver;


    private void Start()
    {
        replaceFinal = GetComponent<ReplaceFinal>();
    }

    private void Update()
    {

        // Determine when map has loaded
        // This part might break with timescale mess
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
        yield return new WaitForSeconds(1);
        ReloadRooms();
        //Invoke("ReloadRooms", 1f);
        yield return new WaitForSeconds(1);
        roomSize = currentRooms[0].transform.lossyScale.x;
        onTemplateOver.Invoke(GetNavGridSize());
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

    private float[] GetNavGridSize ()
    {
        // Float values are ordered as such{ -x, x, -y, y }
        float[] sizeVals = new float[] { 0, 0, 0, 0 };
        for (int i = 0; i < currentRooms.Count; i++)
        {
            Vector2 pos = currentRooms[i].transform.position;

            if (pos.x < sizeVals[0])
            {
                sizeVals[0] = pos.x;
            }

            if (pos.x > sizeVals[1])
            {
                sizeVals[1] = pos.x;
            }

            if (pos.y < sizeVals[2])
            {
                sizeVals[2] = pos.y;
            }

            if (pos.y > sizeVals[3])
            {
                sizeVals[3] = pos.y;
            }
        }

        float[] gridVals = new float[2];
        gridVals[0] = (Mathf.Abs(sizeVals[0]) > Mathf.Abs(sizeVals[1])) ? Mathf.Abs(sizeVals[0]) : Mathf.Abs(sizeVals[1]);
        gridVals[1] = (Mathf.Abs(sizeVals[2]) > Mathf.Abs(sizeVals[3])) ? Mathf.Abs(sizeVals[2]) : Mathf.Abs(sizeVals[3]);

        return gridVals;
    } 

}
