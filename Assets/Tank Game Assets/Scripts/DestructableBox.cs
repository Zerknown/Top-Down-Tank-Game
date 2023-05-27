using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableBox : MonoBehaviour
{

    [SerializeField] private GameObject extraLifePrefab;
    [SerializeField] private GameObject shieldPrefab;
    [SerializeField] private GameObject ghostPrefab;

    private int currentPowerUp;
    // Start is called before the first frame update
    void Start()
    {
        currentPowerUp = Random.Range(1, 11);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit");
        if (other.tag == "tankBullet")
        {
            if (currentPowerUp == 1)
            {
                GameObject extraLife = (GameObject)Instantiate(extraLifePrefab);
                extraLife.transform.position = transform.position;
            }else if(currentPowerUp == 2)
            {
                GameObject shield = (GameObject)Instantiate(shieldPrefab);
                shield.transform.position = transform.position;
            }else if(currentPowerUp == 3)
            {
                GameObject ghost = (GameObject)Instantiate(ghostPrefab);
                ghost.transform.position = transform.position;
            }
            
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        //Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision hit");

        other.rigidbody.velocity = new Vector2(0, 0);
    }
}
