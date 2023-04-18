using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class PlayerInputMovementSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entity playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();

        if (playerEntity == null)
        {
            Debug.LogError("NO PLAYER, CANT MOVE????");
            return;
        }

        RefRW<LocalTransform> playerTransform = SystemAPI.GetComponentRW<LocalTransform>(playerEntity, false);

        float3 moveByDir = math.normalizesafe(new float3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0)
            , float3.zero);

        playerTransform.ValueRW.Position += moveByDir
            * SystemAPI.Time.DeltaTime * SystemAPI.GetComponent<Speed>(playerEntity).value;

    }
}
