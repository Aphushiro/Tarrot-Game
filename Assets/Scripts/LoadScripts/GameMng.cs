using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMng : MonoBehaviour
{
    public static GameMng Instance;

    public int currentLevel = 0;
    public List<GameObject> allBosses;

    public static int maxRooms;
    public int[] levelLimits;

    public List<GameObject> allEnemies;
    public List<GameObject> availableEnemies;
    int nextEnemy = 0;
    public int waveSize = 4;

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

    public void NextLevel ()
    {
        AddEnemy();
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
}
