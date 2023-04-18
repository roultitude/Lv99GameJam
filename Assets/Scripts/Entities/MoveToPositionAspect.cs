using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public readonly partial struct MoveToPositionAspect : IAspect
{
    private readonly Entity entity;

    private readonly RefRO<Speed> speed;
    private readonly RefRW<LocalTransform> transform;
    private readonly RefRO<EnemyTag> enemyTag;

    public void MoveToPos(float deltaTime, float3 pos)
    {
        
        float3 dir = math.normalize(pos - transform.ValueRW.Position);

        transform.ValueRW.Position += dir * deltaTime * speed.ValueRO.value;
        
    }

}
