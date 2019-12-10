using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pause : MonoBehaviour
{
    public static bool isPause = false;
    private Text text;
	void Start()
    {
		text = GetComponent<Text>();
        text.enabled = false;
    }

    void Update()
    {
        if (isPause)
		{
            text.enabled = true;
			Time.timeScale = 0;
		}
        else
        {
            Time.timeScale = 1;
            text.enabled = false;
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
