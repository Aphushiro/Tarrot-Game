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

    public void SnapshotPlayerStats (int time)
    {
        int mH = maxHealth;
        int cH = currentHealth;

        float mM = maxMana;
        float cM = currentMana;

        int maxP = maxPentacles;
        int curP = curPentacles;

        float wepDmg = wpn.swordDamage;
        float wepDel = wpn.swordDelay;

        float wanDmg = wpn.wandDamage;
        float wanDel = wpn.wandDelay;

        StartCoroutine(ResetPlayerStats(time, mH, cH, mM, cM, maxP, curP, wepDmg, wepDel, wanDmg, wanDel));
    }



    private IEnumerator ResetPlayerStats (int sec, int mH, int cH, float mM, float cM, int maxP, int curP, float wepDmg, float wepDel, float wanDmg, float wanDel)
    {
        yield return new WaitForSeconds (sec);

        maxHealth = mH;
        currentHealth = cH;

        currentMana = cM;
        maxMana = mM;

        maxPentacles = maxP;
        curPentacles = curP;

        wpn.swordDamage = wepDmg;
        wpn.swordDelay = wepDel;

        wpn.wandDamage = wanDmg;
        wpn.wandDelay = wanDel;
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
