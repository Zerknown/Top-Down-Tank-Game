using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    
    public static PlanetSpawner planetSpawner;

    [SerializeField] private GameObject planet1Prefab;
    [SerializeField] private GameObject planet2Prefab;
    [SerializeField] private GameObject planet3Prefab;
    [SerializeField] private GameObject planet4Prefab;
    [SerializeField] private GameObject planet5Prefab;

    [SerializeField] private float xPosMax = 2.75f;
    [SerializeField] private float xPosMin = -2.75f;
    [SerializeField] private float yPosMax = 7f;
    [SerializeField] private float yPosMin = 5f;

    private float spawnInterval;
    private int currentPlanet;

    private void Awake()
    {
        if (planetSpawner == null)
        {
            planetSpawner = this;
        }
        else if (planetSpawner != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        spawnInterval = Random.Range(6, 9);
        currentPlanet = Random.Range(1, 6);
    }

    void Update()
    {
        spawnInterval -= Time.deltaTime;

        if (spawnInterval <= 0)
        {
            float spawnXPosition = Random.Range(xPosMin, xPosMax);
            float spawnYPosition = Random.Range(yPosMin, yPosMax);



            if (currentPlanet == 1)
            {
                GameObject planet1 = (GameObject)Instantiate(planet1Prefab);
                planet1.transform.position = new Vector2(spawnXPosition, spawnYPosition);
            }
            else if (currentPlanet == 2)
            {
                GameObject planet2 = (GameObject)Instantiate(planet2Prefab);
                planet2.transform.position = new Vector2(spawnXPosition, spawnYPosition);
            }
            else if (currentPlanet == 3)
            {
                GameObject planet3 = (GameObject)Instantiate(planet3Prefab);
                planet3.transform.position = new Vector2(spawnXPosition, spawnYPosition);

            } else if (currentPlanet == 4)
            {
                GameObject planet4 = (GameObject)Instantiate(planet4Prefab);
                planet4.transform.position = new Vector2(spawnXPosition, spawnYPosition);
            } else if (currentPlanet == 5)
            {
                GameObject planet5 = (GameObject)Instantiate(planet5Prefab);
                planet5.transform.position = new Vector2(spawnXPosition, spawnYPosition);
            }

            spawnInterval = Random.Range(6, 9);
            currentPlanet = Random.Range(1, 6);
        }
    }
}

