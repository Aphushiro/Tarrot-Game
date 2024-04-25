using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool canMove = false;
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    Vector2 movement;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (canMove == false) { return; }
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    public void ToggleCanMove()
    {
        canMove = !canMove;
    }
}