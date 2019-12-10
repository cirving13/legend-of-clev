using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_manager : MonoBehaviour
{
	public static AudioClip slap;
	static AudioSource audioSrc;

    void Start()
    {
		slap = Resources.Load<AudioClip>("slap");
		audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySlap()
	{
		audioSrc.PlayOneShot(slap);
	}
}
