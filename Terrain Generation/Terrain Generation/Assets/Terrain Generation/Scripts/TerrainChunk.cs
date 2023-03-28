using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerrainGeneration
{
    public class TerrainChunk
    {
        const float colliderGenerationDistanceThreshold = 5;
        public event Action<TerrainChunk, bool> OnVisibilityChanged;

        public Vector2 coord;
        GameObject meshObject;
        Vector2 sampleCentre;
        Bounds bounds;

        MeshRenderer meshRenderer;
        MeshFilter meshFilter;
        MeshCollider meshCollider;

        HeightMap heightMap;
        bool heightMapRecieved;

        LODInfo[] detailLevels;
        LODMesh[] lodMeshes;
        int colliderLODIndex;
        int previousLODLevel = -1;
        bool hasSetCollider;
        float maxViewDistance;

        HeightMapSettings heightMapSettings;
        MeshSettings meshSettings;
        Transform viewer;

        public TerrainChunk(Vector2 coord, HeightMapSettings heightMapSettings, MeshSettings meshSettings, LODInfo[] detailLevels, int colliderLODIndex, Transform parent, Transform viewer, Material material)
        {
            this.coord = coord;
            this.detailLevels = detailLevels;
            this.colliderLODIndex = colliderLODIndex;
            this.heightMapSettings = heightMapSettings;
            this.meshSettings = meshSettings;
            this.viewer = viewer;

            sampleCentre = coord * meshSettings.meshWorldSize / meshSettings.meshScale;
            Vector2 position = coord * meshSettings.meshWorldSize;
            bounds = new Bounds(position, Vector2.one * meshSettings.meshWorldSize);

            meshObject = new GameObject("Terrain Chunk");
            meshRenderer = meshObject.AddComponent<MeshRenderer>();
            meshFilter = meshObject.AddComponent<MeshFilter>();
            meshCollider = meshObject.AddComponent<MeshCollider>();
            meshRenderer.material = material;

            meshObject.transform.position = new Vector3(position.x, 0, position.y);
            meshObject.transform.parent = parent;
            SetVisible(false);

            lodMeshes = new LODMesh[detailLevels.Length];
            for (int i = 0; i < detailLevels.Length; i++)
            {
                lodMeshes[i] = new LODMesh(detailLevels[i].lod);
                lodMeshes[i].updateCallback += UpdateTerrainChunk;
                if (i == colliderLODIndex)
                {
                    lodMeshes[i].updateCallback += UpdateCollisionMesh;
                }
            }

            maxViewDistance = detailLevels[detailLevels.Length - 1].visibleDistanceThreshold;
        }

        void OnHeightMapRecieved(object heightMapObject)
        {
            this.heightMap = (HeightMap)heightMapObject;
            heightMapRecieved = true;

            UpdateTerrainChunk();
        }

        public void Load()
        {
            ThreadedDataRequester.RequestData(() => HeightMapGenerator.GenerateHightMap(meshSettings.numberVertsPerLine, meshSettings.numberVertsPerLine, heightMapSettings, sampleCentre), OnHeightMapRecieved);
        }

        Vector2 ViewerPosition
        {
            get
            {
                return new Vector2(viewer.position.x, viewer.position.z);
            }
        }

        public void UpdateTerrainChunk()
        {
            if (heightMapRecieved)
            {
                float viewerDistanceFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(ViewerPosition));

                bool wasVisible = IsVisible();
                bool visible = viewerDistanceFromNearestEdge <= maxViewDistance;

                if (visible)
                {
                    int lodIndex = 0;

                    for (int i = 0; i < detailLevels.Length - 1; i++)
                    {
                        if (viewerDistanceFromNearestEdge > detailLevels[i].visibleDistanceThreshold)
                        {
                            lodIndex = i + 1;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (lodIndex != previousLODLevel)
                    {
                        LODMesh lodMesh = lodMeshes[lodIndex];
                        if (lodMesh.hasMesh)
                        {
                            previousLODLevel = lodIndex;
                            meshFilter.mesh = lodMesh.mesh;
                        }
                        else if (!lodMesh.hasReqestedMesh)
                        {
                            lodMesh.RequestMesh(heightMap,meshSettings);
                        }
                    }
                }
                if (wasVisible != visible)
                {
                    SetVisible(visible);

                    if (OnVisibilityChanged != null)
                    {
                        OnVisibilityChanged(this, visible);
                    }
                }
            }

        }

        public void UpdateCollisionMesh()
        {
            if (!hasSetCollider)
            {
                float sqrDistanceFromViewerToEdge = bounds.SqrDistance(ViewerPosition);

                if (sqrDistanceFromViewerToEdge < detailLevels[colliderLODIndex].sqrVisibleDistanceThreshold)
                {
                    if (!lodMeshes[colliderLODIndex].hasReqestedMesh)
                    {
                        lodMeshes[colliderLODIndex].RequestMesh(heightMap,meshSettings);
                    }
                }

                if (sqrDistanceFromViewerToEdge < colliderGenerationDistanceThreshold * colliderGenerationDistanceThreshold)
                {
                    if (lodMeshes[colliderLODIndex].hasMesh)
                    {
                        meshCollider.sharedMesh = lodMeshes[colliderLODIndex].mesh;
                        hasSetCollider = true;
                    }
                }
            }

        }

        public void SetVisible(bool visible)
        {
            meshObject.SetActive(visible);
        }

        public bool IsVisible()
        {
            return meshObject.activeSelf;
        }
    }

    class LODMesh
    {
        public Mesh mesh;
        public bool hasReqestedMesh;
        public bool hasMesh;
        int lod;
        public event System.Action updateCallback;

        public LODMesh(int lod)
        {
            this.lod = lod;
        }

        void OnMeshDataReceived(object meshDataObject)
        {
            mesh = ((MeshData)meshDataObject).CreateMesh();
            hasMesh = true;

            updateCallback();
        }

        public void RequestMesh(HeightMap heightMap, MeshSettings meshSettings)
        {
            hasReqestedMesh = true;
            ThreadedDataRequester.RequestData(() => MeshGenerator.GenerateTerrainMesh(heightMap.values, meshSettings, lod), OnMeshDataReceived);
        }
    }
}
