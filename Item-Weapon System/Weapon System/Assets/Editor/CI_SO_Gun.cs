using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SO_Gun), true)]
public class CI_SO_Gun : CI_SO_Weapon
{
    SerializedProperty spReloadSpeed;
    SerializedProperty spFireRange;
    SerializedProperty spFireSpeed;
    SerializedProperty spRecoilAmount;
    public override void OnEnable()
    {
        base.OnEnable();
        spReloadSpeed = serializedObject.FindProperty("reloadSpeed");
        spFireRange = serializedObject.FindProperty("fireRange");
        spFireSpeed = serializedObject.FindProperty("fireSpeed");
        spRecoilAmount = serializedObject.FindProperty("recoilAmount");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.PropertyField(spReloadSpeed);
        EditorGUILayout.PropertyField(spFireRange);
        EditorGUILayout.PropertyField(spFireSpeed);
        EditorGUILayout.PropertyField(spRecoilAmount);
        serializedObject.ApplyModifiedProperties();
    }
}
