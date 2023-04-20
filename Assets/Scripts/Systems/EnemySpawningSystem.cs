using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public partial class EnemySpawningSystem : SystemBase
{
    protected override void OnUpdate()
    {
        EntityCommandBuffer ecb = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>()
            .CreateCommandBuffer(World.Unmanaged);

        foreach(var spawner in SystemAPI.Query<RefRW<EnemySpawner>>())
        {
            spawner.ValueRW.timer -= SystemAPI.Time.DeltaTime;
            if(spawner.ValueRW.timer < 0)
            {
                spawner.ValueRW.timer = spawner.ValueRW.spawnInterval;
                Entity e = ecb.Instantiate(spawner.ValueRO.enemyEntity);
            }
        }
    }
}
