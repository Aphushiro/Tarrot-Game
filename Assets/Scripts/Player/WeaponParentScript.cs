using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.GraphicsBuffer;

public class WeaponParentScript : MonoBehaviour
{
    public Sprite[] weaponSprites;
    public GameObject wandBolt;
    public SpriteRenderer weaponRenderer;

    public Animator animator;
    public float swordDelay = 0.35f;

    private bool attackBlocked;
    private bool animDone = true;

    public Collider2D swordCollider;
    float swordColOffsetX;
    public float swordDamage;

    // Wand stats
    public float wandDamage;
    public float wandDelay = 0.5f;

    private void Start()
    {
        swordColOffsetX = swordCollider.offset.x;
        weaponRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        // Find mousepos
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        if (animDone)
        {
            FaceCursor(mousePos);
        }

        if (Input.GetMouseButtonDown(0))
        {
            AttackSword();
        }

        if (Input.GetMouseButtonDown(1))
        {
            WandFire();
        }
    }

    void FaceCursor (Vector3 target)
    {
        Vector3 diff = target - transform.position; 
        diff.Normalize();
        
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        rot_z -= 90f;
        int low = -100, high = -250;

        // Reposition rotation based on cursor, but snap to closest min/max
        if (rot_z < low && rot_z > high) 
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;

            if (mousePos.x > transform.position.x)
            {
                rot_z = low;
            } else
            {
                rot_z = high;
            }
        }
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
    }

    public void AttackSword()
    {
        // Set sword position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x > transform.position.x)
        {
            swordCollider.offset = new Vector2(swordColOffsetX, swordCollider.offset.y);
        }
        else
        {
            swordCollider.offset = new Vector2(-swordColOffsetX, swordCollider.offset.y);
        }

        // If we're waiting for our attack cooldown
        if (attackBlocked)
            return;

        // Swap to sword sprite
        weaponRenderer.sprite = weaponSprites[0];

        // When we attack
        animator.SetTrigger("Attack");
        attackBlocked = true;
        animDone = false;
        swordCollider.enabled = attackBlocked;
        StartCoroutine(DelayAttack(swordDelay));
    }
    
    private IEnumerator DelayAttack(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        attackBlocked = false;
    }

    // Hitting the enemy with the sword collider
    public void HitEnemy (Collider2D other)
    {

        if (other.CompareTag("Enemy") && other.GetComponent<EnemyStats>() != null)
        {
            other.GetComponent<EnemyStats>().Takedamage(swordDamage, gameObject.transform.position);
        }
    }

    public void WandFire ()
    {

        if (attackBlocked)
            return;

        // Swap to sword sprite
        weaponRenderer.sprite = weaponSprites[1];

        // When we attack
        animator.SetTrigger("WandFire");
        attackBlocked = true;
        animDone = false;

        // Instantiate and fire bolt
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector2 boltPos = transform.position + (mousePos - transform.position).normalized;
        Vector2 boltForce = (mousePos - transform.position).normalized;

        GameObject orb = Instantiate(wandBolt, boltPos, Quaternion.identity);
        orb.GetComponent<PlayerOrb>().damage = wandDamage;
        orb.GetComponent<Rigidbody2D>().AddForce(boltForce * 5f, ForceMode2D.Impulse);

        StartCoroutine(DelayAttack(wandDelay));
    }

    // Something about activating/deactivating trigger on animationevent...
    public void SwitchAttackBlock (int swap)
    {
        if (swap == 0)
        {
            animDone = true;
            swordCollider.enabled = false;
        }
    }
}
