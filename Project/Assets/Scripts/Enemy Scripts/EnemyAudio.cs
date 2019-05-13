using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour {
    // Skilgreinir audiosource
    private AudioSource audioSource;
    // SerializeField til að geta dregið audio clips í inspectorinum
    [SerializeField]
    private AudioClip scream_Clip, die_Clip;
    // SerializeField array til geta dregið audio clips í inspectorinum
    [SerializeField]
    private AudioClip[] attack_Clips;

    // Use this for initialization
    void Awake () {
        // Sækir AudioSource component
        audioSource = GetComponent<AudioSource>();
	}
    public void Play_ScreamSound() {
        // Spilar öskur clip
        audioSource.clip = scream_Clip;
        audioSource.Play();
    }
    public void Play_AttackSound() {
        // Spilar random clip úr attack array
        audioSource.clip = attack_Clips[Random.Range(0, attack_Clips.Length)];
        audioSource.Play();
    }
    public void Play_DeadSound() {
        // Spilar death sound
        audioSource.clip = die_Clip;
        audioSource.Play();
    }
} 

































