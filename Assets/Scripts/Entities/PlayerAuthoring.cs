using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour
{
    public float moveSpeed;
    
}
public class PlayerBaker : Baker<PlayerAuthoring>
{
    public override void Bake(PlayerAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(entity,
            new Speed
            {
                value = authoring.moveSpeed
            });
        AddComponent(entity, new PlayerTag());

    }
}

public struct PlayerTag : IComponentData
{
}
