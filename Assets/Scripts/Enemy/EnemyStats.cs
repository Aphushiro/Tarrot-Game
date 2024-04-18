using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyStats : MonoBehaviour
{
    public float health;
    float damageTimer = 1f;

    bool damageBlocked = false;
    public float knockBackAmount = 5f;

    public UnityEvent onDeath;

    public void Takedamage (float damage, Vector3 sourcePos)
    {
        if (damageBlocked) { return; }

        if (health <= 0 )
        {
            Die();
        }

        if (gameObject.GetComponent<Rigidbody2D>() != null )
        {
            health -= damage;
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

            Vector2 dir = (Vector2)(transform.position - sourcePos).normalized * knockBackAmount;
            rb.AddForce(dir, ForceMode2D.Impulse);
        }

        if (gameObject.GetComponent<RedAnimation>() != null)
        {
            gameObject.GetComponent<RedAnimation>().ActivateRedTintFeedback();
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
        onDeath.Invoke();
        Destroy(gameObject);
    }
}
