using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    public UnityEvent OnDeath;
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
    float swordModifier = 1.0f;
    float swordUpAmount = 0.1f;

    float wandModifier = 1.0f;
    float wandUpAmount = 0.1f;

    public int maxPentacles = 1;
    public int curPentacles = 0;

    // Stat upgrades count
    int cupUps = 0;
    int swordUps = 0;
    int pentacleUps = 0;
    int wandUps = 0;

    // Tarot upgrades
    public bool wandOrbsPierce = false;                 // Magician
    public float highPriestessHealPercent = 0.5f;       // High Priestess
    public float empressSlowPercent = 0.1f;             // The Empress
    public float emperorSwordDmgBuffPercent = 0.5f;     // The Emperor
    public float hierophantPushAmount = 50f;            // The Hierophant

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
        wpn = FindObjectOfType<WeaponParentScript>();
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        healthbar.SetHealth(currentHealth);

        cupManaBar.SetMaxMana(maxMana);
        currentMana = cupManaBar.slider.value;
        cupManaBar.SetMana(cupManaBar.slider.value);

        curPentacles = maxPentacles;
        pentacleVis.UpdatePentacles(curPentacles);
    }

    public void SnapshotPlayerStats (float time)
    {
        /*
        int mH = maxHealth;
        int cH = currentHealth;

        float mM = maxMana;
        float cM = currentMana;

        int maxP = maxPentacles;
        int curP = curPentacles;
        */
        float wepDmg = wpn.swordDamage;
        float wepDel = wpn.swordDelay;

        float wanDmg = wpn.wandDamage;
        float wanDel = wpn.wandDelay;
        bool wanPier = wandOrbsPierce;

        StartCoroutine(ResetPlayerStats(time, /*mH, cH, mM, cM, maxP, curP,*/ wepDmg, wepDel, wanDmg, wanDel, wanPier));
    }

    private IEnumerator ResetPlayerStats (float sec, /*int mH, int cH, float mM, 
        float cM, int maxP, int curP,*/ float wepDmg, float wepDel, float wanDmg, 
        float wanDel, bool wanPier)
    {
        yield return new WaitForSeconds (sec);
        /*
        maxHealth = mH;
        currentHealth = cH;

        currentMana = cM;
        maxMana = mM;

        maxPentacles = maxP;
        curPentacles = curP;
        */
        wpn.swordDamage = wepDmg;
        wpn.swordDelay = wepDel;

        wpn.wandDamage = wanDmg;
        wpn.wandDelay = wanDel;
        wandOrbsPierce = wanPier;

        UpdatePostStatUpgrade();
    }

    public void GainMana (float toGain)
    {
        currentMana += toGain;
        currentMana = Mathf.Clamp(currentMana, 0, maxMana);
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
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void HealDamage(float damage)
    {
        int intDmg = Mathf.CeilToInt(damage);

        currentHealth += intDmg;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthbar.SetHealth(currentHealth);
    }

    public void SwordDamageIncrease (float percent)
    {
        // Should only trigger after a snapshot has been made
        wpn.swordDamage += wpn.swordDamage * percent;
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

    public bool UpgradeStat(int toUpgrade)
    {
        switch (toUpgrade)
        {
            case 0:
                if (cupUps == 13) { return false; }
                cupUps++; 
                UpdatePostStatUpgrade();
                return true;

            case 1:
                if (swordUps == 13) { return false; }
                swordUps++;
                UpdatePostStatUpgrade();
                return true;

            case 2:
                if (pentacleUps == 13) { return false; }
                pentacleUps++;
                UpdatePostStatUpgrade();
                return true;

            case 3:
                if (wandUps == 13) { return false; }
                wandUps++;
                UpdatePostStatUpgrade();
                return true;

            default:
                return false;
        }

    }

    public void UpdatePostStatUpgrade ()
    {
        // Mana
        maxMana = 1 + cupUps;
        cupManaBar.SetMaxMana(maxMana);
        // Sword
        swordModifier = 1.0f + (swordUpAmount * swordUps);
        wpn.swordDamage = wpn.baseDamage * swordModifier;
        // Pentacles
        maxPentacles = 1 + pentacleUps;
        pentacleVis.UpdatePentacles(curPentacles);
        // Wand
        wandModifier = 1.0f + (wandUpAmount * wandUps);
        wpn.wandDamage = wpn.baseDamage * wandModifier;
    }


    // Death or other meta stuff ------
    public void Die()
    {
        OnDeath.Invoke();
        StartCoroutine(ResetWorld());
    }

    IEnumerator ResetWorld()
    {
        // Hard coded to match load screen time
        yield return new WaitForSeconds(2f);
        RespawnPlayer();
    }

    public void RespawnPlayer ()
    {
        ResetPentacles();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = Vector2.zero;

        cupUps = 0;
        swordUps = 0;
        pentacleUps = 0;
        wandUps = 0;


        HealDamage(maxHealth);
        maxMana = 1f;
        ExpendMana(currentMana);
        wpn.swordDamage = wpn.baseDamage;
        wpn.wandDamage = wpn.baseDamage;
        maxPentacles = 1;
        ResetPentacles();
    }
}
