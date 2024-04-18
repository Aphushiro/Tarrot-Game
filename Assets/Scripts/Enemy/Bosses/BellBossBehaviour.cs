using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BellBossBehaviour : MonoBehaviour
{
    public GameObject bellEnemy;
    private Transform target;

    float minionTimerStart = 0.2f;
    float minionTimer = 0.2f;

    List<GameObject> spawnedMinions = new List<GameObject>();

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10) {
            SummonBellsHells();
        }
    }

    private void Update()
    {
        if (minionTimer >= 0f)
        {
            minionTimer -= Time.deltaTime;
        }
    }

    public void ClearBellSpawn ()
    {
        for (int i = 0; i < spawnedMinions.Count;  ++i)
        {
            Destroy(spawnedMinions[i]);
        }
        spawnedMinions.Clear();
    }

    private void SummonBellsHells()
    {

        if (minionTimer > 0f)
        {
            return;
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        float bossRadius = GetComponent<CircleCollider2D>().radius;
        Vector2 pos = (Vector2)(target.position - transform.position).normalized * bossRadius;

        // First spawn
        /*FireShot fireShot = GetComponent<FireShot>();
        fireShot.SetBulletSpawnPos(pos);
        */

        GameObject bulletSpawn = Instantiate(bellEnemy, (Vector2)transform.position + pos, Quaternion.identity);
        bulletSpawn.GetComponent<BounceMovement>().SetDirection(pos);
        spawnedMinions.Add(bulletSpawn);
        
        minionTimer = minionTimerStart;
    }
}
