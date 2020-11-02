using System;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;
using Microsoft.CSharp;
using UnityEngine;

public class PlayerShooting : ComponentSystem
{
    private Unity.Entities.EntityQuery BulletSpawner_Query;
    private Unity.Entities.EntityQuery vehicle_playerShip_Query;
    public struct GraphData : Unity.Entities.IComponentData
    {
        public int Shots;
    }

    protected override void OnCreate()
    {
        BulletSpawner_Query = GetEntityQuery(ComponentType.ReadOnly<BulletPrefab>());
        vehicle_playerShip_Query = GetEntityQuery(ComponentType.ReadOnly<Unity.Transforms.LocalToWorld>(), ComponentType.ReadOnly<Unity.Transforms.Rotation>(), ComponentType.ReadOnly<Unity.Transforms.Translation>(), ComponentType.ReadOnly<PlayerTag>());
        EntityManager.CreateEntity(typeof (GraphData));
        SetSingleton(new GraphData{Shots = 10});
    }

    protected override void OnUpdate()
    {
        GraphData graphData = GetSingleton<GraphData>();
        {
            Entities.With(BulletSpawner_Query).ForEach((Unity.Entities.Entity BulletSpawner_QueryEntity, ref BulletPrefab BulletSpawner_QueryBulletPrefab) =>
            {
                if (UnityEngine.Input.GetButton("Fire1"))
                {
                    {
                        var vehicle_playerShip_QueryEntities = vehicle_playerShip_Query.ToEntityArray(Allocator.TempJob);
                        var vehicle_playerShip_QueryTranslationArray = vehicle_playerShip_Query.ToComponentDataArray<Unity.Transforms.Translation>(Allocator.TempJob);
                        for (int vehicle_playerShip_QueryIdx = 0; vehicle_playerShip_QueryIdx < vehicle_playerShip_QueryEntities.Length; vehicle_playerShip_QueryIdx++)
                        {
                            var vehicle_playerShip_QueryEntity = vehicle_playerShip_QueryEntities[vehicle_playerShip_QueryIdx];
                            var vehicle_playerShip_QueryTranslation = vehicle_playerShip_QueryTranslationArray[vehicle_playerShip_QueryIdx];
                            int Index = 0;
                            for (; (Index < graphData.Shots); Index++)
                            {
                                Unity.Entities.Entity entity = PostUpdateCommands.Instantiate(BulletSpawner_QueryBulletPrefab.prefab);
                                PostUpdateCommands.SetComponent<Unity.Transforms.Translation>(entity, new Unity.Transforms.Translation{Value = new Unity.Mathematics.float3{x = vehicle_playerShip_QueryTranslation.Value.x, y = 0F, z = (vehicle_playerShip_QueryTranslation.Value.z + 1F)}});
                                PostUpdateCommands.SetComponent<BulletDiretion>(entity, new BulletDiretion{X = math.mul(math.sin(math.mul(((30F / -2) + (Index * (30F / (graphData.Shots - 1)))), (3.1415925F / 180))), -1), Y = 0F, Z = math.cos(math.mul(((30F / -2) + (Index * (30F / (graphData.Shots - 1)))), (3.1415925F / 180)))});
                            }
                        }

                        vehicle_playerShip_QueryTranslationArray.Dispose();
                        vehicle_playerShip_QueryEntities.Dispose();
                    }
                }
            }

            );
        }
    }
}