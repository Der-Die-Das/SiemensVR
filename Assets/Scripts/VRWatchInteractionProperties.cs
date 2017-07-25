using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if (UNITY_EDITOR)
using UnityEditor;
#endif
public class VRWatchInteractionProperties : ScriptableObject
{
    public GameObject watchPrefab;

    public float FadeSpeed = 1;
    public float TurnSpeed = 4;

    public Material normalMaterial;
    public Material highlightedMaterial;
    public int normalSize;
    public int highlightedSize;
#if (UNITY_EDITOR)
    [MenuItem("ScriptableObjects/VRWatchInteractionProperties")]
    static void Init()
    {
        VRWatchInteractionProperties data = ScriptableObject.CreateInstance<VRWatchInteractionProperties>();
        AssetDatabase.CreateAsset(data, "Assets/VRWatchInteractionProperties.asset");
    }
#endif
}
