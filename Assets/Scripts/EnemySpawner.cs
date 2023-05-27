using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyShipPrefab;
    public GameObject enemyMotherShipPrefab;
    

    public static EnemySpawner enemySpawner;

    //spawnposition outside camera
    public float xPosMax = 2.75f;
    public float xPosMin = -2.75f;
    public float yPosMax = 7f;
    public float yPosMin = 5f;
    float spawnInterval;
    private int currentEnemyShip = 0;

    private float currentScore;
    private float bossScore = 4000f;
    private float motherShipHealth;
    public bool motherShipInitialSpawned = false;

    private void Awake()
    {
        if (enemySpawner == null)
        {
            enemySpawner = this;
        }
        else if(enemySpawner != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnInterval = Random.Range(1, 3);
        //PlayerPrefs.SetInt("Score", 0);
    }

    // Update is called once per frame
    void Update()
    {
        spawnInterval -= Time.deltaTime;

        //PlayerPrefs.SetInt("Score", 0);

        //Debug.Log("Boss Health is " + boss);

        if (spawnInterval <= 0)
        {
            float spawnXPosition = Random.Range(xPosMin, xPosMax);
            float spawnYPosition = Random.Range(yPosMin, yPosMax);

            GameObject enemyShip = (GameObject)Instantiate(enemyShipPrefab);

            enemyShip.transform.position = new Vector2(spawnXPosition, spawnYPosition);

            //currentEnemyShip++;
            spawnInterval = Random.Range(1, 3);
        }

        currentScore = PlayerPrefs.GetInt("Score");

        if (currentScore >= bossScore)
        {
            if (!motherShipInitialSpawned)
            {
                motherShipHealth = 2000f;

                GameObject enemyMotherShip = (GameObject)Instantiate(enemyMotherShipPrefab);
                bossScore += 8500;
                
                MotherShipController.motherShipController.setMotherShipHealth(motherShipHealth);
                motherShipInitialSpawned = true;
            }else
            {

                motherShipHealth += 500f;
                GameObject enemyMotherShip = (GameObject)Instantiate(enemyMotherShipPrefab);
                MotherShipController.motherShipController.setMotherShipHealth(motherShipHealth);
                bossScore += 8500;
            }
            
            
        }

        

        
    }
}
