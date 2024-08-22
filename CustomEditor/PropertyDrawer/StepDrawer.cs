#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(StepAttribute))]
public class StepDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var stepAttribute = attribute as StepAttribute;

        if (property.propertyType == SerializedPropertyType.Float)
        {
            EditorGUI.BeginChangeCheck();
            float newValue = EditorGUI.Slider(position, label, property.floatValue, property.floatValue - stepAttribute.Step, property.floatValue + stepAttribute.Step);
            if (EditorGUI.EndChangeCheck())
            {
                property.floatValue = Mathf.Round(newValue / stepAttribute.Step) * stepAttribute.Step;
            }
        }
        else
        {
            EditorGUI.LabelField(position, label.text, "Use Step with float.");
        }
    }
}
#endif