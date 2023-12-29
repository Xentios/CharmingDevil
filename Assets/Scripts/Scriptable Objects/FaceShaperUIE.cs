using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

// IngredientDrawerUIE
[CustomPropertyDrawer(typeof(BlendShapes))]
public class FaceShaperUIE : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        // Create property container element.
        var container = new VisualElement();

        // Create property fields.
        var amountField = new PropertyField(property.FindPropertyRelative("name"));
        var unitField = new PropertyField(property.FindPropertyRelative("index"));
        var nameField = new PropertyField(property.FindPropertyRelative("value"));
        RangeAttribute range = attribute as RangeAttribute;
       
        // Add fields to the container.
        container.Add(amountField);
        container.Add(unitField);
        container.Add(nameField);

        return container;
    }

    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // First get the attribute since it contains the range for the slider
        RangeAttribute range = attribute as RangeAttribute;

        // Now draw the property as a Slider or an IntSlider based on whether it's a float or integer.
        if (property.propertyType == SerializedPropertyType.Float)
            EditorGUI.Slider(position, property, range.min, range.max, label);
        else if (property.propertyType == SerializedPropertyType.Integer)
            EditorGUI.IntSlider(position, property, Convert.ToInt32(range.min), Convert.ToInt32(range.max), label);
        else
            EditorGUI.LabelField(position, label.text, "Use Range with float or int.");
    }
}