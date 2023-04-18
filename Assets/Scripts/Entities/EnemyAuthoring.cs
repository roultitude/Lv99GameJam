using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class EnemyAuthoring : MonoBehaviour
{
    public float moveSpeed;

}
public class EnemyBaker : Baker<EnemyAuthoring>
{
    public override void Bake(EnemyAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(entity,
            new Speed
            {
                value = authoring.moveSpeed
            });
        AddComponent(entity, new EnemyTag());

    }
}

public struct EnemyTag : IComponentData
{
}
