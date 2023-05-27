using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPlayer2 : MonoBehaviour
{

    public static TankPlayer2 tankPlayer2;

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

    [SerializeField] private GameObject Explosion;

    [SerializeField] private AudioSource fire;
    [SerializeField] private AudioSource powerUpSound;

    [SerializeField] private GameObject shield;

    [SerializeField] private float shieldHealth = 400f;

    private bool isShielded = false;

    [SerializeField] private bool isGameOver = false;
    [SerializeField] private GameObject RespawnPoint;

    [SerializeField] private Color c;

    [SerializeField] private GameObject getShieldPowerUp;
    [SerializeField] private GameObject getExtraLife;
    [SerializeField] private GameObject getGhostUpgrade;

    //private Animator anim;
    private SpriteRenderer sprite;
    private float dirX = 0f;

    private bool Upward;
    private bool Downward;
    private bool Leftward;
    private bool Rightward;

    private void Awake()
    {
        if(tankPlayer2 == null)
        {
            tankPlayer2 = this;
        }else if(tankPlayer2 != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        nextFire = fireInterval;
        //anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        c = sprite.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal2");

        nextFire -= Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Keypad0) && nextFire <= 0)
        {
            fire.Play();

            if (Leftward)
            {
                GameObject bulletLeft = (GameObject)Instantiate(tankBulletLeft);
                bulletLeft.transform.position = projectilePosition.transform.position;
            }else if (Rightward)
            {
                GameObject bulletRight = (GameObject)Instantiate(tankBulletRight);
                bulletRight.transform.position = projectilePosition.transform.position;
            }else if (Upward)
            {
                GameObject bulletUp = (GameObject)Instantiate(tankBulletUp);
                bulletUp.transform.position = projectilePosition.transform.position;
            }else
            {
                GameObject bulletDown = (GameObject)Instantiate(tankBulletDown);
                bulletDown.transform.position = projectilePosition.transform.position;
            }

            nextFire = fireInterval;
        }

        float x = Input.GetAxisRaw("Horizontal2");
        float y = Input.GetAxisRaw("Vertical2");

        if(x > .1f)
        {
            goRight();
        }else if(x < -.1f)
        {
            goLeft();
        }

        if(y > .1f)
        {
            goUp();
        }else if(y < -.1f)
        {
            goDown();
        }

        Vector2 direction = new Vector2(x, y).normalized;
        Move(direction);
    }

    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x = max.x - .5f;
        min.x = min.x + .5f;

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
        if(other.tag == "EnemySide" || other.tag == "enemyProjectile")
        {
            TankGameController.tankGameController.PlayExplode();
            Vector2 expos = transform.position;

            if(isShielded == true)
            {
                if(shieldHealth > 0)
                {
                    if(other.tag == "enemyProjectile")
                    {
                        shieldHealth -= 200f;
                    }

                    if(other.tag == "Enemy")
                    {
                        shieldHealth -= 400;
                    }
                }else
                {
                    isShielded = false;
                    shield.SetActive(false);
                }
            }else
            {
                GameObject explosion = (GameObject)Instantiate(Explosion);
                explosion.transform.position = expos;

                TankPlayerStats.tankPlayerStats.P2Life--;
                //TankPlayerStats.tankPlayerStats.enemyTanksLeft--;
                //TankPlayerStats.tankPlayerStats.UpdateP2Score();
                TankGameController.tankGameController.PlayExplode();

                if(TankPlayerStats.tankPlayerStats.P2Life > 0)
                {
                    transform.position = RespawnPoint.transform.position;
                    StartCoroutine("GetInvulnerable");
                }
                else
                {
                    isGameOver = true;
                    Destroy(gameObject);
                }
            }
            if (other.tag == "enemyProjectile")
            {
                Destroy(other.gameObject);
            }
        }

        if(other.tag == "Shield")
        {
            powerUpSound.Play();
            StartCoroutine("GetShieldText");
            shieldHealth = 400f;
            shield.SetActive(true);
            isShielded = true;

            Destroy(other.gameObject);
        }

        if(other.tag == "ExtraLife")
        {
            powerUpSound.Play();
            StartCoroutine("GetLifeText");
            if(TankPlayerStats.tankPlayerStats.P2Life < 3)
            {
                TankPlayerStats.tankPlayerStats.P2Life++;
                Destroy(other.gameObject);
            }
            else
            {
                Destroy(other.gameObject);
            }
        }

        if(other.tag == "Ghost")
        {
            powerUpSound.Play();
            StartCoroutine("GetGhostText");
            StartCoroutine("GhostPowerUp");
            Destroy(other.gameObject);
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
            transform.localRotation = Quaternion.Euler(0, 0, 180);
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
            transform.localRotation = Quaternion.Euler(0, 0, 0);
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
            transform.localRotation = Quaternion.Euler(0, 0, -90);
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
            transform.localRotation = Quaternion.Euler(0, 0, 90);
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
