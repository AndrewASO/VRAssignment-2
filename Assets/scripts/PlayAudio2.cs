using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This script is triggering audio when used with GazeOverEvent.cs script.
 */

public class PlayAudio2 : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySong()
    {
        audioSource.Play();                //play an audio
    }
}
