using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBowScript : MonoBehaviour {
    // Skilgreinir rigidbody
    private Rigidbody myBody;
    // Hraði
    public float speed = 30f;
    // Tími þangað til gameobject deactivate-ar
    public float deactivate_Timer = 3f;
    // Damage
    public float damage = 50f;
    // Awake er það fyrsta sem keyrir þegar leikurinn byrjar
    void Awake() {
        // Sækir Rigidobdy component
        myBody = GetComponent<Rigidbody>();
    }
    // Use this for initialization
    void Start () {
        // Invoke kallar á function á ákveðnum tíma
        Invoke("DeactivateGameObject", deactivate_Timer);
	}
    // Ýtir örinni / spjóti áfram
    public void Launch(Camera mainCamera) {
        myBody.velocity = mainCamera.transform.forward * speed;
        // Snýr örinni frá playerinum
        transform.LookAt(transform.position + myBody.velocity);
    }
	// Slekkur á game object ef það er active
    void DeactivateGameObject() {
        if(gameObject.activeInHierarchy) {
            gameObject.SetActive(false);
        }
    }
    // after we touch an enemy deactivate game object
    void OnTriggerEnter(Collider target) {
        // Eftir að það er hitt enemy
        if(target.tag == "Enemy") {
            // Gerir damage
            target.GetComponent<HealthScript>().ApplyDamage(damage);
            // deactivate game object
            gameObject.SetActive(false);
        }
    }
}