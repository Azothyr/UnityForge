using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MonoBehaviour), true)]
public class ButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        INeedButton myScript = target as INeedButton;

        if (myScript != null)
        {
            if (GUILayout.Button(myScript.GetButtonName()))
            {
                myScript.ButtonAction();
            }
        }
    }
}