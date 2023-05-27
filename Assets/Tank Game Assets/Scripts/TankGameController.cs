using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TankGameController : MonoBehaviour
{

    public static TankGameController tankGameController;
    public AudioSource explode;

    public GameObject gameOverScreen;
    public GameObject YouWinScreen;

    public bool gameOver = false;
    public bool winner = false;

    private void Awake()
    {
        if (tankGameController == null)
        {
            tankGameController = this;
        }else if(tankGameController != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("Player1Score", 0);
       // PlayerPrefs.SetInt("Player2Score", 0);
    }

    public void PlayExplode()
    {
        explode.Play();
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        gameOver = true;
        TankEnemySpawner.tankEnemySpawner.enabled = false;
        Player1Controller.player1Controller.enabled = false;
       // PlayerPrefs.SetInt("Player1Score", 0);
       // PlayerPrefs.SetInt("Player2Score", 0);
    }

    public void Winner()
    {
        YouWinScreen.SetActive(true);
        winner = true;
        TankEnemySpawner.tankEnemySpawner.enabled = false;
        Player1Controller.player1Controller.enabled = false;
        TankPlayer2.tankPlayer2.enabled = false;
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

    public void GoToMenu()
    {
        //gameOverScreen.SetActive(true);
        //gameOver = true;
        //TankEnemySpawner.tankPlayerController.enabled = false;
        //Player1Controller.player1Controller.enabled = false;
        SceneManager.LoadScene(0);
    }
}
