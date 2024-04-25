using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameMng : MonoBehaviour
{
    public static GameMng Instance;

    public UnityEvent onFloorLoaded;

    public int currentLevel = 0;
    public static int maxRooms;
    public int[] levelLimits;

    // Boss list
    public List<GameObject> allBosses;

    // Enemy settings for floor
    public List<GameObject> allEnemies;
    public List<GameObject> availableEnemies;
    int nextEnemy = 0;
    public int waveSize = 4;

    // Treasure per floor
    public int[] maxPentacleForDoor;

    // Treasure list
    public GameObject StatUpgradePrefab;
    public GameObject tarotCardPickupPrefab;

    //This thing is impossible to not hard code right now, but please expand for the future
    public int tarotCardsAvailable = 0;

    public void OnFinishedLoadingFloor ()
    {
        onFloorLoaded.Invoke();
        FindObjectOfType<PlayerMovement>().ToggleCanMove();
    }

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (Instance == null )
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }
    private void SetMaxRooms(int setTo)
    {
        maxRooms = levelLimits[setTo];
    }

    private void Start()
    {
        SetMaxRooms(0);

        for (int i = 0; i < 2; i++)
        {
            availableEnemies.Add(allEnemies[nextEnemy]);
            nextEnemy++;
        }
    }

    public int GetPentacleForDoor()
    {
        int max = maxPentacleForDoor[currentLevel];
        int pForDoor = Random.Range(1, max);
        Debug.Log("Door cost: " + pForDoor);
        return pForDoor;
    }

    public void NextLevel ()
    {
        AddEnemy();
        waveSize += 2;
    }

    public void AddEnemy ()
    {
        if (allEnemies.Count < nextEnemy)
        {
            Debug.Log("All enemies added");
            return;
        }
        availableEnemies.Add(allEnemies[nextEnemy]);
        nextEnemy++;
    }

    public GameObject GetBossForLevel ()
    {
        if (allBosses[currentLevel] == null)
        {
            return allBosses[0];
        }

        return allBosses[currentLevel];
    }

    public GameObject GetTreasure()
    {
        GameObject[] toGive = new GameObject[2] { StatUpgradePrefab, tarotCardPickupPrefab };
        int treasure = 0;
        if (tarotCardsAvailable > 0)
        {
            treasure = Random.Range(0, toGive.Length);
            tarotCardsAvailable--;
        }
        return toGive[treasure];
    }

    public void ResetGame ()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        GameObject[] finRooms = GameObject.FindGameObjectsWithTag("FinalRoom");
        foreach (GameObject finRoom in finRooms)
        {
            Destroy(finRoom);
        }

        FindObjectOfType<ReplaceFinal>().FullClearFloor();
        FindObjectOfType<PlayerMovement>().ToggleCanMove();

        FindObjectOfType<RoomTemplates>().FullClearFloor();
    }
}
