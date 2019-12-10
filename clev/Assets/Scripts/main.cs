using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class main : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            player_controller.health = 10;
            SceneManager.LoadScene("demo");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Application.Quit();
        }
    }   
}
