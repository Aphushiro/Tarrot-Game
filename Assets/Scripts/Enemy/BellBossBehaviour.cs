using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellBossBehaviour : MonoBehaviour
{
    public GameObject bellEnemy;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle")) {
            SummonBellsHells();
        }
    }

    private void SummonBellsHells()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        float bossRadius = GetComponent<CircleCollider2D>().radius;
        Vector2 pos = (rb.position + -rb.velocity).normalized * bossRadius;

        // First spawn
        GameObject first = Instantiate(bellEnemy, pos, Quaternion.identity);
        first.GetComponent<BounceMovement>().SetDirection(-rb.velocity);

        // Second and third

    }
}
