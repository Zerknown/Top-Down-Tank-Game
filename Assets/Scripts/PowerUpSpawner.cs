using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{

    public static PowerUpSpawner powerUpSpawner;

    [SerializeField] private GameObject attackPowerUpPrefab;
    [SerializeField] private GameObject shieldPowerUpPrefab;
    [SerializeField] private GameObject extraLifePrefab;

    [SerializeField] private float xPosMax = 2.75f;
    [SerializeField] private float xPosMin = -2.75f;
    [SerializeField] private float yPosMax = 7f;
    [SerializeField] private float yPosMin = 5f;
    
    private float spawnInterval;
    private int currentPowerUp;

    private void Awake()
    {
        if(powerUpSpawner == null)
        {
            powerUpSpawner = this;
        }else if(powerUpSpawner != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnInterval = Random.Range(6, 9);
        currentPowerUp = Random.Range(1, 4);
    }

    // Update is called once per frame
    void Update()
    {
        spawnInterval -= Time.deltaTime;

        if(spawnInterval <= 0)
        {
            float spawnXPosition = Random.Range(xPosMin, xPosMax);
            float spawnYPosition = Random.Range(yPosMin, yPosMax);

            if (currentPowerUp == 1)
            {
                GameObject attackPowerUp = (GameObject)Instantiate(attackPowerUpPrefab);
                attackPowerUp.transform.position = new Vector2(spawnXPosition, spawnYPosition);
            }
            else if(currentPowerUp == 2)
            {
                GameObject shieldPowerUp = (GameObject)Instantiate(shieldPowerUpPrefab);
                shieldPowerUp.transform.position = new Vector2(spawnXPosition, spawnYPosition);
            }
            else if(currentPowerUp == 3)
            {
                GameObject extraLife = (GameObject)Instantiate(extraLifePrefab);
                extraLife.transform.position = new Vector2(spawnXPosition, spawnYPosition);
            }

            spawnInterval = Random.Range(6, 9);
            currentPowerUp = Random.Range(1, 4);
        }
    }
}
