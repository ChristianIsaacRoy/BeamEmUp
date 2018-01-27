using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public Mesh itemMesh;
    public CapsuleCollider collider;

    public new string name;
    public int pointValue;    
}
