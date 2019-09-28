using UnityEditor;
using UnityEngine;

#if (UNITY_EDITOR)

[CustomEditor(typeof(Phone))]
public class PhoneEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (!Application.isPlaying)
            return;

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

#endif