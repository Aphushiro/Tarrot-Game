using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    Vector2 movement;

    //private WeaponParentScript weaponParent;

    //[SerializeField]
    //private InputActionReference sword, pointerPosition;

    //private void OnEnable()
    //{
    //    sword.action.performed += PerformAttack;
    //}

    //private void OnDisable()
    //{
    //    sword.action.performed -= PerformAttack;
    //}

    //private void PerformAttack(InputAction.CallbackContext obj)
    //{
    //    weaponParent.Attack();
    //}

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    //private Vector2 GetPointerInput()
    //{
    //    Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
    //    mousePos.z = Camera.main.nearClipPlane;
    //    return Camera.main.ScreenToWorldPoint(mousePos);
    //}

}