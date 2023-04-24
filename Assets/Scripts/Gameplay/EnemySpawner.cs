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
    public Queue<Enemy> enemyPool = new();
    public static EnemySpawner instance;

    private float timer;

    private void Awake()
    {   
        if(instance == null)
        {
            instance = this;
        }
        timer = spawnInterval;
    }

    public void SpawnEnemy()
    {
        Vector3 spawnPos = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        if (enemyPool.Count > 0)
        {
            Enemy newEnemy = enemyPool.Dequeue();
            newEnemy.Setup(enemySO);
            newEnemy.gameObject.transform.position = spawnPos;   
        }
        else
        {
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity).Setup(enemySO);
        }
    }

    public void AddToPool(Enemy toAdd)
    {
        enemyPool.Enqueue(toAdd);
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
