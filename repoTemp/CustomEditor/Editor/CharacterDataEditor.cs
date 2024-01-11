using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CharacterData), true)]
public class CharacterDataEditor : Editor
{
    SerializedProperty scriptProperty;
    bool dataFoldout = true; // State of the foldout for Data Elements, starts as folded
    bool builtInFoldout = true; // State of the foldout for Built-in Elements, starts as folded
    bool enemyDataFoldout = true; // State of the foldout for Enemy Specific Data, starts as folded

    private void OnEnable()
    {
        scriptProperty = serializedObject.FindProperty("m_Script");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.PropertyField(scriptProperty);
        EditorGUI.EndDisabledGroup();

        CharacterData characterData = (CharacterData)target;

        // Enemy Specific Data Foldout
        if (target.GetType() == typeof(EnemyData))
        {
            EnemyData enemyData = (EnemyData)characterData;
            enemyDataFoldout = EditorGUILayout.Foldout(enemyDataFoldout, "Enemy Specific Data", true);
            if (enemyDataFoldout)
            {
                enemyData.prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", enemyData.prefab, typeof(GameObject), false);
                enemyData.speedDelta = EditorGUILayout.IntSlider("Speed Delta", enemyData.speedDelta, -100, 100);
                enemyData.scoreValue = EditorGUILayout.IntField("Score Value", enemyData.scoreValue);
                enemyData.currencyValue = EditorGUILayout.IntField("Wealth Value", enemyData.currencyValue);
            }
            GUILayout.Space(5f); // Add space after the section
        }

        // Data Elements Foldout
        dataFoldout = EditorGUILayout.Foldout(dataFoldout, "Data Elements", true);
        if (dataFoldout)
        {
            DrawCharacterDataFields(characterData);
        }
        GUILayout.Space(5f); // Add space after the section

        // Built-in Elements Foldout
        builtInFoldout = EditorGUILayout.Foldout(builtInFoldout, "Built-in Elements", true);
        if (builtInFoldout)
        {
            characterData.knockBackPower = EditorGUILayout.FloatField("Knock Back Power", characterData.knockBackPower);
            characterData.knockBackResistance = EditorGUILayout.FloatField("Knock Back Resistance", characterData.knockBackResistance);
        }
        GUILayout.Space(5f); // Add space after the section

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawCharacterDataFields(CharacterData characterData)
    {
        characterData.id = (ID)EditorGUILayout.ObjectField("ID", characterData.id, typeof(ID), false);
        characterData.speed = (FloatData)EditorGUILayout.ObjectField("Speed", characterData.speed, typeof(FloatData), false);
        characterData.topSpeed = (FloatData)EditorGUILayout.ObjectField("Top Speed", characterData.topSpeed, typeof(FloatData), false);
        characterData.canRun = (BoolData)EditorGUILayout.ObjectField("Can Run", characterData.canRun, typeof(BoolData), false);
        characterData.gameOver = (BoolData)EditorGUILayout.ObjectField("Game Over", characterData.gameOver, typeof(BoolData), false);
    }
}
