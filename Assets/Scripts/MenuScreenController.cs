using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreenController : MonoBehaviour
{
    public static MenuScreenController menuScreenController;
    public GameObject mainMenuScreen;

    void Awake()
    {
        if (menuScreenController == null)
        {
            menuScreenController = this;
        }else if (menuScreenController != this)
        {
            Destroy(gameObject);
        }
    }

    public void changeScene(int SceneNumber)
    {
        SceneManager.LoadScene(SceneNumber);
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
