using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(VRWatchInteraction))]
public class WatchInteractionEditor : Editor
{
    VRWatch watch = null;
    int time;
    Vector2 timeVec;
    public override void OnInspectorGUI()
    {
        GUILayout.Label("|- WatchInteraction -|");
        base.OnInspectorGUI();

        GUILayout.Label("|- WatchInteractionEditor -|");
        watch = EditorGUILayout.ObjectField(watch, typeof(VRWatch), true) as VRWatch;


        if (GUILayout.Button("Trigger"))
        {
            ((VRWatchInteraction)target).EditorWatchTriggered(watch);
        }
        if (((VRWatchInteraction)target).interactingWatch != null)
        {
            GUILayout.Label("-- SetTime --");
            time = EditorGUILayout.IntField(time);
            if (GUILayout.Button("SetTime with int"))
            {
                ((VRWatchInteraction)target).SetTime(time);
            }
            timeVec = EditorGUILayout.Vector2Field("Vector",timeVec);
            //if (GUILayout.Button("SetTime with int"))
            //{
            //    int convertedTime = 
            //    ((VRWatchInteraction)target).SetTime();
            //}
            watch = EditorGUILayout.ObjectField(watch, typeof(VRWatch), true) as VRWatch;
        }
    }
}