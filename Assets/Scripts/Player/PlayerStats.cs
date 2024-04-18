using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public HealthBar healthbar;
    public int maxHealth = 100;
    public int currentHealth = 100;

    public int maxTokens = 1;
    public int curTokens = 0;

    private void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        healthbar.SetHealth(currentHealth);
        curTokens = maxTokens;
    }

    public void TakeDamage (float damage)
    {
        int intDmg = Mathf.FloorToInt(damage);
        if (intDmg < 1)
        {
            intDmg = 1;
        }
        currentHealth -= intDmg;
        healthbar.SetHealth(currentHealth);
    }

    public int DepositTokens (int toDep)
    {
        if (toDep > curTokens)
        {
            return 0;
        } else
        {
            curTokens -= toDep;
            return toDep;
        }
    }

    public void RespawnPlayer ()
    {
        curTokens = maxTokens;

    }

    public void RestartPosition ()
    {
        transform.position = Vector2.zero;
    }
}
