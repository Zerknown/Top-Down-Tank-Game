using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update

    public static GameController gameController;
    public AudioSource explode;
    public GameObject gameOverScreen;

    public bool gameOver = false;

    public bool player2 = false;

    private void Awake()
    {
        if (gameController == null)
        {
            gameController = this;
        }else if(gameController != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayerPrefs.SetInt("Score", 0);
    }

    //Update is called once per frame
    private void Update()
    {
        
    }

    public void PlayExplode()
    {
        explode.Play();
    }

    public void GameOver()
    {
        //display game over menu
        gameOverScreen.SetActive(true);
        gameOver = true;
        EnemySpawner.enemySpawner.enabled = false;
        PlayerController.playerController.enabled = false;
        PlayerPrefs.SetInt("Score", 0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit() 
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void GotoMenu()
    {
        //gameOverScreen.SetActive(true);
        //gameOver = true;
        //EnemySpawner.enemySpawner.enabled = false;
        //PlayerController.playerController.enabled = false;
        SceneManager.LoadScene("Menu");
    }
}
