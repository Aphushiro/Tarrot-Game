using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureSpawn : MonoBehaviour
{
    public SpriteRenderer[] spritesToFade = new SpriteRenderer[2];
    public float spritesFadeTime = 1f;
    float spritesFadeVal = 0f;

    bool treasureSpawned = false;
    bool playerEntered = false;

    GameMng gameMng;
    DoorInteract door;

    private void Start()
    {
        gameMng = GameMng.Instance;
        door = transform.GetComponentInChildren<DoorInteract>();
        StartCoroutine(SetupTreasureRoom());
    }

    IEnumerator SetupTreasureRoom ()
    {
        yield return new WaitForSeconds(0.1f);
        door.requiredTokens = gameMng.GetPentacleForDoor();
        GameObject treasure = Instantiate(gameMng.GetTreasure(), transform.position, Quaternion.identity);
        spritesToFade[1] = treasure.GetComponentInChildren<SpriteRenderer>();

        // Turn sprites invisible
        for (int i = 0; i < spritesToFade.Length; i++)
        {
            spritesToFade[i].color = new Color(1, 1, 1, spritesFadeVal);
        }

        treasureSpawned = true;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && treasureSpawned == true)
        {
            StartCoroutine(ShowTreasure());
        }
    }

    IEnumerator ShowTreasure()
    {
        yield return new WaitForSeconds(spritesFadeTime);
        // Things to happen after treasure was shown
        playerEntered = true;
    }



    private void Update()
    {
        if (playerEntered == true && treasureSpawned == true && spritesFadeVal < 1f)
        {
            spritesFadeVal += Time.deltaTime / spritesFadeTime;
            spritesFadeVal = Mathf.Clamp01(spritesFadeVal);
            for (int i = 0; i < spritesToFade.Length; i++)
            {
                spritesToFade[i].color = new Color(1, 1, 1, spritesFadeVal);
            }

        }
    }
}
