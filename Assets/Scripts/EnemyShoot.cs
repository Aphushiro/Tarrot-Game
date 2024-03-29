using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    //this code is based on https://www.youtube.com/watch?v=_Z1t7MNk0c4

    public float speed;
    public float stopDistance;
    public float retreatSpeed;
    public float retreatDistance;

    private float timeBetweenShots;
    public float startTimeBtwShots;

    private Transform target;

    [SerializeField] GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        timeBetweenShots = startTimeBtwShots;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, target.position) < stopDistance && Vector2.Distance(transform.position, target.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, target.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, -retreatSpeed * Time.deltaTime);
        }

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
}
