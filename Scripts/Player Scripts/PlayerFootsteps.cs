using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour {

    private AudioSource footstep_Sound;

    // Allows to add many audio clips into a single array in the inspector
    [SerializeField]
    private AudioClip[] footstep_Clip;

    private CharacterController character_Controller;

    // Hidden in the inspector but remains public so the volume can be adjusted(through scripts)
    [HideInInspector]
    public float volume_Min, volume_Max;

    private float accumulated_Distance;

    [HideInInspector]
    public float step_Distance;

	void Awake () {
        footstep_Sound = GetComponent<AudioSource>();

        character_Controller = GetComponentInParent<CharacterController>();
	}
    // Update is called once per frame
    void Update () {
        CheckToPlayFootstepSound();	
	}
    // Function to play sound
    void CheckToPlayFootstepSound() {

        // If the player is NOT on the ground(jumping)
        if (!character_Controller.isGrounded) 
            return;
            
        if(character_Controller.velocity.sqrMagnitude > 0) {

            // Accumulated distance is the value of how far can we go e.g. make a step or sprint, or move while crouching until we play the footstep sound
            accumulated_Distance += Time.deltaTime;

            if(accumulated_Distance > step_Distance) {

                footstep_Sound.volume = Random.Range(volume_Min, volume_Max);
                footstep_Sound.clip = footstep_Clip[Random.Range(0, footstep_Clip.Length)];
                footstep_Sound.Play();

                accumulated_Distance = 0f;

            }

        } else {
            accumulated_Distance = 0f;
        }


    }



}


































