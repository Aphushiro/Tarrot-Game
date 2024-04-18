using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    //this code is based on https://www.youtube.com/watch?v=_Z1t7MNk0c4

    public float retreatSpeed;


    private Transform target;
    EnemyFollow movement;
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        movement = GetComponent<EnemyFollow>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float playerDist = Vector2.Distance(rb.position, target.position);
        if (playerDist < movement.nextWaypointDistance)
        {
            rb.AddForce(-movement.force * retreatSpeed);
        }
    }

    
}
