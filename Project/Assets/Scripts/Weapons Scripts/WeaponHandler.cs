using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Enum býr til set af elementum sem geta verið notuð seinna
public enum WeaponAim {
    NONE,
    SELF_AIM,
    AIM
}

public enum WeaponFireType {
    SINGLE,
    MULTIPLE
}

public enum WeaponBulletType {
    BULLET,
    ARROW,
    SPEAR,
    NONE
}

public class WeaponHandler : MonoBehaviour {
    // Sækir animatior component
    private Animator anim;

    public WeaponAim weapon_Aim;
    // SerializeField býr til box til að draga muzzle flash object inn í
    [SerializeField]
    private GameObject muzzleFlash;
    // SerializeField býr til box til að draga hljóð inní
    [SerializeField]
    private AudioSource shootSound, reload_Sound;

    public WeaponFireType fireType;

    public WeaponBulletType bulletType;

    // Þetta er notað til að sjá hvort player collided with an enemy eða ekki
    public GameObject attack_Point;
    // Awake er það fyrsta sem keyrir þegar leikurinn byrjar
    void Awake()
    {
        // Sækir animator components+
        anim = GetComponent<Animator>();
    }
    // Spilar skot animation
    public void ShootAnimation()
    {
        // Ef Idle animation er að spilast
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            // Spilar skot animation
            anim.SetTrigger("Shoot");
        }
    }
    // Spilar aim animation
    public void Aim(bool canAim)
    {
        anim.SetBool("Aim", canAim);
    }
    // Kveikir á muzzle flash
    void Turn_On_MuzzleFlash()
    {
        muzzleFlash.SetActive(true);
    }
    // Slekkur á muzzle flash
    void Turn_Off_MuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }
    // Spilar skothlóð
    void Play_ShootSound()
    {
        shootSound.Play();
    }
    // Spilar reload hlóð hjá shotgun
    void Play_ReloadSound()
    {
        reload_Sound.Play();
    }
    // Kveikir á attack point
    void Turn_On_AttackPoint()
    {
        attack_Point.SetActive(true);
    }
    // Slekkur á attack point
    void Turn_Off_AttackPoint()
    {
        if (attack_Point.activeInHierarchy)
        {
            attack_Point.SetActive(false);
        }
    }
}