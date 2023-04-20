using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class EnemySpawnerAuthoring : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval;

}
public class EnemySpawnerBaker : Baker<EnemySpawnerAuthoring>
{
    public override void Bake(EnemySpawnerAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(entity,
            new EnemySpawner
            {
                enemyEntity = GetEntity(authoring.enemyPrefab, TransformUsageFlags.Dynamic),
                spawnInterval = authoring.spawnInterval
            });

    }
}

public struct EnemySpawner : IComponentData
{
    public Entity enemyEntity;
    public float spawnInterval;
    public float timer;
}
