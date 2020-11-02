using System;
using System.ComponentModel;
using Unity.Entities;
using UnityEngine;
using VisualScripting.Entities.Runtime;
using System.Collections.Generic;
[Serializable, ComponentEditor]
public struct BulletDiretion : IComponentData
{
    public float X;
    public float Y;
    public float Z;
}

[AddComponentMenu("Visual Scripting Components/BulletDiretion")]
class BulletDiretionProxy : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
{
    public float X;
    public float Y;
    public float Z;

    public void Convert(Unity.Entities.Entity entity, Unity.Entities.EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new BulletDiretion { X = X, Y = Y, Z = Z });
    }

    public void DeclareReferencedPrefabs(List<UnityEngine.GameObject> referencedPrefabs)
    {
    }
}