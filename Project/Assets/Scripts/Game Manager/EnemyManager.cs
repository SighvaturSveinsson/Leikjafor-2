using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    // Skilgreinir Enemy manager scriptið
    public static EnemyManager instance;
    // SerializeField fyrir boar og cannibal objects
    [SerializeField]
    private GameObject boar_Prefab, cannibal_Prefab;
    // Array fyrir random spawn points
    public Transform[] cannibal_SpawnPoints, boar_SpawnPoints;
    // SerializeField til að geta breytt í inspector en private til að geta ekki breytt í gegnum script
    [SerializeField]
    private int cannibal_Enemy_Count, boar_Enemy_Count;
    // Fjoldi óvina
    private int initial_Cannibal_Count, initial_Boar_Count;
    // Respawn timer
    public float wait_Before_Spawn_Enemies_Time = 10f;
    // Awake er það fyrsta sem keyrir þegar leikurinn byrjar
    void Awake () {
        MakeInstance();
    }
    // Use this for initialization
    void Start() {
        // Spawnar X marga enemies
        initial_Cannibal_Count = cannibal_Enemy_Count;
        initial_Boar_Count = boar_Enemy_Count;

        SpawnEnemies();
        // Kallar reglulega á function sem gáir hvort þarf að spawna fleiri enemies
        StartCoroutine("CheckToSpawnEnemies");
    }
    // Býr til instance
    void MakeInstance() {
        if(instance == null) {
            instance = this;
        }
    }
    // Spawnar enemies
    void SpawnEnemies() {
        SpawnCannibals();
        SpawnBoars();
    }
    // Function til að spawna cannibal
    void SpawnCannibals() {
        int index = 0;
        // 
        for (int i = 0; i < cannibal_Enemy_Count; i++) {

            if (index >= cannibal_SpawnPoints.Length) {
                index = 0;
            }
            // Segir til hvaða prefab er verið að búa til og hvar
            Instantiate(cannibal_Prefab, cannibal_SpawnPoints[index].position, Quaternion.identity);

            index++;

        }

        cannibal_Enemy_Count = 0;

    }
    // Function til að spawna boar
    void SpawnBoars() {

        int index = 0;

        for (int i = 0; i < boar_Enemy_Count; i++) {

            if (index >= boar_SpawnPoints.Length)
            {
                index = 0;
            }
            // Segir til hvaða prefab er verið að búa til og hvar
            Instantiate(boar_Prefab, boar_SpawnPoints[index].position, Quaternion.identity);

            index++;

        }

        boar_Enemy_Count = 0;

    }

    IEnumerator CheckToSpawnEnemies() {
        yield return new WaitForSeconds(wait_Before_Spawn_Enemies_Time);

        SpawnCannibals();

        SpawnBoars();

        StartCoroutine("CheckToSpawnEnemies");

    }
    // Þegar enemy deyr
    public void EnemyDied(bool cannibal) {

        if(cannibal) {

            cannibal_Enemy_Count++;

            if(cannibal_Enemy_Count > initial_Cannibal_Count) {
                cannibal_Enemy_Count = initial_Cannibal_Count;
            }

        } else {

            boar_Enemy_Count++;

            if(boar_Enemy_Count > initial_Boar_Count) {
                boar_Enemy_Count = initial_Boar_Count;
            }

        }

    }
    // Hættir að spawna enemies
    public void StopSpawning() {
        StopCoroutine("CheckToSpawnEnemies");
    }
} 