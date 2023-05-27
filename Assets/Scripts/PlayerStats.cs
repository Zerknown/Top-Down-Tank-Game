using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI scoreText;
    public static PlayerStats playerStats;
    int score;

    public int playerLife;
    public int maxLife;

    public Image[] life;
    public Sprite fullLife;
    public Sprite emptyLife;

    private void Awake()
    {
        if (playerStats == null)
        {
            playerStats = this;
        }
        else if (playerStats != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        score = PlayerPrefs.GetInt("Score");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLife();
        //score = PlayerPrefs.GetInt("Score");
    }

    public void UpdateScore()
    {
        score += 200;
        PlayerPrefs.SetInt("Score", score);
        string scoreStr = string.Format("{0:00000000}", score);
        scoreText.text = "Score: " + scoreStr;
    }

    public void UpdateMotherShipScore()
    {
        score += 4000;
        PlayerPrefs.SetInt("Score", score);
        string scoreStr = string.Format("{0:00000000}", score);
        scoreText.text = "Score: " + scoreStr;
    }

    void UpdateLife()
    {
        if(playerLife > maxLife)
        {
            playerLife = maxLife; //If the player's current number
                                  //of lives is 5 and he picks a
                                  //lifeUp, the current life will
                                  //be the max life
        }

        for (int i = 0; i < life.Length; i++)
        {
            //the source image of the sprite is changed when it
            //falls into the following condition. if the image's
            //index is below the current life of the player the
            //sprite being rendered is full, else it is empty

            if(i < playerLife)
            {
                life[i].sprite = fullLife;
            }else
            {
                life[i].sprite = emptyLife;
            }

            //if you choose to limit lives to a certain number, 
            //array elements higher than that are disabled. say
            //if you want your max life to 3, array elements 0, 1, 2
            //are enabled while 3, 4 are disabled
            if(i < maxLife)
            {
                life[i].enabled = true;
            }
            else
            {
                life[i].enabled = false;
            }
        }
    }
}
