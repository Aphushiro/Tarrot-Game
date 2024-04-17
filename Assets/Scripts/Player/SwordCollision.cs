using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        WeaponParentScript wps = FindObjectOfType<WeaponParentScript>();
        wps.HitEnemy(other);
    }
}
