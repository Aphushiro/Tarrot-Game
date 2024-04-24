using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOrb : MonoBehaviour
{
    bool piercing = false;
    [HideInInspector]
    public float damage;

    private void Start()
    {
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        piercing = PlayerStats.Instance.wandOrbsPierce;
        if (other.transform.CompareTag("Enemy")) {
            other.GetComponent<EnemyStats>().Takedamage(damage, transform.position);

            // If piercing, the bolt should continue
            if (piercing) { return; }
            Destroy(gameObject);
        }

        
    }
}
