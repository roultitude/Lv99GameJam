using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class PlayerEntityTracker : MonoBehaviour
{
    private Entity playerEntity;


    private void LateUpdate()
    {
        if(playerEntity != Entity.Null)
        {
            Vector3 followPos = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<LocalToWorld>(playerEntity).Position;
            transform.position = followPos;
        } else
        {
            playerEntity = GetPlayerEntity();
        }
    }

    private Entity GetPlayerEntity()
    {
        EntityQuery playerTagEntityQuery = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery(typeof(PlayerTag));
        NativeArray<Entity> entityNativeArray = playerTagEntityQuery.ToEntityArray(Unity.Collections.Allocator.Temp);

        if(entityNativeArray.Length > 0)
        {
            return entityNativeArray[0];
        }
        return Entity.Null;
    }
}
