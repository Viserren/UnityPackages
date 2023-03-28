using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TerrainGeneration;

namespace TerrainGeneration.Editors
{
    [CustomEditor(typeof(MapPreview))]
    public class MapGeneratorEditor : Editor
    {
        private void OnEnable()
        {

        }

        public override void OnInspectorGUI()
        {
            MapPreview mapGenerator = (MapPreview)target;
            base.OnInspectorGUI();
            serializedObject.Update();
            if (GUILayout.Button("Generate"))
            {
                mapGenerator.DrawMepInEditor();
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
