using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour {
    // Skilgreinir EnemyAnimator script, NavMesh fyrir enemies og EnemyController script
    private EnemyAnimator enemy_Anim;
    private NavMeshAgent navAgent;
    private EnemyController enemy_Controller;
    // Health
    public float health = 100f;
    // Bool til að vita hvað objectið er
    public bool is_Player, is_Boar, is_Cannibal;
    // Bool til að vita hvort player er dauður eða ekki
    private bool is_Dead;
    // Skilgreinir EnemyAudio script
    private EnemyAudio enemyAudio;
    // Skilgreinir PlayerStats script
    private PlayerStats player_Stats;
    // Skilgreinir PlayerStats script sem er notað fyrir score
    private PlayerStats player_StatsUpdate;
    // Awake er það fyrsta sem keyrir þegar leikurinn byrjar
    void Awake () {
        // Finnur playerstats scriptið í player objectinu
        player_StatsUpdate = GameObject.Find("Player").GetComponent<PlayerStats>();
        // Ef objectið er taggað með is_Boar eða is_Cannibal
        if (is_Boar || is_Cannibal) {
            // Sækir EnemyAnimator script, NavMesh fyrir enemies og EnemyController script
            enemy_Anim = GetComponent<EnemyAnimator>();
            enemy_Controller = GetComponent<EnemyController>();
            navAgent = GetComponent<NavMeshAgent>();
            // Sækir enemy audio úr child objecti
            enemyAudio = GetComponentInChildren<EnemyAudio>();
        }
        // Ef objectið er taggað með is_Player
        if (is_Player) {
            // Sækir PlayerStats script
            player_Stats = GetComponent<PlayerStats>();
        }

	}
    // Function sem setur damage á player / enemy
    public void ApplyDamage(float damage) {
        // Ef player deyr ekki keyra restina af kóðanum í þessu falli
        if (is_Dead)
            return;
        // Minnkar Health
        health -= damage;
        if(is_Player) {
            // Uppfærir health UI(display health UI value)
            player_Stats.Display_HealthStats(health);
        }
        //
        if(is_Boar || is_Cannibal) {
            if (enemy_Controller.Enemy_State == EnemyState.PATROL) {
                enemy_Controller.chase_Distance = 50f;
            }
        }
        // Ef health fer undir 0
        if(health <= 0f) {
            PlayerDied();
            is_Dead = true;
        }

    }
    // Þegar player deyr
    void PlayerDied() {
        // Ef objectið er taggað með is_Cannibal
        if (is_Cannibal) {
            // Slekkur á enemy componentum
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<Rigidbody>().AddTorque(-transform.forward * 5f);
            // Slekkur á enemy scripts
            enemy_Controller.enabled = false;
            navAgent.enabled = false;
            enemy_Anim.enabled = false;
            // Spilar death sound
            StartCoroutine(DeadSound());
            // Segir EnemyManager að spawn fleiri enemies
            EnemyManager.instance.EnemyDied(true);
            // Uppfærir score display
            player_StatsUpdate.Display_Score();
        }
        // Ef objectið er taggað með is_Boar
        if (is_Boar) {
            // Stoppar velocity og slekkur á enemy scripts
            navAgent.velocity = Vector3.zero;
            navAgent.isStopped = true;
            enemy_Controller.enabled = false;
            // Spilar death animation
            enemy_Anim.Dead();
            // Spilar death sound
            StartCoroutine(DeadSound());
            // EnemyManager spawn more enemies
            EnemyManager.instance.EnemyDied(false);
            // Uppfærir score display
            player_StatsUpdate.Display_Score();
        }
        // Ef player deyr
        if (is_Player) {
            // Slekkur á enemy controller fyrir alla enemies
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemies.Length; i++) {
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }
            // Segir enemy manager að hætta að spawn-a enemies
            EnemyManager.instance.StopSpawning();
            // Slekkur á player scripts
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);

        }
        //
        if(tag == "Player") {
            Invoke("RestartGame", 3f);
        } else {
            Invoke("TurnOffGameObject", 3f);
        }

    }
    // Þegar player deyr er hann sendur í end game screen
    void RestartGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("EndScene");
        Cursor.lockState = CursorLockMode.None;
    }
    // Function til að slökkva á gameObjecti
    void TurnOffGameObject() {
        gameObject.SetActive(false);
    }
    // Spilar death sound fyrir enemies
    IEnumerator DeadSound() {
        yield return new WaitForSeconds(0.3f);
        enemyAudio.Play_DeadSound();
    }
}