using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float health;
    float damageTimer = 1f;

    bool damageBlocked = false;


    public void Takedamage (float damage)
    {
        if (damageBlocked) { return; }

        health -= damage;

        if (health <= 0 )
        {
            Die();
        }

        damageBlocked = true;
        StartCoroutine(TookDamage());
    }

    IEnumerator TookDamage ()
    {
        yield return new WaitForSeconds(damageTimer);
        damageBlocked = false;
    }

    void Die ()
    {
        Destroy(gameObject);
    }
}
