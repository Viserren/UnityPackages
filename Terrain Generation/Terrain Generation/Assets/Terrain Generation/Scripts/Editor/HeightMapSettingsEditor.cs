using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TerrainGeneration.Editors
{
    [CustomEditor(typeof(HeightMapSettings))]
    public class HeightMapSettingsEditor : UpdateableDataEditor
    {
        HeightMapSettings heightMapSettings;

        NoiseSettings noiseSettings;

        private void OnEnable()
        {
            heightMapSettings = (HeightMapSettings)target;
            noiseSettings = heightMapSettings.noiseSettings;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            if (GUILayout.Button("Random Seed"))
            {
                noiseSettings.seed = Random.Range(0, 1073741824);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
