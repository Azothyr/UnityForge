#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PrefabDataList))]
public class PrefabDataListEditor : Editor
{
    public GameObject prefabToAdd;
    public int priority;
    private bool _showPrefabList;

    private void OnEnable()
    {
        _showPrefabList = SessionState.GetBool("showPrefabList", false);
    }

    public override void OnInspectorGUI()
    {
        PrefabDataList myPrefabList = (PrefabDataList)target;

        prefabToAdd = (GameObject)EditorGUILayout.ObjectField("Prefab To Add:", prefabToAdd, typeof(GameObject), false);
        priority = EditorGUILayout.IntField("Priority:", priority);

        if (GUILayout.Button("Add Prefab"))
        {
            if (prefabToAdd != null)
            {
                var newPrefabData = CreateInstance<PrefabData>();

                newPrefabData.obj = prefabToAdd;
                newPrefabData.priority = priority;

                myPrefabList.prefabDataList.Add(newPrefabData);

                AssetDatabase.AddObjectToAsset(newPrefabData, myPrefabList);
                AssetDatabase.SaveAssets();
            }
            else
            {
                Debug.Log("No prefab selected");
            }
        }

        if (GUILayout.Button("Clear List"))
        {
            myPrefabList.prefabDataList.Clear();
            AssetDatabase.SaveAssets();
        }

        if (myPrefabList.prefabDataList.Count == 0) return;

        EditorGUILayout.Space();

        _showPrefabList = EditorGUILayout.Foldout(_showPrefabList, "Prefab Data List Items:");
        SessionState.SetBool("showPrefabList", _showPrefabList);

        if (_showPrefabList)
        {
            int counter = 0;
            PrefabData elementToRemove = null;

            EditorGUI.indentLevel++;

            EditorGUILayout.BeginVertical(GUI.skin.box); // Begin a vertical group with a box around it

            foreach (PrefabData prefabData in myPrefabList.prefabDataList)
            {
                if (prefabData == null)
                {
                    elementToRemove = prefabData;
                    continue;
                }

                EditorGUILayout.BeginVertical(GUI.skin.box); // Box for each element

                EditorGUILayout.BeginHorizontal();
                var spacer = GUILayout.Width(265);
                EditorGUILayout.LabelField("Element [" + counter + "]", spacer);
                EditorGUILayout.LabelField("Priority: " + prefabData.priority, spacer);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(prefabData.prefab.name);
                if (GUILayout.Button("Remove"))
                {
                    elementToRemove = prefabData;
                }

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.EndVertical(); // End box for each element

                counter++;
            }

            EditorGUILayout.EndVertical(); // End of vertical group

            EditorGUI.indentLevel--;

            if (elementToRemove != null)
            {
                myPrefabList.prefabDataList.Remove(elementToRemove);
                DestroyImmediate(elementToRemove, true);
                AssetDatabase.SaveAssets();
            }
        }
    }
}
#endif