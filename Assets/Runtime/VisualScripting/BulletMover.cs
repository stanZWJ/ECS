using System;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;
using Microsoft.CSharp;
using UnityEngine;

public class BulletMover : ComponentSystem
{
    private Unity.Entities.EntityQuery Bullet_Query;
    protected override void OnCreate()
    {
        Bullet_Query = GetEntityQuery(ComponentType.ReadWrite<Unity.Transforms.Translation>(), ComponentType.ReadOnly<BulletDiretion>(), ComponentType.ReadOnly<Unity.Transforms.Rotation>(), ComponentType.ReadOnly<BulletTag>());
    }

    protected override void OnUpdate()
    {
        {
            Entities.With(Bullet_Query).ForEach((Unity.Entities.Entity Bullet_QueryEntity, ref BulletDiretion Bullet_QueryBulletDiretion, ref Unity.Transforms.Translation Bullet_QueryTranslation) =>
            {
                Bullet_QueryTranslation.Value.x += Bullet_QueryBulletDiretion.X;
                Bullet_QueryTranslation.Value.y += Bullet_QueryBulletDiretion.Y;
                Bullet_QueryTranslation.Value.z += Bullet_QueryBulletDiretion.Z;
            }

            );
        }
    }
}