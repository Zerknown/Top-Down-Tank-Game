using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBullet : MonoBehaviour
{
    
    [SerializeField] private float xSpeed = 0;
    [SerializeField] private float ySpeed = 0;

    [SerializeField] private GameObject Explosion;

    [SerializeField] private bool Player1Bullet;
    [SerializeField] private bool Player2Bullet;
    [SerializeField] private bool EnemyBullet;

    private string axisCurrent;

    public TankBullet tankBulletController;

    private void Awake()
    {
        
        if (tankBulletController == null)
        {
            
            tankBulletController = this;
           
        }
        else if(tankBulletController != this)
        {
            Destroy(gameObject);
        }

        //Debug.Log(tankBulletController);
    }


    // Start is called before the first frame update
    void Start()
    {
        //xSpeed = 0f;
        //ySpeed = -3f;
    }

    // Update is called once per frame
    void Update()
    {
        //ShootLeft();
        //SetXSpeed(3f);
        //SetYSpeed(0f);

        //Debug.Log(tankBulletController);
            Vector2 position = transform.position;

            position = new Vector2(position.x + xSpeed * Time.deltaTime, position.y + ySpeed * Time.deltaTime); //moves the project upward

            transform.position = position;
            //transform.localRotation = Quaternion.Euler(0, 0, 90);
        //if (axisCurrent == "x")
        //{
        //    Vector2 position = transform.position;

        //    position = new Vector2(position.x + 3 * Time.deltaTime, position.y ); //moves the project upward

        //    transform.position = position;
        //}
        //else if (axisCurrent == "y"){
        //    Vector2 position = transform.position;

        //    position = new Vector2(position.x , position.y + 3 * Time.deltaTime); //moves the project upward

        //    transform.position = position;
        //}


        //changeBulletSpeedAndTrajectory();

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); //transforms position from viewport space into world point


        //if (transform.position.y > max.y)
        //{
        //    Destroy(gameObject); //destroys the projectile if it moved out the screen
        //}
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Player1Bullet)
        {
            if (other.tag == "Enemy")
            {
                TankPlayerStats.tankPlayerStats.enemyTanksLeft--;
                TankPlayerStats.tankPlayerStats.UpdateP1Score();
                TankGameController.tankGameController.PlayExplode();
                Vector2 expos = transform.position;
                GameObject explosion = (GameObject)Instantiate(Explosion);
                explosion.transform.position = expos;
                //GameController.gameController.PlayExplode();
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
        }

        if (Player2Bullet)
        {
            if (other.tag == "Enemy")
            {
                TankPlayerStats.tankPlayerStats.enemyTanksLeft--;
                TankPlayerStats.tankPlayerStats.UpdateP2Score();
                TankGameController.tankGameController.PlayExplode();
                Vector2 expos = transform.position;
                GameObject explosion = (GameObject)Instantiate(Explosion);
                explosion.transform.position = expos;
                //GameController.gameController.PlayExplode();
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
        }

        if (EnemyBullet)
        {
            if(other.tag == "Enemy")
            {

            }
        }

        if(other.tag == "BulletDestroyer")
        {
            Destroy(gameObject);
        }

        if(other.tag == "HomeBase")
        {
            TankGameController.tankGameController.GameOver();
        }
        

        //explosion effect is 
        //positioned in the location where
        //player has collided with the 
        //enemy or hit by its projectile



        if (other.tag == "Box" || other.tag == "enemyProjectile")
        {
            Vector2 expos = transform.position;
            GameObject explosion = (GameObject)Instantiate(Explosion);
            explosion.transform.position = expos;
            //GameController.gameController.PlayExplode();
            TankGameController.tankGameController.PlayExplode();
            Destroy(gameObject);
            Destroy(other.gameObject);
            Debug.Log("Explode");
        }
    }

}
