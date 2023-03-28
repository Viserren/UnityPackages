using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TerrainGeneration.Editors
{
    [CustomEditor(typeof(NoiseSettings))]
    public class NoiseEditor : UpdateableDataEditor
    {
        SerializedProperty seed;

        private void OnEnable()
        {
            seed = serializedObject.FindProperty("seed");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            if (GUILayout.Button("Random Seed"))
            {
                seed.intValue = Random.Range(0, 1073741824);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
