using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    // Skilgreinir Animator
    private Animator anim;
    // Awake er það fyrsta sem keyrir þegar leikurinn byrjar
    void Awake()
    {
        // Sækir animator component
        anim = GetComponent<Animator>();
    }
    public void Walk(bool walk)
    {
        // Labba animtion
        anim.SetBool("Walk", walk);
    }
    public void Run(bool run)
    {
        // Hlaup animation
        anim.SetBool("Run", run);
    }
    public void Attack()
    {
        // Attack animation
        anim.SetTrigger("Attack");
    }

    public void Dead()
    {
        // Death animation
        anim.SetTrigger("Dead");
    }
}