using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemyController : MonoBehaviour
{

    float speed;

    [SerializeField] private float upSpeed;
    [SerializeField] private float downSpeed;
    [SerializeField] private float leftSpeed;
    [SerializeField] private float rightSpeed;

    public GameObject enemyProjectile;
    public GameObject enemyProjectilePosition;
    float fireInterval = 1f;
    float waitToFire;

    [SerializeField] private GameObject tankBulletUp;
    [SerializeField] private GameObject tankBulletDown;
    [SerializeField] private GameObject tankBulletLeft;
    [SerializeField] private GameObject tankBulletRight;

    private bool Upward;
    private bool Downward;
    private bool Leftward;
    private bool Rightward;

    private int direction;

    [SerializeField] private float moveInterval = 5f;
    [SerializeField] private float movementInterval = 1f;

    private float waitToMove;
    private float movingTime;

    // Start is called before the first frame update
    void Start()
    {
        speed = -3f;

        upSpeed = speed;
        downSpeed = -speed;
        leftSpeed = -speed;
        rightSpeed = speed;

        waitToFire = fireInterval;
        direction = Random.Range(1, 5);
        waitToMove = moveInterval;
        movingTime = movementInterval;
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x = max.x - .5f;
        min.x = min.x + .5f;

        max.y = max.y - .5f;
        min.y = min.y + .5f;
        
        waitToFire -= Time.deltaTime;

        if (waitToFire <= 0)
        {
            FireProjectile();
            waitToFire = fireInterval;
        }

        waitToMove -= Time.deltaTime;

        if (waitToMove <= 0)
        {
            switch (direction)
            {
                case 1: goUp(); break;
                case 2: goDown(); break;
                case 3: goLeft(); break;
                case 4: goRight(); break;

                default: break;

            }


            movingTime -= Time.deltaTime;


            if(movingTime >= 0)
            {
                if (Upward)
                {
                    
                    Vector2 position = transform.position;

                    if(position.y > min.y && position.y < max.y)
                    {

                        position = new Vector2(position.x, position.y + upSpeed * Time.deltaTime);

                        transform.position = position;
                    }
                    

                }
                else if (Downward)
                {
                    Vector2 position = transform.position;

                    if (position.y > min.y && position.y < max.y)
                    {

                        position = new Vector2(position.x, position.y + downSpeed * Time.deltaTime);

                        transform.position = position;
                    }

                    
                }
                else if (Leftward)
                {
                    Vector2 position = transform.position;

                    if (position.x > min.x && position.x < max.x)
                    {

                        position = new Vector2(position.x + leftSpeed * Time.deltaTime, position.y);

                        transform.position = position;
                    }

                    
                }
                else if (Rightward)
                {
                    Vector2 position = transform.position;


                    if (position.x > min.x && position.x < max.x)
                    {

                        position = new Vector2(position.x + rightSpeed * Time.deltaTime, position.y );

                        transform.position = position;
                    } 
                }

            }
            else
            {
                waitToMove = moveInterval;
                movingTime = movementInterval;
                direction = Random.Range(1, 5);
            }
            
            

        }

        
        

        //Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        if(transform.position.y < min.y)
        {
            //Destroy(gameObject);
        }
    }

    void FireProjectile()
    {
        //GameObject enemyprojectile = (GameObject)Instantiate(enemyProjectile);
        //enemyprojectile.transform.position = enemyProjectilePosition.transform.position;

        if (Downward)
        {
            GameObject enemyProjectileUp = (GameObject)Instantiate(tankBulletUp);
            enemyProjectileUp.transform.position = enemyProjectilePosition.transform.position;

        }else if (Upward)
        {
            GameObject enemyProjectileDown = (GameObject)Instantiate(tankBulletDown);
            enemyProjectileDown.transform.position = enemyProjectilePosition.transform.position;
        }
        else if (Rightward)
        {
            GameObject enemyProjectileLeft = (GameObject)Instantiate(tankBulletLeft);
            enemyProjectileLeft.transform.position = enemyProjectilePosition.transform.position;
        }
        else if (Leftward)
        {
            GameObject enemyProjectileRight = (GameObject)Instantiate(tankBulletRight);
            enemyProjectileRight.transform.position = enemyProjectilePosition.transform.position;
        }
    }

    private void goUp()
    {
        if (!Upward)
        {
            Upward = true;
            Downward = false;
            Leftward = false;
            Rightward = false;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void goDown()
    {
        if (!Downward)
        {
            Downward = true;
            Upward = false;
            Leftward = false;
            Rightward = false;
            transform.localRotation = Quaternion.Euler(0, 0, 180);
        }
    }

    private void goLeft()
    {
        if (!Leftward)
        {
            Leftward = true;
            Rightward = false;
            Upward = false;
            Downward = false;
            transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
    }

    private void goRight()
    {
        if (!Rightward)
        {
            Rightward = true;
            Leftward = false;
            Upward = false;
            Downward = false;
            transform.localRotation = Quaternion.Euler(0, 0, -90);
        }
    }
}
