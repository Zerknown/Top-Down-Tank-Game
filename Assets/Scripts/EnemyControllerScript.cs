using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerScript : MonoBehaviour
{

    float speed;
    public GameObject enemyProjectile;
    public GameObject enemyProjectilePosition;
    float fireInterval = 1f;
    float waitToFire;

    // Start is called before the first frame update
    void Start()
    {
        speed = -3f;
        waitToFire = fireInterval;
    }

    // Update is called once per frame
    void Update()
    {

        waitToFire -= Time.deltaTime;

        if (waitToFire <= 0)
        {
            FireProjectile();
            waitToFire = fireInterval;
        }

        Vector2 position = transform.position;

        position = new Vector2(position.x, position.y + speed * Time.deltaTime);

        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    void FireProjectile()
    {
        GameObject enemyprojectile = (GameObject)Instantiate(enemyProjectile);
        enemyprojectile.transform.position = enemyProjectilePosition.transform.position;
    }
}
