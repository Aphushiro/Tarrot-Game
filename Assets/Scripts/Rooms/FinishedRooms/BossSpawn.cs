using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    public SpriteRenderer bossIcon;
    public float iconFadeTime = 1f;
    float iconFadeVal = 0f;

    bool bossSpawned = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && bossSpawned == false)
        {
            bossSpawned = true;
            StartCoroutine("SpawnBoss");
        }
    }

    IEnumerator SpawnBoss ()
    {
        yield return new WaitForSeconds(iconFadeTime);

        // Instantiate boss
        GameObject levelBoss = GameMng.Instance.GetBossForLevel();
        GameObject boss = Instantiate(levelBoss, transform.position, Quaternion.identity);

        // Cue the heckin' boss music!!!
    }

    private void Update()
    {
        if (bossSpawned == true && iconFadeVal < 1f)
        {
            iconFadeVal += Time.deltaTime/iconFadeTime;
            iconFadeVal = Mathf.Clamp01(iconFadeVal);
            bossIcon.color = new Color(1, 1, 1, iconFadeVal);

        }
    }
}
