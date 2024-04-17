using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    bool enemiesSpawned = false;
    Vector2 playerPos;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && enemiesSpawned == false)
        {
            playerPos = other.transform.position;
            SpawnEnemies();
            enemiesSpawned = true;
        }
    }

    void SpawnEnemies ()
    {
        var enemyList = GameMng.Instance.availableEnemies;
        int enemyWaveSize = GameMng.Instance.waveSize;

        for (int i = 0; i < enemyWaveSize; i++)
        {
            int enemyType = Random.Range(0, enemyList.Count);

            float xSize = 7f;
            float ySize = 7f;

            float xPos = Random.Range(0, xSize);
            float yPos = Random.Range(0, ySize);

            Vector2 enemyPos = new Vector2(xPos, yPos);

            float dist = (enemyPos - playerPos).magnitude;

            while (dist < 1f)
            {
                xPos = Random.Range(0, xSize);
                yPos = Random.Range(0, ySize);

                enemyPos = new Vector2(xPos, yPos);

                dist = (enemyPos - playerPos).magnitude;
            }
            Vector2 actualPos = new Vector2(transform.position.x - enemyPos.x, transform.position.y - enemyPos.y);
            GameObject newEnemy = Instantiate(enemyList[enemyType], actualPos, Quaternion.identity) as GameObject;
        }
    }
}
