using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShipController : MonoBehaviour
{

    float speed;
    float projectileSpeed;
    float projectileSpeed2;

    public static MotherShipController motherShipController;

    public GameObject enemyProjectile;
    public GameObject enemyProjectilePosition;
    public GameObject enemyProjectilePosition2;

    public GameObject Explosion; 

    float fireInterval = 1f;
    float waitToFire;

    [SerializeField] private float motherShipHealth = 2000f;

    private bool introFinished;
    private bool goingRightP1;
    private bool goingRightP2;

    public bool motherShipDead = false;

    // Start is called before the first frame update
    void Start()
    {
        speed = -3f;
        projectileSpeed = -2f;
        projectileSpeed2 = 2f;
        waitToFire = fireInterval;
        introFinished = false;
        goingRightP1 = false;
        goingRightP2 = true;
        motherShipDead = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log("going right p1 is " + goingRightP1);
        //Debug.Log("going right p2 is " + goingRightP2);
        Debug.Log("Boss Health is " + motherShipHealth);

        waitToFire -= Time.deltaTime;

        if(waitToFire <= 0)
        {
            FireProjectile();
            waitToFire = fireInterval;
        }

        if (introFinished == false)
        {
            StartCoroutine("startIntro");
        }
        else
        {
            ProjectilePositionMovement();
        }

        

    }

    void FireProjectile()
    {
        GameObject enemyprojectile = (GameObject)Instantiate(enemyProjectile);

        enemyprojectile.transform.position = enemyProjectilePosition.transform.position;
    }


    void ProjectilePositionMovement()
    {
        //enemyProjectilePosition.transform.position = new Vector2(enemyProjectilePosition.transform.position.x + speed * Time.deltaTime, enemyProjectilePosition.transform.position.y);

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x = max.x - .5f;
        min.x = min.x + .5f;


        if (goingRightP1)
        {
            if (enemyProjectilePosition.transform.position.x < max.x)
            {
                //Debug.Log("going left");
                //Debug.Log("Projectile Speed is " + projectileSpeed);
                enemyProjectilePosition.transform.position = new Vector2(enemyProjectilePosition.transform.position.x + projectileSpeed * Time.deltaTime, enemyProjectilePosition.transform.position.y);
            }
            else
            {
                goingRightP1 = false;
                projectileSpeed = -2f;
            }
        }
        else if (!goingRightP1)
        {

            if (enemyProjectilePosition.transform.position.x > min.x)
            {
                enemyProjectilePosition.transform.position = new Vector2(enemyProjectilePosition.transform.position.x + projectileSpeed * Time.deltaTime, enemyProjectilePosition.transform.position.y);
            }
            else
            {
                //Debug.Log("going right");
                goingRightP1 = true;
                projectileSpeed = 2f;
            }

        }

        if (goingRightP2)
        {

            if (enemyProjectilePosition2.transform.position.x < max.x)
            {
                //Debug.Log("going right");
                //Debug.Log("Projectile Speed is " + projectileSpeed);
                enemyProjectilePosition2.transform.position = new Vector2(enemyProjectilePosition2.transform.position.x + projectileSpeed2 * Time.deltaTime, enemyProjectilePosition2.transform.position.y);
            }
            else
            {
                goingRightP2 = false;
                projectileSpeed2 = -2f;
            }
        }
        else if (!goingRightP2)
        {

            if (enemyProjectilePosition2.transform.position.x > min.x)
            {
                enemyProjectilePosition2.transform.position = new Vector2(enemyProjectilePosition2.transform.position.x + projectileSpeed2 * Time.deltaTime, enemyProjectilePosition2.transform.position.y);
            }
            else
            {
                //Debug.Log("going right");
                goingRightP2 = true;
                projectileSpeed2 = 2f;
            }

        }

        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "playerProjectile")
        {
            motherShipHealth -= 200f;

            if (motherShipHealth > 0)
            {
                Vector2 expos = other.transform.position;
                GameObject explosion = (GameObject)Instantiate(Explosion);
                explosion.transform.position = expos;
                GameController.gameController.PlayExplode();
                Destroy(other.gameObject);
            }
            else
            {
                Vector2 expos = other.transform.position;
                GameObject explosion = (GameObject)Instantiate(Explosion);
                explosion.transform.position = expos;
                GameController.gameController.PlayExplode();
                PlayerStats.playerStats.UpdateMotherShipScore();
                motherShipDead = true;
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
        }
    }


    IEnumerator startIntro()
    {
        Vector2 position = transform.position;

        position = new Vector2(position.x, position.y + speed * Time.deltaTime);
        transform.position = position;
        yield return new WaitForSeconds(1.3f);
        introFinished = true;
    }

    public void setMotherShipHealth(float health)
    {
        motherShipHealth = health;

    }


}
