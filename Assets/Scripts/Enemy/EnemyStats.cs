using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyStats : MonoBehaviour
{
    public float health;
    float damageTimer = .4f;

    bool damageBlocked = false;
    public float knockBackAmount = 5f;

    public UnityEvent onDeath;

    // Loot section
    public GameObject manaOrbObj;
    public int manaToDrop = 1;

    public void Takedamage (float damage, Vector3 sourcePos)
    {
        if (damageBlocked) { return; }

        if (gameObject.GetComponent<Rigidbody2D>() != null )
        {
            health -= damage;
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

            Vector2 dir = (Vector2)(transform.position - sourcePos).normalized * knockBackAmount;
            rb.AddForce(dir, ForceMode2D.Impulse);
        }

        if (health <= 0)
        {
            Die();
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

    private void DropLoot ()
    {
        for (int i = 0; i < manaToDrop; i++)
        {
            GameObject orb = Instantiate(manaOrbObj, transform.position, Quaternion.identity);
            float randomDir = Random.Range(0, Mathf.PI * 2);
            float ranX = Mathf.Sin(randomDir);
            float ranY = Mathf.Cos(randomDir);
            Vector2 dir = new Vector2(ranX, ranY).normalized;

            orb.GetComponent<Rigidbody2D>().AddForce(dir * 4f, ForceMode2D.Impulse);
        }
    }

    void Die ()
    {
        DropLoot();
        onDeath.Invoke();
        Destroy(gameObject);
    }
}
