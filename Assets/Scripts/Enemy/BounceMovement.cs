using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BounceMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [HideInInspector]
    public Vector2 direction;
    public float speed = 2f;
    [HideInInspector] public float constantMagnitute;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        float randomDir = Random.Range(0, Mathf.PI * 2);
        float ranX = Mathf.Sin(randomDir);
        float ranY = Mathf.Cos(randomDir);
        direction = new Vector2 (ranX, ranY).normalized;
        rb.AddForce (direction*speed, ForceMode2D.Impulse);

        constantMagnitute = rb.velocity.magnitude;
        rb.AddTorque(0.5f, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = rb.velocity.normalized * constantMagnitute;
    }

    public void SetDirection (Vector2 newDir)
    {
        rb.velocity = newDir.normalized * constantMagnitute;
    }
}
