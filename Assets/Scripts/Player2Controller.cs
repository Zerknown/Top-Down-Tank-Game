using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{

    public static Player2Controller player2Controller;
    public float speed; //movement speed of the player
    public GameObject Projectile;
    public GameObject projectilePosition;
    float fireInterval = .5f;
    float nextFire;

    [SerializeField] private float playerHealth = 400f;

    public GameObject Explosion;

    public AudioSource fire;

    [SerializeField] private GameObject shield;
    [SerializeField] private float shieldHealth = 400f;
    private bool isShielded = false;

    private bool hasUpgradedAttack = false;

    public bool isGameOver = false;
    public GameObject RespawnPoint;

    public Color c;

    [SerializeField] private GameObject getShieldPowerUp;
    [SerializeField] private GameObject getExtraLife;
    [SerializeField] private GameObject getAttackUpgrade;

    private Animator anim;
    private SpriteRenderer sprite;
    private float dirX = 0f;
    private enum MovementState { front, left, right }


    private void Awake()
    {
        if (player2Controller == null)
        {
            player2Controller = this;
        }
        else if (player2Controller != this)
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
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal2");
        Debug.Log("P2 dirX is " + dirX);
        nextFire -= Time.deltaTime;



        //player fires a projectile when space is pressed

        if (Input.GetKeyDown(KeyCode.Keypad0) && nextFire <= 0)
        {
            if (hasUpgradedAttack)
            {
                GameObject projectile = (GameObject)Instantiate(Projectile);
                GameObject projectile2 = (GameObject)Instantiate(Projectile);

                projectile.transform.position = new Vector3(projectilePosition.transform.position.x + .5f, projectilePosition.transform.position.y, projectilePosition.transform.position.z);
                projectile2.transform.position = new Vector3(projectilePosition.transform.position.x - .5f, projectilePosition.transform.position.y, projectilePosition.transform.position.z);
            }
            else
            {
                GameObject projectile = (GameObject)Instantiate(Projectile);
                projectile.transform.position = projectilePosition.transform.position;
            }

            fire.Play();

            nextFire = fireInterval;
        }

        float x = Input.GetAxisRaw("Horizontal2");
        float y = Input.GetAxisRaw("Vertical2");

        Vector2 direction = new Vector2(x, y).normalized;
        //Debug.Log("direction is" + direction);
        Move(direction);

        //Debug.Log("x is " + x);
        UpdateAnimationState();



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
        if (other.tag == "Enemy" || other.tag == "enemyProjectile")
        {

            Debug.Log("Hit");
            GameController.gameController.PlayExplode();
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

                PlayerStats.playerStats.playerLife--;
                playerHealth = 400f;


                if (PlayerStats.playerStats.playerLife > 0)
                {
                    transform.position = RespawnPoint.transform.position;
                    StartCoroutine("GetInvulnerable");
                }
                else
                {
                    GameController.gameController.GameOver();
                    isGameOver = true;
                    Destroy(gameObject);
                }
            }


            Destroy(other.gameObject);


        }

        if (other.tag == "Shield")
        {
            //Debug.Log("Shield powerup got hit");
            //if (ShieldController.shieldController.isShielded == false)
            //{

            //Debug.Log("Shield health is " + ShieldController.shieldController.shieldHealth);
            StartCoroutine("GetShieldText");
            //ShieldController.shieldController.isShielded = true;
            //ShieldController.shieldController.shieldHealth = 400f;
            shieldHealth = 400f;
            shield.SetActive(true);
            isShielded = true;

            //}
            //shield.SetActive(true);
            Destroy(other.gameObject);

        }

        if (other.tag == "ExtraLife")
        {
            StartCoroutine("GetLifeText");
            if (PlayerStats.playerStats.playerLife < 5)
            {
                PlayerStats.playerStats.playerLife++;
                Destroy(other.gameObject);
            }
            else
            {
                Destroy(other.gameObject);
            }
        }

        if (other.tag == "PowerUp")
        {
            StartCoroutine("GetAttackText");
            if (!hasUpgradedAttack)
            {
                StartCoroutine("FireUpgrade");
            }

            Destroy(other.gameObject);
        }
    }

    private void UpdateAnimationState()
    {
        MovementState state;


        if (dirX > 0f)
        {
            state = MovementState.right;
        }
        else if (dirX < 0f)
        {
            state = MovementState.left;
        }
        else
        {
            state = MovementState.front;
        }

        anim.SetInteger("state", (int)state);
    }

    IEnumerator FireUpgrade()
    {
        hasUpgradedAttack = true;
        yield return new WaitForSeconds(10f);
        hasUpgradedAttack = false;
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

    IEnumerator GetAttackText()
    {
        getAttackUpgrade.SetActive(true);

        yield return new WaitForSeconds(2f);

        getAttackUpgrade.SetActive(false);
    }
}
