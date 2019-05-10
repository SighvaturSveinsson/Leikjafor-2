using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enum creates words that can be used later on (like a list)
public enum WeaponAim {
    NONE,
    SELF_AIM,
    AIM
}

// Auto vs Single fire
public enum WeaponFireType {
    SINGLE,
    MULTIPLE
}

public enum WeaponBulletType {
    BULLET,
    NONE
}

public class WeaponHandler : MonoBehaviour {

    private Animator anim;

    public WeaponAim weapon_Aim;

    [SerializeField]
    private GameObject muzzleFlash;

    [SerializeField]
    private AudioSource shootSound, reload_Sound;

    public WeaponFireType fireType;

    public WeaponBulletType bulletType;

    // This will be used to determine if we collided with an enemy or not
    public GameObject attack_Point;

    void Awake() {
        anim = GetComponent<Animator>();
    }

    public void ShootAnimation() {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            anim.SetTrigger("Shoot");
        }
            
    }

    public void Aim(bool canAim) {
        anim.SetBool("Aim", canAim);
    }

    void Turn_On_MuzzleFlash() {
        muzzleFlash.SetActive(true);
    }

    void Turn_Off_MuzzleFlash() {
        muzzleFlash.SetActive(false);
    }

    void Play_ShootSound() {
        shootSound.Play();
    }

    void Play_ReloadSound() {
        reload_Sound.Play();
    }

    void Turn_On_AttackPoint() {
        attack_Point.SetActive(true);
    }

    void Turn_Off_AttackPoint() {
        if(attack_Point.activeInHierarchy) {
            attack_Point.SetActive(false);
        }
    }

}





































