using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sprite_animator : MonoBehaviour
{
	[SerializeField]
	private Sprite[] frameArray;
	private int currentFrame;
	private float timer;
    private float framerate = 0.1f;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
		timer += Time.deltaTime;

        if (timer >= framerate)
		{
			timer -= framerate;
			currentFrame = (currentFrame + 1) % frameArray.Length;
            spriteRenderer.sprite = frameArray[currentFrame];
		}
    }
}
