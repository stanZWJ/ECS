using System;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;
using Microsoft.CSharp;
using UnityEngine;

public class PlayerControl : ComponentSystem
{
    private Unity.Entities.EntityQuery vehicle_playerShip_Query;
    public struct GraphData : Unity.Entities.IComponentData
    {
        public float Speed;
    }

    protected override void OnCreate()
    {
        vehicle_playerShip_Query = GetEntityQuery(ComponentType.ReadWrite<Unity.Transforms.Translation>(), ComponentType.ReadOnly<Unity.Transforms.Rotation>(), ComponentType.ReadOnly<PlayerTag>());
        EntityManager.CreateEntity(typeof (GraphData));
        SetSingleton(new GraphData{Speed = 8F});
    }

    protected override void OnUpdate()
    {
        GraphData graphData = GetSingleton<GraphData>();
        {
            Entities.With(vehicle_playerShip_Query).ForEach((Unity.Entities.Entity vehicle_playerShip_QueryEntity, ref Unity.Transforms.Translation vehicle_playerShip_QueryTranslation) =>
            {
                vehicle_playerShip_QueryTranslation.Value.x = math.clamp((vehicle_playerShip_QueryTranslation.Value.x + math.mul(math.mul(graphData.Speed, Time.deltaTime), UnityEngine.Input.GetAxis("Horizontal"))), -7F, 7F);
                vehicle_playerShip_QueryTranslation.Value.y = 0F;
                vehicle_playerShip_QueryTranslation.Value.z = math.clamp((math.mul(math.mul(graphData.Speed, Time.deltaTime), UnityEngine.Input.GetAxis("Vertical")) + vehicle_playerShip_QueryTranslation.Value.z), -12F, 12F);
            }

            );
        }
    }
}