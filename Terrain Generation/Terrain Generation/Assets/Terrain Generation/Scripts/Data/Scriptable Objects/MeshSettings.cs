using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerrainGeneration
{
    [CreateAssetMenu(fileName = "newMeshSettings", menuName = "Terrain Generation/Scriptable Objects/Mesh Settings")]
    public class MeshSettings : UpdateableData
    {
        public const int numberSupportedLODs = 5;
        public const int numberSupportedChunkSizes = 9;
        public static readonly int[] supportedChunkSizes = { 48, 72, 96, 120, 144, 168, 192, 216, 240 };

        public float meshScale = 2f;


        [Range(0f, numberSupportedChunkSizes - 1)]
        public int chunkSizeIndex;

        // Number of verts per line of mesh rendered at LOD = 0. Includes the 2 extra verts that are excluded from final mesh, but used for calculating mesh.
        public int numberVertsPerLine
        {
            get
            {
                return supportedChunkSizes[chunkSizeIndex] + 5;
            }
        }

        public float meshWorldSize
        {
            get
            {
                return (numberVertsPerLine - 3) * meshScale;
            }
        }
    }
}
