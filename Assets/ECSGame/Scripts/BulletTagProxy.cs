using System;
using System.ComponentModel;
using Unity.Entities;
using UnityEngine;
using VisualScripting.Entities.Runtime;
using System.Collections.Generic;
[Serializable, ComponentEditor]
public struct BulletTag : ISharedComponentData, IEquatable<BulletTag>
{
    public bool Equals(BulletTag other)
    {
        return true;
    }

    public override int GetHashCode()
    {
        int hash = 0;
        return hash;
    }
}

[AddComponentMenu("Visual Scripting Components/BulletTag")]
class BulletTagProxy : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
{
    public void Convert(Unity.Entities.Entity entity, Unity.Entities.EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddSharedComponentData(entity, new BulletTag { });
    }

    public void DeclareReferencedPrefabs(List<UnityEngine.GameObject> referencedPrefabs)
    {
    }
}