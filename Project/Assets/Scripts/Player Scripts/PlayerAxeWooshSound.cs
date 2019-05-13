using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAxeWooshSound : MonoBehaviour
{
    // Field fyrir audio clips
    [SerializeField]
    private AudioSource audioSource;
    // Array af fields fyrir audio clips
    [SerializeField]
    private AudioClip[] woosh_Sounds;

    void PlayWooshSound()
    {
        // Spilar random audio clips úr arrayinu
        audioSource.clip = woosh_Sounds[Random.Range(0, woosh_Sounds.Length)];
        audioSource.Play();
    }
}