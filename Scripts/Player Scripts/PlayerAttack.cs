using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private WeaponManager weapon_Manager;

    public float fireRate = 15f;
    private float nextTimeToFire;
    public float damage = 20f;

    private Animator zoomCameraAnim;
    private bool zoomed;

    private Camera mainCam;

    private GameObject crosshair;

    private bool is_Aiming;

    [SerializeField]
    private GameObject arrow_Prefab, spear_Prefab;

    [SerializeField]
    private Transform arrow_Bow_StartPosition;

    void Awake() {

        weapon_Manager = GetComponent<WeaponManager>();

        zoomCameraAnim = transform.Find("Look Root")
                                  .transform.Find("FP Camera").GetComponent<Animator>();

        crosshair = GameObject.FindWithTag("Crosshair");

        mainCam = Camera.main;

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        WeaponShoot();
        ZoomInAndOut();
    }

    // Function to fire the weapon the player is currently holding
    void WeaponShoot() {

        // Gets current selected weapon from WeaponManager.cs and if the player has a weapon with auto(multiple) fire such as an assault rifle
        if (weapon_Manager.GetCurrentSelectedWeapon().fireType == WeaponFireType.MULTIPLE) {

            // if we press and hold left mouse click AND
            // if Time is greater than the nextTimeToFire
            if(Input.GetMouseButton(0) && Time.time > nextTimeToFire) {

                nextTimeToFire = Time.time + 1f / fireRate;

                weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();

                BulletFired();

            }


        // If the player has a weapon that shoots once
        }
        else {
            // If left mouse click
            if(Input.GetMouseButtonDown(0)) {

                // If the player has the axe equipped
                if (weapon_Manager.GetCurrentSelectedWeapon().tag == "Axe") {
                    // Calls the attack animation for the axe
                    weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
                }

                // If the player has a gun equipped
                if (weapon_Manager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.BULLET) {
                    // Calls the attack animation for that weapon
                    weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();

                    BulletFired();

                }

            }

        }

    }

    // Function that zooms in and out
    void ZoomInAndOut() {

        // we are going to aim with our camera on the weapon
        if(weapon_Manager.GetCurrentSelectedWeapon().weapon_Aim == WeaponAim.AIM) {

            // If the player presses and holds the right mouse button
            if(Input.GetMouseButtonDown(1)) {

                zoomCameraAnim.Play("ZoomIn");

                crosshair.SetActive(false);
            }

            // When the player releases the right mouse button
            if (Input.GetMouseButtonUp(1)) {

                zoomCameraAnim.Play("ZoomOut");

                crosshair.SetActive(true);
            }

        } // if we need to zoom the weapon

        if(weapon_Manager.GetCurrentSelectedWeapon().weapon_Aim == WeaponAim.SELF_AIM) {

            if(Input.GetMouseButtonDown(1)) {

                weapon_Manager.GetCurrentSelectedWeapon().Aim(true);

                is_Aiming = true;

            }

            if (Input.GetMouseButtonUp(1)) {

                weapon_Manager.GetCurrentSelectedWeapon().Aim(false);

                is_Aiming = false;

            }

        } // weapon self aim

    }

    void BulletFired() {

        RaycastHit hit;

        if(Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit)) {

            if(hit.transform.tag == "Enemy") {
                // hit.transform.GetComponent<HealthScript>().ApplyDamage(damage);
            }

        }

    } // bullet fired

} // class































