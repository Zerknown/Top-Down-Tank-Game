using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TankPlayerStats : MonoBehaviour
{

    public TextMeshProUGUI P1scoreText;
    public TextMeshProUGUI P2scoreText;

    public TextMeshProUGUI p1LivesText;
    public TextMeshProUGUI p2LivesText;

    [SerializeField] private TextMeshProUGUI enemyTanksLeftText;

    public int enemyTanksLeft = 20;

    public static TankPlayerStats tankPlayerStats;

    int P1score;
    int P2score;

    public int P1Life;
    public int P2Life;

    public bool P1Dead;
    public bool P2Dead;

    public int P1maxLife;
    public int P2maxLife;

    public bool TwoPlayers = false;

    public bool YouWon;

    private void Awake()
    {
        if (tankPlayerStats == null)
        {
            tankPlayerStats = this;
        }else if(tankPlayerStats != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //P1score = PlayerPrefs.GetInt("Player1Score");
        //P2score = PlayerPrefs.GetInt("Player2Score");
        P1score = 0;
        P2score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        UpdateLife();
        UpdateNumberOfEnemies();

        if (enemyTanksLeft <= 0)
        {
            TankGameController.tankGameController.Winner();
        }

        if (TwoPlayers)
        {
            
            
            if(P1Life <= 0 && P2Life <= 0)
            {
                TankGameController.tankGameController.GameOver();
            }
        }
        else
        {
            if(P1Life <= 0)
            {
                TankGameController.tankGameController.GameOver();
            }
        }
    }

    public void UpdateP1Score()
    {
        P1score += 200;
        //PlayerPrefs.SetInt("Player1Score", P1score);
        P1scoreText.text = "P1 Score: " + P1score; 
    }

    public void UpdateP2Score()
    {
        P2score += 200;
        //PlayerPrefs.SetInt("Player2Score", P2score);
        P2scoreText.text = "P2 Score: " + P2score;
    }

    public void UpdateNumberOfEnemies()
    {
        enemyTanksLeftText.text = "Enemy Left: " + enemyTanksLeft + "/20";

    }

    void UpdateLife()
    {
        if(P1Life > P1maxLife)
        {
            P1Life = P1maxLife;
        }

        if(P2Life > P2maxLife)
        {
            P2Life = P2maxLife;
        }

        
        string p1Life = string.Format("{0}", P1Life);
        p1LivesText.text = p1Life + "x";

        string p2Life = string.Format("{0}", P2Life);
        p2LivesText.text = P2Life + "x";
        
    }
}
