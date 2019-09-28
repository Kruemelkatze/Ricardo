using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Phone))]
public class PhoneEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Phone myScript = (Phone) target;
        if (GUILayout.Button("Reset Photo"))
        {
            myScript.ResetPhoto();
        }
        
        if (GUILayout.Button("Shoot Photo"))
        {
            myScript.ShootPhoto();
        }
    }
}