using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(PhoneSpawner))]
public class PhoneSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PhoneSpawner myScript = (PhoneSpawner) target;
        if (GUILayout.Button("Spawn Phone"))
        {
            myScript.SpawnPhone();
        }
        
        if (GUILayout.Button("Clear Phones"))
        {
            myScript.RemoveAllPhones();
        }
    }
}