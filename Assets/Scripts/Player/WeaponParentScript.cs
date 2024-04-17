using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WeaponParentScript : MonoBehaviour
{
    public Animator animator;
    public float delay = 0.3f;
    private bool attackBlocked;

    public Collider2D swordCollider;
    public float playerDamage;


    void Update()
    {
        // Find mousepos
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        if (attackBlocked == false)
        {
            FaceCursor(mousePos);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void FaceCursor (Vector3 target)
    {
        Vector3 diff = target - transform.position; 
        diff.Normalize();
        
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg; 
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    public void Attack()
    {
        // If we're waiting for our attack cooldown
        if (attackBlocked)
            return;

        // When we attack
        animator.SetTrigger("Attack");
        attackBlocked = true;
        swordCollider.enabled = attackBlocked;
    }
    /*
    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }*/


    public void HitEnemy (Collider2D other)
    {

        if (other.CompareTag("Enemy") && other.GetComponent<EnemyStats>() != null)
        {
            Debug.Log(other.name);
            other.GetComponent<EnemyStats>().Takedamage(playerDamage, gameObject.transform.position);
        }
    }


    // Something about activating/deactivating trigger on animationevent...
    public void SwitchAttackBlock (int swap)
    {
        if (swap == 0)
        {
            attackBlocked = false;
            swordCollider.enabled = attackBlocked;
        }
    }

    void StuffOn ()
    {
        //private WeaponParentScript weaponParent;

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


        

    }
}
