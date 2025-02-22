﻿using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EnumFlagAttribute))]
public class EnumFlagDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EnumFlagAttribute flagSettings = (EnumFlagAttribute)attribute;
        Enum targetEnum = (Enum)Enum.ToObject(fieldInfo.FieldType, property.intValue);

        GUIContent propName = new GUIContent(flagSettings.enumName);
        if (string.IsNullOrEmpty(flagSettings.enumName))
            propName = label;

        EditorGUI.BeginProperty(position, label, property);
        Enum enumNew = EditorGUI.EnumMaskField(position, propName, targetEnum);
        property.intValue = (int)Convert.ChangeType(enumNew, fieldInfo.FieldType);
        EditorGUI.EndProperty();
    }
}