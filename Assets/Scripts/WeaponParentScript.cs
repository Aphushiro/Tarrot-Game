using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParentScript : MonoBehaviour
{
    public Animator animator;
    public float delay = 0.3f;
    private bool attackBlocked;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        if (attackBlocked)
            return;
        animator.SetTrigger("Attack");
        attackBlocked = true;
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }
}
