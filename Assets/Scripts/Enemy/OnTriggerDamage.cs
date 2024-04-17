using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerDamage : MonoBehaviour
{
    float damagetimer = 0f;
    public float damage;

    private void Update()
    {
        if (damagetimer > 0f)
        {
            damagetimer -= Time.deltaTime;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player") && damagetimer <= 0f)
        {

            PlayerStats player = other.transform.GetComponent<PlayerStats>();
            player.TakeDamage(damage);
            damagetimer += 1f;
        }
    }
}
