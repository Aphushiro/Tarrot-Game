using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FireShot : MonoBehaviour
{
    public float startTimeBtwShots;
    private float timeBetweenShots;
    [SerializeField] GameObject bullet;

    public Vector2 bulletSpawnPos;


    private void Start()
    {
        timeBetweenShots = startTimeBtwShots;
        bulletSpawnPos = transform.position;
    }

    void Update()
    {
        // Shoot bullets
        if (timeBetweenShots <= 0)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            timeBetweenShots = startTimeBtwShots;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }
    }



    public void SetBulletSpawnPos (Vector2 newPos)
    {
        bulletSpawnPos = newPos;
    }
}
