using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class VRFireExtinguisherButtonInteractionProperties : ScriptableObject {
    public GameObject buttonPrefab;
    public float turnSpeed;

	[MenuItem("ScriptableObjects/VRFireExtinguisherInteractionProperties")]
    static void Init()
    {
        VRFireExtinguisherButtonInteractionProperties data = CreateInstance<VRFireExtinguisherButtonInteractionProperties>();
        AssetDatabase.CreateAsset(data, "Assets/VRFireExtinguisherInteractionProperties.asset");
    }
}
