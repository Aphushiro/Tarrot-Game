using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public HealthBar healthbar;
    public CupManaBar cupManaBar;

    public int maxHealth = 100;
    public int currentHealth = 100;

    // Stat upgrades
    public float currentMana = 0f;
    public float maxMana = 1f;

    WeaponParentScript wpn;

    public int maxTokens = 1;
    public int curTokens = 0;

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        healthbar.SetHealth(currentHealth);

        cupManaBar.SetMaxMana(maxMana);
        curTokens = maxTokens;
    }

    public void GainMana (float toGain)
    {
        currentMana += toGain;
        currentMana += Mathf.Clamp(currentMana, 0, maxMana);
        cupManaBar.SetMana(currentMana);
    }

    public void ExpendMana (float toExpend)
    {
        currentMana += toExpend;
        currentMana += Mathf.Clamp(currentMana, 0, maxMana);
        cupManaBar.SetMana(currentMana);
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
