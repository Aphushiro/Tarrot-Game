using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public HealthBar healthbar;
    public CupManaBar cupManaBar;
    public PentacleVis pentacleVis;

    public int maxHealth = 100;
    public int currentHealth = 100;

    // Stat upgrades
    public float currentMana = 0f;
    public float maxMana = 1f;

    WeaponParentScript wpn;

    public int maxPentacles = 1;
    public int curPentacles = 0;

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
        currentMana = cupManaBar.slider.value;
        cupManaBar.SetMana(cupManaBar.slider.value);

        curPentacles = maxPentacles;
        pentacleVis.UpdatePentacles(curPentacles);
    }

    public void GainMana (float toGain)
    {
        currentMana += toGain;
        currentMana = Mathf.Clamp(currentMana, 0, maxMana);
        Debug.Log(currentMana);
        cupManaBar.SetMana(currentMana);
    }

    public bool ExpendMana (float toExpend)
    {
        if (toExpend > currentMana)
        {
            return false;
        }
        currentMana -= toExpend;
        currentMana = Mathf.Clamp(currentMana, 0, maxMana);
        cupManaBar.SetMana(currentMana);
        return true;
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

    public int DepositPentacles (int toDep)
    {
        if (toDep > curPentacles)
        {
            return 0;
        } else
        {
            curPentacles -= toDep;
            pentacleVis.UpdatePentacles(curPentacles);
            return toDep;
        }
    }
    public void ResetPentacles ()
    {
        curPentacles = maxPentacles;
        pentacleVis.UpdatePentacles(curPentacles);
    }

    public void AddMaxPentacles ()
    {
        maxPentacles++;
    }

    public void RespawnPlayer ()
    {
        ResetPentacles();
    }

    public void RestartPosition ()
    {
        transform.position = Vector2.zero;
    }
}
