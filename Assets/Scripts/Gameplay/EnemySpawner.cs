using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class EnemySpawner : MonoBehaviour
{
    public Enemy enemyPrefab;
    public EnemySO[] enemySOs;
    public Transform[] spawnPoints;
    public float spawnInterval;
    public Queue<Enemy> enemyPool = new();
    public static EnemySpawner instance;

    public int spawnThreshold;


    [SerializeField]
    private PixelPerfectCamera pixelPerfectCamera;
    private int spawnedCounter;
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
        EnemySO enemySO = GetSpawnableEnemy();
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
        spawnedCounter++;
    }

    private EnemySO GetSpawnableEnemy()
    {
        int spawnLevel = Mathf.Min(spawnedCounter / spawnThreshold + 1, enemySOs.Length - 1);
        pixelPerfectCamera.assetsPPU = 32 - spawnLevel;
        transform.localScale = new Vector3(1,1,1) * (1 + spawnLevel * 0.1f);
        return enemySOs[Random.Range(0, spawnLevel)];
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
