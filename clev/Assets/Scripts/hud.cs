using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hud : MonoBehaviour
{
    private Text HUD;
	void Start()
	{
		HUD = GetComponent<Text>();
	}

	void Update()
	{
		HUD.text = "Health: " + player_controller.health + "\r\nExperience: " + player_controller.experience + "\r\nGold: " + player_controller.gold;
	}
}
