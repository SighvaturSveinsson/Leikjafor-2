using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    // Sækir AudioSource component
    private AudioSource footstep_Sound;
    // Gerir kleift að setja mörg audio clips í eitt array í inspectorium
    [SerializeField]
    private AudioClip[] footstep_Clip;
    // Sækir CharacterController component
    private CharacterController character_Controller;
    // Falið í inspectorinum en er public svo það er hægt að breyta volume í gegnum scripts
    [HideInInspector]
    public float volume_Min, volume_Max;
    // Útskýrt seinna
    private float accumulated_Distance;
    // Falið í inspectorinum en er public svo það er hægt að breyta volume í gegnum scripts
    [HideInInspector]
    public float step_Distance;
    // Awake er það fyrsta sem keyrir þegar leikurinn byrjar
    void Awake()
    {
        footstep_Sound = GetComponent<AudioSource>();
        // Sækir character controller úr parent objecti(Player)
        character_Controller = GetComponentInParent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        CheckToPlayFootstepSound();
    }
    // Function til að spila hljóð
    void CheckToPlayFootstepSound()
    {

        // Ef player er ekki á jörðinni(jumping)
        if (!character_Controller.isGrounded)
            // Keyrir ekki restina af þessu functioni
            return;
        if (character_Controller.velocity.sqrMagnitude > 0)
        {
            // Accumulated distance er hversu lang er hægt að fara áður en fótspor sound effectið er spilað, mismunandi langt eftir hvort maður er hlaupandi, labbandi eða crouching
            accumulated_Distance += Time.deltaTime;
            // Spilar hljóð ef Accumulated distance er meira en step distance
            if (accumulated_Distance > step_Distance)
            {
                // Spilar random clip úr hljóð array-i
                footstep_Sound.volume = Random.Range(volume_Min, volume_Max);
                footstep_Sound.clip = footstep_Clip[Random.Range(0, footstep_Clip.Length)];
                footstep_Sound.Play();
                // Endurstillir Accumulated distance
                accumulated_Distance = 0f;
            }
        }
        else
        {
            // Endurstillir Accumulated distance
            accumulated_Distance = 0f;
        }
    }
}


































