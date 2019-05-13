using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    // Skilgreinir Weapon manager
    private WeaponManager weapon_Manager;
    // 
    public float fireRate = 15f;
    private float nextTimeToFire;
    public float damage = 20f;
    // Skilgreinir Animator
    private Animator zoomCameraAnim;
    private bool zoomed;

    private Camera mainCam;
    // Skilgreinir crosshair gameobject
    private GameObject crosshair;

    private bool is_Aiming;
    // SerializeField til að draga prefabs inní
    [SerializeField]
    private GameObject arrow_Prefab, spear_Prefab;
    // SerializeField fyrir upphafsstöðu spjóts og örvar
    [SerializeField]
    private Transform arrow_Bow_StartPosition;
    // Awake er það fyrsta sem keyrir þegar leikurinn byrjar
    void Awake() {
        weapon_Manager = GetComponent<WeaponManager>();
        zoomCameraAnim = transform.Find("Look Root").transform.Find("FP Camera").GetComponent<Animator>();
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
    // Function til að skjóta vopninu sem player er haldandi á
    void WeaponShoot()
    {
        // Fær current selected weapon úr WeaponManager.cs og sér hvort playerinn er með vopn mep autofire eða ekki
        if (weapon_Manager.GetCurrentSelectedWeapon().fireType == WeaponFireType.MULTIPLE)
        {
            // Ef playerinn ýtir á left mouse click og ef Time meira en nextTimeToFire
            if (Input.GetMouseButton(0) && Time.time > nextTimeToFire)
            {
                // Spilar shoot animation
                nextTimeToFire = Time.time + 1f / fireRate;
                weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();

                BulletFired();
            }
        }
        else
        {
            // Ef left mouse click
            if (Input.GetMouseButtonDown(0))
            {
                // Ef player heldur á axe
                if (weapon_Manager.GetCurrentSelectedWeapon().tag == "Axe")
                {
                    // Kallar á attack animation fyrir öxina
                    weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
                }
                // Ef player heldur á byssu
                if (weapon_Manager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.BULLET)
                {
                    // Calls the attack animation for that weapon
                    weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
                    BulletFired();
                }
                else {

                    // Ef player er með boga eða spjót
                    if(is_Aiming) {
                        weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
                        if(weapon_Manager.GetCurrentSelectedWeapon().bulletType
                           == WeaponBulletType.ARROW) {

                            // Skýtur ör
                            ThrowArrowOrSpear(true);

                        } else if(weapon_Manager.GetCurrentSelectedWeapon().bulletType
                                  == WeaponBulletType.SPEAR) {
                            // Kastar Spjóti
                            ThrowArrowOrSpear(false);
                        }
                    }
                }
            } 
        } 
    }
    // Function sem zoomar út og inn (miðar)
    void ZoomInAndOut()
    {
        // we are going to aim with our camera on the weapon
        if (weapon_Manager.GetCurrentSelectedWeapon().weapon_Aim == WeaponAim.AIM)
        {
            // Þegar player ýtir á right mouse button
            if (Input.GetMouseButtonDown(1))
            {
                zoomCameraAnim.Play("ZoomIn");
                // Slekkur á crosshair
                crosshair.SetActive(false);
            }
            // Þegar player sleppir right mouse button
            if (Input.GetMouseButtonUp(1))
            {
                zoomCameraAnim.Play("ZoomOut");
                // Kveikir á crosshair
                crosshair.SetActive(true);
            }
        }
        // Ef það er miðað með boga eða spjóti
        if (weapon_Manager.GetCurrentSelectedWeapon().weapon_Aim == WeaponAim.SELF_AIM) {

            if(Input.GetMouseButtonDown(1)) {
                weapon_Manager.GetCurrentSelectedWeapon().Aim(true);

                is_Aiming = true;
                if (Input.GetMouseButtonDown(0))
                {
                    weapon_Manager.GetCurrentSelectedWeapon().Aim(false);
                    is_Aiming = false;
                }
            }
            if (Input.GetMouseButtonUp(1)) {
                weapon_Manager.GetCurrentSelectedWeapon().Aim(false);
                is_Aiming = false;
            }
        }
    }
    // Function til að kasta spjóti / skjóta ör
    void ThrowArrowOrSpear(bool throwArrow) {
        // Ef true þá er skotið ör
        if(throwArrow) {
            GameObject arrow = Instantiate(arrow_Prefab);
            arrow.transform.position = arrow_Bow_StartPosition.position;
            arrow.GetComponent<ArrowBowScript>().Launch(mainCam);
        }
        // Ef false er kastað spjóti
        else {
            GameObject spear = Instantiate(spear_Prefab);
            spear.transform.position = arrow_Bow_StartPosition.position;
            spear.GetComponent<ArrowBowScript>().Launch(mainCam);
        }
    } 
    // Function sem skýtur
    void BulletFired() {
        // Reycast sendir ósýnilega línu þar sem player er að horfa
        RaycastHit hit;
        // Ef reycast hittir enemy
        if(Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit)) {
            if(hit.transform.tag == "Enemy") {
                // Gerir damage
                hit.transform.GetComponent<HealthScript>().ApplyDamage(damage);
            }
        }
    } 
} 