using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarotPowers : MonoBehaviour
{
    public static TarotPowers Instance;

    [SerializeField] GameObject manaOrbObj;
    PlayerStats plaSta;
    Transform playerTransform;

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
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        plaSta = PlayerStats.Instance;
    }

    public void TheFoolTarot () //Spawn mana orbs.
    {
        int manaToDrop = 30;
        for (int i = 0; i < manaToDrop; i++)
        {
            float randomDir = Random.Range(0, Mathf.PI * 2);
            float ranX = Mathf.Sin(randomDir);
            float ranY = Mathf.Cos(randomDir);
            Vector2 dir = new Vector2(ranX, ranY).normalized;


            GameObject orb = Instantiate(manaOrbObj, (Vector2)playerTransform.position + dir, Quaternion.identity);
            orb.GetComponent<Rigidbody2D>().AddForce(dir * 1f, ForceMode2D.Impulse);
        }
    }

    public void TheMagicianTarot () //Your wand attacks can pierce for 10 seconds.
    {
        plaSta.SnapshotPlayerStats(10f);
        plaSta.wandOrbsPierce = true;
    }

    public void ThePriestessTarot () //You heal 50% of your max HP.
    { 
        plaSta.HealDamage(plaSta.maxHealth * plaSta.highPriestessHealPercent);
    }

    public void TheEmpressTarot () //Enemies’ movement (and attacks) are significantly slowed for 10 seconds.
    {
        // I really should've added speed to EnemyStats... Oh well
        BounceMovement[] bounceM = FindObjectsOfType<BounceMovement>();
        EnemyFollow[] enemyF = FindObjectsOfType<EnemyFollow>();

        foreach (BounceMovement bell in bounceM)
        {
            bell.constantMagnitute *= 0.5f;
        }
        foreach (EnemyFollow hugs in enemyF)
        {
            hugs.speed *= 0.5f;
        }

        StartCoroutine(ResetEmpress(10f));
    }

    IEnumerator ResetEmpress (float time)
    {
        yield return new WaitForSeconds(time);
        
        BounceMovement[] bounceM = FindObjectsOfType<BounceMovement>();
        EnemyFollow[] enemyF = FindObjectsOfType<EnemyFollow>();
        foreach (BounceMovement bell in bounceM)
        {
            bell.constantMagnitute *= 2f;
        }
        foreach (EnemyFollow hugs in enemyF)
        {
            hugs.speed *= 2f;
        }
    }

    public void TheEmperorTarot () //You gain more damage and attack rate.
    {
        plaSta.SnapshotPlayerStats(10f);
        plaSta.SwordDamageIncrease(plaSta.emperorSwordDmgBuffPercent);
    }

    public void TheHierophantTarot () //You push all enemies away
    {

    }
}
