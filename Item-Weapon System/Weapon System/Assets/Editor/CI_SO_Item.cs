using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SO_Item), true)]
public class CI_SO_Item : Editor
{
    private GUILayoutOption[] spriteAreaOptions;
    SerializedProperty spName;
    SerializedProperty spDescription;
    SerializedProperty spSprite;
    SerializedProperty spItemId;
    public virtual void OnEnable()
    {
        spName = serializedObject.FindProperty("itemName");
        spSprite = serializedObject.FindProperty("itemSprite");
        spDescription = serializedObject.FindProperty("description");
        spItemId = serializedObject.FindProperty("itemId");



        spriteAreaOptions = new GUILayoutOption[]
        {
            GUILayout.Height(100),
            GUILayout.Width(100),
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        //GUILayout.BeginHorizontal("BOX");
        EditorGUILayout.PropertyField(spSprite, spriteAreaOptions);
        //GUILayout.BeginVertical("BOX");
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(spName);
        EditorGUILayout.PropertyField(spDescription);
        EditorGUILayout.PropertyField(spItemId);
        EditorGUILayout.Space(); EditorGUILayout.Space(); EditorGUILayout.Space(); EditorGUILayout.Space();
        //GUILayout.EndVertical();
        //GUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties();
    }
}
