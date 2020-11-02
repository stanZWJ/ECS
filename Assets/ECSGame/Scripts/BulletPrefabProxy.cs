using System;
using System.ComponentModel;
using Unity.Entities;
using UnityEngine;
using VisualScripting.Entities.Runtime;
using System.Collections.Generic;
[Serializable, ComponentEditor]
public struct BulletPrefab : IComponentData
{
    public Unity.Entities.Entity prefab;
}

[AddComponentMenu("Visual Scripting Components/BulletPrefab")]
class BulletPrefabProxy : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
{
    public UnityEngine.GameObject prefab;

    public void Convert(Unity.Entities.Entity entity, Unity.Entities.EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new BulletPrefab { prefab = conversionSystem.GetPrimaryEntity(prefab) });
    }

    public void DeclareReferencedPrefabs(List<UnityEngine.GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(prefab);
    }
}