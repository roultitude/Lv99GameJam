using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct EnemyMoveISystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        Entity playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
        
        foreach(MoveToPositionAspect moveToPositionAspect in SystemAPI.Query<MoveToPositionAspect>())
        {
            moveToPositionAspect.MoveToPos(SystemAPI.Time.DeltaTime, SystemAPI.GetComponent<LocalTransform>(playerEntity).Position);
        }
    }
}
