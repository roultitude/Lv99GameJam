using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy enemyPrefab;
    public EnemySO enemySO;
    public Transform[] spawnPoints;
    public float spawnInterval;

    private float timer;

    private void Awake()
    {
        timer = spawnInterval;
    }

    public void SpawnEnemy()
    {
        Vector3 spawnPos = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        Instantiate(enemyPrefab, spawnPos,Quaternion.identity).Setup(enemySO);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0) 
        { 
            SpawnEnemy();
            timer = spawnInterval; 
        }
    }
}
