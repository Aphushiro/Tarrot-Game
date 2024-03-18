using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMng : MonoBehaviour
{
    public static GameMng Instance;
    public static int maxRooms;
    public int[] levelLimits;

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
    }
}
