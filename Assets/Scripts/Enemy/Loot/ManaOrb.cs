using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaOrb : MonoBehaviour
{
    Transform playerPos;
    Rigidbody2D rb;
    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Vector2 dir = (Vector2)(playerPos.position - rb.transform.position).normalized;
        rb.AddForce(dir * 5f, ForceMode2D.Force);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            PlayerStats.Instance.GainMana(0.03f);
            Destroy(gameObject);
        }
    }
}
