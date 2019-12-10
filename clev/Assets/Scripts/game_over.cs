using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class game_over : MonoBehaviour
{
    public static bool isPlayerDead = false;
    private Text gameOver;
    void Start()
    {
        gameOver = GetComponent<Text>();
        gameOver.enabled = false;
    }

    void Update()
    {
        if (isPlayerDead)
        {
            Time.timeScale = 0;
            gameOver.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("Main");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Application.Quit();
        }
    }
}
