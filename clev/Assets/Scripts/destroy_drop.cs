﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy_drop : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D c)
	{
        if(c.gameObject.tag == "Player")
		{
			Destroy(this.gameObject);
		}
	}
}
