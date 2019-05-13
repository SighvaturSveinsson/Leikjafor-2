using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    // Attack point radius og damage
    public float damage = 2f;
    public float radius = 1f;
    // Layermask verður notað í hit detection
    public LayerMask layerMask;

    void Update()
    {
        // Ef að attack point hittir object sem er á sama layeri er það sett í lista
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layerMask);
        // Ef að listinn er ekki tómur
        if (hits.Length > 0)
        {
            // Sækir HealthScript á objectinu sem var hitt og apply-ar damage
            hits[0].gameObject.GetComponent<HealthScript>().ApplyDamage(damage);
            // Disable game object
            gameObject.SetActive(false);
        }
    }
}