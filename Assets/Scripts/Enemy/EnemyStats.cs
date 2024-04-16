using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float health;

    void Takedamage (float damage)
    {
        health -= damage;

        if (health <= 0 )
        {
            Die();
        }
    }

    void Die ()
    {
        Destroy(gameObject);
    }
}
