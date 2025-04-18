﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DynamicJoystick))]
public class DynamicJoystickEditor : JoystickEditor
{
    private SerializedProperty moveThreshold;
    private SerializedProperty position;

    protected override void OnEnable()
    {
        base.OnEnable();
        moveThreshold = serializedObject.FindProperty("moveThreshold");
        position = serializedObject.FindProperty("position");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (background != null)
        {
            RectTransform backgroundRect = (RectTransform)background.objectReferenceValue;
            backgroundRect.anchorMax = Vector2.zero;
            backgroundRect.anchorMin = Vector2.zero;
            backgroundRect.pivot = center;
        }
    }

    protected override void DrawValues()
    {
        base.DrawValues();
        EditorGUILayout.PropertyField(moveThreshold, new GUIContent("Move Threshold", "The distance away from the center input has to be before the joystick begins to move."));
        EditorGUILayout.PropertyField(position, new GUIContent("Position", "The side of the screen that this joystick is on."));
    }
}