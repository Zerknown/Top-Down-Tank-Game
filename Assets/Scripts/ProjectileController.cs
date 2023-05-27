using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    float speed;
    public GameObject Explosion;

    // Start is called before the first frame update
    void Start()
    {
        speed = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;

        position = new Vector2(position.x, position.y + speed * Time.deltaTime); //moves the project upward

        transform.position = position;

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); //transforms position from viewport space into world point


        if (transform.position.y > max.y)
        {
            Destroy(gameObject); //destroys the projectile if it moved out the screen
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            PlayerStats.playerStats.UpdateScore();
        }
        
        //explosion effect is 
                                            //positioned in the location where
                                            //player has collided with the 
                                            //enemy or hit by its projectile

        

        if (other.tag == "Enemy" || other.tag == "enemyProjectile")
        {
            Vector2 expos = transform.position;
            GameObject explosion = (GameObject)Instantiate(Explosion);
            explosion.transform.position = expos;
            GameController.gameController.PlayExplode();
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
