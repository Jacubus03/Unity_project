using System.Numerics;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip[] musicClips;

    void Update()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.clip = musicClips[Random.Range(0, musicClips.Length)];
            musicSource.Play();
        }
    }
}
