using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState {
    PATROL,
    CHASE,
    ATTACK
}

public class EnemyController : MonoBehaviour {
    // Skilgreinir EnemyAnimator og NavMesh
    private EnemyAnimator enemy_Anim;
    private NavMeshAgent navAgent;

    private EnemyState enemy_State;
    // Hraði óvina
    public float walk_Speed = 0.5f;
    public float run_Speed = 4f;
    //
    public float chase_Distance = 7f;
    private float current_Chase_Distance;
    public float attack_Distance = 1.8f;
    public float chase_After_Attack_Distance = 2f;
    // Radíus fyrir patrol
    public float patrol_Radius_Min = 20f, patrol_Radius_Max = 60f;
    public float patrol_For_This_Time = 15f;
    private float patrol_Timer;
    // Attack cooldown
    public float wait_Before_Attack = 2f;
    private float attack_Timer;
    // Skilgreinir staðsetningu óvins
    private Transform target;
    // Skilgreinir attack point
    public GameObject attack_Point;

    private EnemyAudio enemy_Audio;
    // Awake er það fyrsta sem keyrir þegar leikurinn byrjar
    void Awake() {
        // Sækir script component-a
        enemy_Anim = GetComponent<EnemyAnimator>();
        navAgent = GetComponent<NavMeshAgent>();
        // Finnur player objectinn og staðsetningu hans
        target = GameObject.FindWithTag("Player").transform;
        // Sækir audio script component-a
        enemy_Audio = GetComponentInChildren<EnemyAudio>();
    }
    // Use this for initialization
    void Start ()
    {
        // Óvinir byrja á patrol
        enemy_State = EnemyState.PATROL;
        // Patrol timer
        patrol_Timer = patrol_For_This_Time;
        // Þegar óvinir komast að playerinum attack-a þeir strax
        attack_Timer = wait_Before_Attack;
        //
        current_Chase_Distance = chase_Distance;
	}
	// Update is called once per frame
	void Update () {
		// Keyrir mismunandi functions eftir hvaða state enemy er í 
        if(enemy_State == EnemyState.PATROL) {
            Patrol();
        }
        if(enemy_State == EnemyState.CHASE) {
            Chase();
        }
        if (enemy_State == EnemyState.ATTACK) {
            Attack();
        }
    }
    // Fall til að láta enemy labba um(patrol-a)
    void Patrol() {
        // Segir nav agent að hann geti fært sig
        navAgent.isStopped = false;
        navAgent.speed = walk_Speed;
        // Bætir við patrol timerinn
        patrol_Timer += Time.deltaTime;
        // Ef enemy er buinn að patrola í meira tíma en patrol_for_this_time velur hann nytt random destination
        if(patrol_Timer > patrol_For_This_Time) {
            SetNewRandomDestination();
            // Resetar patrol timer
            patrol_Timer = 0f;
        }
        //
        if(navAgent.velocity.sqrMagnitude > 0) {
            enemy_Anim.Walk(true);
        } else {
            enemy_Anim.Walk(false);
        }
        // Ef vegalengdina milli player og enemy er minni en chase distance fer hann á eftir player
        if(Vector3.Distance(transform.position, target.position) <= chase_Distance) {
            // Stoppar walk animation
            enemy_Anim.Walk(false);
            // Skiptir um state
            enemy_State = EnemyState.CHASE;

            // Spilar audio þegar hann sér player
            enemy_Audio.Play_ScreamSound();
        }
    }
    // Fall sem eltir playerinn
    void Chase() {
        // Leyfir navmesh agent að færa sig aftur
        navAgent.isStopped = false;
        navAgent.speed = run_Speed;
        // Setur staðsetningu player's sem destination
        navAgent.SetDestination(target.position);
        //
        if (navAgent.velocity.sqrMagnitude > 0) {
            enemy_Anim.Run(true);
        } else {
            enemy_Anim.Run(false);
        }
        // Ef fjarlægðin milli enemy and player er minna en attack distance
        if(Vector3.Distance(transform.position, target.position) <= attack_Distance) {
            // Stoppar animations
            enemy_Anim.Run(false);
            enemy_Anim.Walk(false);
            enemy_State = EnemyState.ATTACK;
            // Endurstillir chase distance to previous
            if(chase_Distance != current_Chase_Distance) {
                chase_Distance = current_Chase_Distance;
            }
        }
        // Ef player hleypur nógu langt frá enemy
        else if (Vector3.Distance(transform.position, target.position) > chase_Distance) {
            // Enemy hættir að hlaupa
            enemy_Anim.Run(false);
            // Fer aftur að patrola
            enemy_State = EnemyState.PATROL;
            // Endurstillir patrol timer til að fá nýtt destination strax
            patrol_Timer = patrol_For_This_Time;
            // Endurstillir chase distance to previous
            if (chase_Distance != current_Chase_Distance) {
                chase_Distance = current_Chase_Distance;
            }
        }
    }
    // Fall til að attacka
    void Attack() {
        // Stoppar navmesh
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;
        // Attack timer uppfærður
        attack_Timer += Time.deltaTime;
        // Ef attack time er minni en attack cooldown
        if(attack_Timer > wait_Before_Attack) {
            // Spilar attack animation
            enemy_Anim.Attack();
            // Resetar attack timer
            attack_Timer = 0f;
            // Spilar attack sound
            enemy_Audio.Play_AttackSound();
        }
        // Ef enemy er ekki í range til að attacka þá eltir hann playerinn
        if(Vector3.Distance(transform.position, target.position) >
           attack_Distance + chase_After_Attack_Distance) {
            // State update
            enemy_State = EnemyState.CHASE;
        }
    }
    // Velur random destination
    void SetNewRandomDestination() {
        float rand_Radius = Random.Range(patrol_Radius_Min, patrol_Radius_Max);
        Vector3 randDir = Random.insideUnitSphere * rand_Radius;
        randDir += transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDir, out navHit, rand_Radius, -1);
        // Sendir svo destination í navmesh agent
        navAgent.SetDestination(navHit.position);

    }
    // Kveikir á attack point ef það er active
    void Turn_On_AttackPoint() {
        attack_Point.SetActive(true);
    }
    // Slekkur á attack point ef það er active
    void Turn_Off_AttackPoint() {
        if (attack_Point.activeInHierarchy) {
            attack_Point.SetActive(false);
        }
    }
    //
    public EnemyState Enemy_State {
        get; set;
    }
}