using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class end_game_thing : MonoBehaviour
{
	void Update()
    {
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
