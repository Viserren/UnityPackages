using System.Collections;
using System.Collections.Generic;
using TerrainGeneration;
using UnityEngine;

namespace TerrainGeneration
{
    public class MapPreview : MonoBehaviour
    {
        public Renderer textureRenderer;
        public MeshFilter meshFilter;
        public MeshRenderer meshRenderer;


        public enum DrawMode { NoiseMap, DrawMesh, FallOffMap };
        public DrawMode drawMode;

        public MeshSettings meshSettings;
        public HeightMapSettings heightMapSettings;

        public Material terrainMaterial;


        [Range(0, MeshSettings.numberSupportedLODs - 1)]
        public int editorPreviewLOD;

        public void DrawTexture(Texture2D texture)
        {
            textureRenderer.sharedMaterial.mainTexture = texture;
            textureRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height) / 10f;
            textureRenderer.gameObject.SetActive(true);
            meshFilter.gameObject.SetActive(false);
        }
        public void DrawMesh(MeshData meshData)
        {
            meshFilter.sharedMesh = meshData.CreateMesh();
            textureRenderer.gameObject.SetActive(false);
            meshFilter.gameObject.SetActive(true);
        }

        public void DrawMepInEditor()
        {
            HeightMap heightMap = HeightMapGenerator.GenerateHightMap(meshSettings.numberVertsPerLine, meshSettings.numberVertsPerLine, heightMapSettings, Vector2.zero);
            if (drawMode == DrawMode.NoiseMap)
            {
                DrawTexture(TextureGenerator.TextureFromHeightMap(heightMap));
            }
            else if (drawMode == DrawMode.DrawMesh)
            {
                DrawMesh(MeshGenerator.GenerateTerrainMesh(heightMap.values, meshSettings, editorPreviewLOD));
            }
            else if (drawMode == DrawMode.FallOffMap)
            {
                DrawTexture(TextureGenerator.TextureFromHeightMap(new HeightMap(FallOffNoiseGenerator.GenerateFallOffMap(meshSettings.numberVertsPerLine), 0, 1)));
            }
        }

        void OnValuesUpdated()
        {
            if (!Application.isPlaying)
            {
                DrawMepInEditor();
            }
        }
        private void OnValidate()
        {
            if (meshSettings != null)
            {
                meshSettings.OnValuesUpdate -= OnValuesUpdated;
                meshSettings.OnValuesUpdate += OnValuesUpdated;
            }
            if (heightMapSettings != null)
            {
                heightMapSettings.OnValuesUpdate -= OnValuesUpdated;
                heightMapSettings.OnValuesUpdate += OnValuesUpdated;
            }
        }
    }
}
