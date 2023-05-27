using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyTankPrefab;

    public static TankEnemySpawner tankEnemySpawner;

    [SerializeField] private GameObject respawnPoint1;
    [SerializeField] private GameObject respawnPoint2;
    [SerializeField] private GameObject respawnPoint3;
    [SerializeField] private GameObject respawnPoint4;

    float spawnInterval;

    private int respawnPoints = 0;
    private int EnemyLeft = 20;

    private void Awake()
    {
        if (tankEnemySpawner == null)
        {
            tankEnemySpawner = this;
        }else if(tankEnemySpawner != this)
        {
            Destroy(gameObject);
        }

        
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnInterval = Random.Range(1, 5);
    }

    // Update is called once per frame
    void Update()
    {
        spawnInterval -= Time.deltaTime;

        if(spawnInterval <= 0)
        {
            Debug.Log("Tank Enemy Spawner " + EnemyLeft);

            if(respawnPoints == 0 && EnemyLeft > 0)
            {
                GameObject enemyTank = (GameObject)Instantiate(enemyTankPrefab);
                enemyTank.transform.position = respawnPoint1.transform.position;
                respawnPoints++;
                EnemyLeft--;
            }
            else if(respawnPoints == 1){
                GameObject enemyTank = (GameObject)Instantiate(enemyTankPrefab);
                enemyTank.transform.position = respawnPoint2.transform.position;
                respawnPoints++;
                EnemyLeft--;

            }else if(respawnPoints == 2)
            {
                GameObject enemyTank = (GameObject)Instantiate(enemyTankPrefab);
                enemyTank.transform.position = respawnPoint3.transform.position;
                respawnPoints++;
                EnemyLeft--;
            }else if(respawnPoints == 3)
            {
                GameObject enemyTank = (GameObject)Instantiate(enemyTankPrefab);
                enemyTank.transform.position = respawnPoint4.transform.position;
                respawnPoints = 0;
                EnemyLeft--;
            }
            spawnInterval = Random.Range(1, 5);
        }
    }
}
