using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class VRWatchInteractionProperties : ScriptableObject
{
    public GameObject watchPrefab;

    public float FadeSpeed = 1;
    public float TurnSpeed = 4;

    public Material normalMaterial;
    public Material highlightedMaterial;
    public int normalSize;
    public int highlightedSize;

    [MenuItem("ScriptableObjects/VRWatchInteractionProperties")]
    static void Init()
    {
        VRWatchInteractionProperties data = ScriptableObject.CreateInstance<VRWatchInteractionProperties>();
        AssetDatabase.CreateAsset(data, "Assets/VRWatchInteractionProperties.asset");
    }
}
