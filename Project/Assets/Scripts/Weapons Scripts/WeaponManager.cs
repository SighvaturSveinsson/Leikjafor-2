using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // SerialzieFeild býr til lista í inspectorinum sem gerir það hægt að skrá hversu langur listinn er og svo er hægt að draga weapon objectin í viðeigandi box
    [SerializeField]
    private WeaponHandler[] weapons;

    private int current_Weapon_Index;

    // Use this for initialization
    void Start()
    {
        current_Weapon_Index = 0;
        weapons[current_Weapon_Index].gameObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        // Velur vopn eftir hvaða takka er ýtt á
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TurnOnSelectedWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TurnOnSelectedWeapon(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TurnOnSelectedWeapon(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TurnOnSelectedWeapon(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            TurnOnSelectedWeapon(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            TurnOnSelectedWeapon(5);
        }
    }
    // Function sem kveikir og slekkur á weapons
    void TurnOnSelectedWeapon(int weaponIndex)
    {
        // Passar að draw animation er ekki spilað aftur ef player velur vopnið sem hann heldur á
        if (current_Weapon_Index == weaponIndex)
            return;
        // Slekkur á vopninu sem er kveikt á
        weapons[current_Weapon_Index].gameObject.SetActive(false);
        // Kveikir á vopninu sem var valið 
        weapons[weaponIndex].gameObject.SetActive(true);
        // Geymir indexið á vopninu sem var valið
        current_Weapon_Index = weaponIndex;

    }
    // Finnur vopnið sem er valið
    public WeaponHandler GetCurrentSelectedWeapon()
    {
        return weapons[current_Weapon_Index];
    }
}