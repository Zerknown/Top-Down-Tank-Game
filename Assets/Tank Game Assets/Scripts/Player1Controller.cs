using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{

    public static Player1Controller player1Controller;

    public TankBullet tankBullet;
    public float speed;
    
    [SerializeField] private GameObject Projectile;

    [SerializeField] private GameObject tankBulletUp;
    [SerializeField] private GameObject tankBulletDown;
    [SerializeField] private GameObject tankBulletLeft;
    [SerializeField] private GameObject tankBulletRight;

    [SerializeField] private GameObject projectilePosition;
    float fireInterval = .5f;
    float nextFire;

    [SerializeField] private float playerHealth = 400f;

    [SerializeField] private GameObject Explosion;

    [SerializeField] private AudioSource fire;
    [SerializeField] private AudioSource powerUpSound;

    [SerializeField] private GameObject shield;

    [SerializeField] private float shieldHealth = 400f;

    private bool isShielded = false;

    private bool hasUpgradedAttack = false;

    [SerializeField] private bool isGameOver = false;
    [SerializeField] private GameObject RespawnPoint;

    [SerializeField] private Color c;

    [SerializeField] private GameObject getShieldPowerUp;
    [SerializeField] private GameObject getExtraLife;
    [SerializeField] private GameObject getGhostUpgrade;

    private Animator anim;
    private SpriteRenderer sprite;
    private float dirX = 0f;

    private bool Upward;
    private bool Downward;
    private bool Leftward;
    private bool Rightward; 

    private void Awake()
    {
        if (player1Controller == null)
        {
            player1Controller = this;
        }
        else if (player1Controller != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        nextFire = fireInterval;
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        c = sprite.material.color;

        //tankBullet.testing();



        Debug.Log(TankGameController.tankGameController);

    }

    // Update is called once per frame
    void Update()
    {
        //tankBullet.tankBulletController.testing();
        dirX = Input.GetAxisRaw("Horizontal");
        //Debug.Log("dirX is " + dirX);
        nextFire -= Time.deltaTime;

        //GameObject projectile = (GameObject)Instantiate(Projectile);

        //projectile.transform.position = projectilePosition.transform.position;
        //projectile.transform.localRotation = Quaternion.Euler(0, 0, 90);
        //tankBullet.SetXSpeed(3f);
        //tankBullet.SetYSpeed(0f);

        //projectilePosition.transform.localRotation = Quaternion.Euler(0, 0, -90);

        //player fires a projectile when space is pressed

        if (Input.GetKeyDown("space") && nextFire <= 0)
        {
            //if (hasUpgradedAttack)
            //{
            //    GameObject projectile = (GameObject)Instantiate(Projectile);
            //    GameObject projectile2 = (GameObject)Instantiate(Projectile);

            //    projectile.transform.position = new Vector3(projectilePosition.transform.position.x + .5f, projectilePosition.transform.position.y, projectilePosition.transform.position.z);
            //    projectile2.transform.position = new Vector3(projectilePosition.transform.position.x - .5f, projectilePosition.transform.position.y, projectilePosition.transform.position.z);
            //}
            //else
            //{
            //    GameObject projectile = (GameObject)Instantiate(Projectile);
            //    projectile.transform.position = projectilePosition.transform.position;
            //}

            fire.Play();

            if (Leftward)
            {
                
                GameObject bulletLeft = (GameObject)Instantiate(tankBulletLeft);

                bulletLeft.transform.position = projectilePosition.transform.position;
                //projectile.transform.localRotation = Quaternion.Euler(0, 0, -90);

                //projectile.               
               
            }
            else if (Rightward)
            {
                GameObject bulletRight = (GameObject)Instantiate(tankBulletRight);

                bulletRight.transform.position = projectilePosition.transform.position;
                //projectile.transform.localRotation = Quaternion.Euler(0, 0, 90);
                
            }
            else if (Upward)
            {

                GameObject bulletUp = (GameObject)Instantiate(tankBulletUp);

                bulletUp.transform.position = projectilePosition.transform.position;
                //projectile.transform.localRotation = Quaternion.Euler(0, 0, 180);

            }
            else
            {
                GameObject bulletDown = (GameObject)Instantiate(tankBulletDown);

                bulletDown.transform.position = projectilePosition.transform.position;
                //projectile.transform.localRotation = Quaternion.Euler(0, 0, 0);


            }




            nextFire = fireInterval;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (x > .1f)
        {
            goRight(x);
        }
        else if(x < -.1f)
        {
            goLeft(x);
        }

        if (y > .1f)
        {
            goUp(y);
        }
        else if (y < -.1f)
        {
            goDown(y);
        }

        Vector2 direction = new Vector2(x, y).normalized;
        //Debug.Log("direction is" + direction);
        Move(direction);

        //Debug.Log("x is " + x);
        //UpdateAnimationState();


    }

    void Move(Vector2 direction)
    {
        //x,y position
        //0,0 is bottom left
        //1,1 is top left

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //keep player within screen boundaries
        //max.x is the farthest your object can go to the right
        //min.x is the farthest your object can go to the left
        max.x = max.x - .5f;
        min.x = min.x + .5f;

        //max.y is the farthest your object can go to the top
        //min.y is the farthest your object can go to the bottom
        max.y = max.y - .5f;
        min.y = min.y + .5f;

        Vector2 pos = transform.position;

        pos += direction * speed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemySide" || other.tag == "enemyProjectile")
        {

            Debug.Log("Hit");
            TankGameController.tankGameController.PlayExplode();

            
            Vector2 expos = transform.position; //explosion effect is 
                                                //positioned in the location where
                                                //player has collided with the 
                                                //enemy or hit by its projectile

            if (isShielded == true)
            {
                if (shieldHealth > 0)
                {
                    if (other.tag == "enemyProjectile")
                    {
                        shieldHealth -= 200;
                    }

                    if (other.tag == "Enemy")
                    {
                        shieldHealth -= 400;
                    }

                }
                else
                {
                    isShielded = false;
                    shield.SetActive(false);
                }
            }
            else
            {
                GameObject explosion = (GameObject)Instantiate(Explosion);
                explosion.transform.position = expos;

                //PlayerStats.playerStats.playerLife--;
                TankPlayerStats.tankPlayerStats.P1Life--;
                //playerHealth = 400f;
                //TankPlayerStats.tankPlayerStats.enemyTanksLeft--;
                //TankPlayerStats.tankPlayerStats.UpdateP1Score();
                TankGameController.tankGameController.PlayExplode();

                if (TankPlayerStats.tankPlayerStats.P1Life > 0)
                {
                    transform.position = RespawnPoint.transform.position;
                    StartCoroutine("GetInvulnerable");
                }
                else
                {
                    //TankGameController.tankGameController.GameOver();
                    isGameOver = true;
                    Destroy(gameObject);
                }
            }


            if(other.tag == "enemyProjectile")
            {
                Destroy(other.gameObject);
            }
            //Destroy(other.gameObject);


        }

        if (other.tag == "Shield")
        {
            //Debug.Log("Shield powerup got hit");
            //if (ShieldController.shieldController.isShielded == false)
            //{



            //Debug.Log("Shield health is " + ShieldController.shieldController.shieldHealth);
            //Debug.Log("Hello World");
            //ShieldController.shieldController.isShielded = true;
            //ShieldController.shieldController.shieldHealth = 400f;
            powerUpSound.Play();
            StartCoroutine("GetShieldText");
            shieldHealth = 400f;
            shield.SetActive(true);
            isShielded = true;

            //}
            //shield.SetActive(true);
            Destroy(other.gameObject);

        }

        if (other.tag == "ExtraLife")
        {
            Debug.Log("Extra Life Hit");

            powerUpSound.Play();
            StartCoroutine("GetLifeText");
            if (TankPlayerStats.tankPlayerStats.P1Life < 3)
            {
                TankPlayerStats.tankPlayerStats.P1Life++;
                Destroy(other.gameObject);
            }
            else
            {
                Destroy(other.gameObject);
            }
        }

        if (other.tag == "Ghost")
        {

            powerUpSound.Play();
            StartCoroutine("GetGhostText");
            if (!hasUpgradedAttack)
            {
                StartCoroutine("GhostPowerUp");
            }

            Destroy(other.gameObject);
        }
    }

    private void goUp(float direction) {

        if (!Upward)
        {
            Upward = true;
            Downward = false;
            Leftward = false;
            Rightward = false;
            transform.localRotation = Quaternion.Euler(0, 0, 180);
            //projectilePosition.transform.localRotation = Quaternion.Euler(0, 0, 180);
            //tankBullet.changeBulletSpeedAndTrajectory(3f, direction, "y");


        }
        else
        {

        }
    }

    private void goDown(float direction)
    {
        if (!Downward)
        {
            Downward = true;
            Upward = false;
            Leftward = false;
            Rightward = false;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            //projectilePosition.transform.localRotation = Quaternion.Euler(0, 0, 0);
            //tankBullet.changeBulletSpeedAndTrajectory(-3f, direction, "y");
        }
        else
        {
             
        }

    }

    private void goLeft(float direction)
    {
        if (!Leftward)
        {
            Leftward = true;
            Rightward = false;
            Upward = false;
            Downward = false;
            transform.localRotation = Quaternion.Euler(0, 0, -90);
            //projectilePosition.transform.localRotation = Quaternion.Euler(0, 0, -90);
           // tankBullet.changeBulletSpeedAndTrajectory(-3f, direction, "x");
        }
        else
        {

        }

    }

    private void goRight(float direction)
    {
        if (!Rightward)
        {
            Rightward = true;
            Leftward = false;
            Upward = false;
            Downward = false;
            transform.localRotation = Quaternion.Euler(0, 0, 90);
            //projectilePosition.transform.localRotation = Quaternion.Euler(0, 0, 90);
            //tankBullet.changeBulletSpeedAndTrajectory(3f, direction, "x");
        }
        else
        {

        }

    }


    IEnumerator GetInvulnerable()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
        c.a = 0.5f;
        sprite.material.color = c;

        yield return new WaitForSeconds(2f);

        Physics2D.IgnoreLayerCollision(8, 9, false);
        c.a = 1f;
        sprite.material.color = c;
    }

    IEnumerator GhostPowerUp()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
        c.a = 0.5f;
        sprite.material.color = c;

        yield return new WaitForSeconds(5f);

        Physics2D.IgnoreLayerCollision(8, 9, false);
        c.a = 1f;
        sprite.material.color = c;
    }

    IEnumerator GetShieldText()
    {
        getShieldPowerUp.SetActive(true);

        yield return new WaitForSeconds(2f);

        getShieldPowerUp.SetActive(false);
    }

    IEnumerator GetLifeText()
    {
        getExtraLife.SetActive(true);

        yield return new WaitForSeconds(2f);

        getExtraLife.SetActive(false);
    }

    IEnumerator GetGhostText()
    {
        getGhostUpgrade.SetActive(true);

        yield return new WaitForSeconds(2f);

        getGhostUpgrade.SetActive(false);
    }
}
