using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    // Sækir PlayerMovement script
    private PlayerMovement playerMovement;
    // Hraða values fyrir moving states
    public float sprint_Speed = 10f;
    public float move_Speed = 5f;
    public float crouch_Speed = 2f;
    // Player hæð þegar hann er crouching/standing
    private Transform look_Root;
    private float stand_Height = 1.6f;
    private float crouch_Height = 1f;
    // Bool til að gá hvort player er að beygja sig eða ekki
    private bool is_Crouching;
    // Sækir PlayerFootsteps script
    private PlayerFootsteps player_Footsteps;
    // Stillir volume fyrir moving states
    private float sprint_Volume = 1f;
    private float crouch_Volume = 0.1f;
    private float walk_Volume_Min = 0.2f, walk_Volume_Max = 0.6f;
    // Hvað 1 skref er langt fyrir moving states
    private float walk_Step_Distance = 0.4f;
    private float sprint_Step_Distance = 0.25f;
    private float crouch_Step_Distance = 0.5f;
    // Sækir PlayerStats script
    private PlayerStats player_Stats;
    // Sprint stamina
    private float sprint_Value = 100f;
    public float sprint_Treshold = 10f;
    // Awake er það fyrsta sem keyrir þegar leikurinn byrjar
    void Awake()
    {
        // Sækir PlayerMovement script sem er attachað sem component
        playerMovement = GetComponent<PlayerMovement>();
        // Sækir fyrsta child objectið af current object
        look_Root = transform.GetChild(0);
        // Sækir PlayerFootsteps scriptið frá child objecti(Player Audio)
        player_Footsteps = GetComponentInChildren<PlayerFootsteps>();
        // Sækir PlayerStats script sem er attachað sem component
        player_Stats = GetComponent<PlayerStats>();

    }
    void Start()
    {
        // Stillir hljóð
        player_Footsteps.volume_Min = walk_Volume_Min;
        player_Footsteps.volume_Max = walk_Volume_Max;
        player_Footsteps.step_Distance = walk_Step_Distance;
    }
    // Update is called once per frame
    void Update()
    {
        Sprint();
        Crouch();
    }
    // Function til að hlaupa
    void Sprint()
    {
        // Ef player er með stamina getur hann hlaupið
        if (sprint_Value > 0f)
        {
            // Ef player er að ýta á shift og er ekki crouching
            if (Input.GetKeyDown(KeyCode.LeftShift) && !is_Crouching)
            {
                // Setur sprint hraða
                playerMovement.speed = sprint_Speed;
                // Setur sprint volume fyrir hljóð(PlayerFootsteps.cs)
                player_Footsteps.step_Distance = sprint_Step_Distance;
                player_Footsteps.volume_Min = sprint_Volume;
                player_Footsteps.volume_Max = sprint_Volume;

            }

        }
        // Ef player er ekki að ýta á shift og er ekki crouching
        if (Input.GetKeyUp(KeyCode.LeftShift) && !is_Crouching)
        {
            // Setur walk hraða
            playerMovement.speed = move_Speed;
            // Setur walk volume fyrir hljóð(PlayerFootsteps.cs)
            player_Footsteps.step_Distance = walk_Step_Distance;
            player_Footsteps.volume_Min = walk_Volume_Min;
            player_Footsteps.volume_Max = walk_Volume_Max;

        }
        // Ef player er að ýta á shift og er ekki crouching
        if (Input.GetKey(KeyCode.LeftShift) && !is_Crouching)
        {
            // Lækkar stamina
            sprint_Value -= sprint_Treshold * Time.deltaTime;
            // Hættir að hlaupa ef stamina er 0
            if (sprint_Value <= 0f)
            {
                sprint_Value = 0f;
                // Endurstillir speed og hlóð
                playerMovement.speed = move_Speed;
                player_Footsteps.step_Distance = walk_Step_Distance;
                player_Footsteps.volume_Min = walk_Volume_Min;
                player_Footsteps.volume_Max = walk_Volume_Max;
            }
            // Sendir hvað player hefur mikið stamina í PlayerStat script svo það geti uppfært UI
            player_Stats.Display_StaminaStats(sprint_Value);

        }
        else
        {
            // Ef player er ekki að hlaupa og stamina er ekki í 100
            if (sprint_Value != 100f)
            {
                // Hækkar stamina
                sprint_Value += (sprint_Treshold / 2f) * Time.deltaTime;
                // Sendir hvað player hefur mikið stamina í PlayerStat script svo það geti uppfært UI
                player_Stats.Display_StaminaStats(sprint_Value);
                // Ef stamina fer yfir 100 er það lagað og sett sem 100
                if (sprint_Value > 100f)
                {
                    sprint_Value = 100f;
                }
            }
        }
    }
    // Function til að crouch
    void Crouch()
    {
        // Ef player ýtir á C
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Ef player er crouching
            if (is_Crouching)
            {
                // Stendur upp (hækkar myndavél)
                look_Root.localPosition = new Vector3(0f, stand_Height, 0f);
                playerMovement.speed = move_Speed;
                // Setur walk volume fyrir audio script-ið(PlayerFootsteps.cs)
                player_Footsteps.step_Distance = walk_Step_Distance;
                player_Footsteps.volume_Min = walk_Volume_Min;
                player_Footsteps.volume_Max = walk_Volume_Max;

                is_Crouching = false;

            }
            // Ef player er ekki crouching
            else
            {
                // Beygir sig (lækkar myndavél)
                look_Root.localPosition = new Vector3(0f, crouch_Height, 0f);
                playerMovement.speed = crouch_Speed;
                // Setur crouch volume fyrir audio script-ið(PlayerFootsteps.cs)
                player_Footsteps.step_Distance = crouch_Step_Distance;
                player_Footsteps.volume_Min = crouch_Volume;
                player_Footsteps.volume_Max = crouch_Volume;
                is_Crouching = true;
            }
        }
    }
}