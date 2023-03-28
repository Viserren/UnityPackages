using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerrainGeneration
{
    public static class MeshGenerator
    {
        public static MeshData GenerateTerrainMesh(float[,] heightMap, MeshSettings meshSettings, int levelOfDetail)
        {
            int skipIncrement = (levelOfDetail == 0) ? 1 : levelOfDetail * 2;
            int numberVertsPerLine = meshSettings.numberVertsPerLine;

            Vector2 topLeft = new Vector2(-1, 1) * meshSettings.meshWorldSize / 2f;

            MeshData meshData = new MeshData(numberVertsPerLine, skipIncrement);
            int[,] vertextIndicesMap = new int[numberVertsPerLine, numberVertsPerLine];
            int meshVertexIndex = 0;
            int outOfMeshVertexIndex = -1;

            for (int y = 0; y < numberVertsPerLine; y++)
            {
                for (int x = 0; x < numberVertsPerLine; x++)
                {
                    bool isOutOfMeshVertex = y == 0 || y == numberVertsPerLine - 1 || x == 0 || x == numberVertsPerLine - 1;
                    bool isSkippedVertex = x > 2 && x < numberVertsPerLine - 3 && y > 2 && y < numberVertsPerLine - 3 && ((x - 2) % skipIncrement != 0 || (y - 2) % skipIncrement != 0);

                    if (isOutOfMeshVertex)
                    {
                        vertextIndicesMap[x, y] = outOfMeshVertexIndex;
                        outOfMeshVertexIndex--;
                    }
                    else if (!isSkippedVertex)
                    {
                        vertextIndicesMap[x, y] = meshVertexIndex;
                        meshVertexIndex++;
                    }
                }
            }

            for (int y = 0; y < numberVertsPerLine; y++)
            {
                for (int x = 0; x < numberVertsPerLine; x++)
                {
                    bool isSkippedVertex = x > 2 && x < numberVertsPerLine - 3 && y > 2 && y < numberVertsPerLine - 3 && ((x - 2) % skipIncrement != 0 || (y - 2) % skipIncrement != 0);
                    if (!isSkippedVertex)
                    {
                        bool isOutOfMeshVertex = y == 0 || y == numberVertsPerLine - 1 || x == 0 || x == numberVertsPerLine - 1;
                        bool isMeshEdgeVertex = (y == 1 || y == numberVertsPerLine - 2 || x == 1 || x == numberVertsPerLine - 2) && !isOutOfMeshVertex;
                        bool isMainVertex = (x - 2) % skipIncrement == 0 && (y - 2) % skipIncrement == 0 && !isOutOfMeshVertex && !isMeshEdgeVertex;
                        bool isEdgeConnectionVertex = (y == 2 || y == numberVertsPerLine - 3 || x == 2 || x == numberVertsPerLine - 3) && !isOutOfMeshVertex && !isMeshEdgeVertex && !isMainVertex;

                        int vertexIndex = vertextIndicesMap[x, y];
                        Vector2 percent = new Vector2(x - 1, y - 1) / (numberVertsPerLine - 3);
                        Vector2 vertexPosition2D = topLeft + new Vector2(percent.x, -percent.y) * meshSettings.meshWorldSize;
                        float height = heightMap[x, y];

                        if (isEdgeConnectionVertex)
                        {
                            bool isVertical = x == 2 || x == numberVertsPerLine - 3;
                            int distanceToMainvertexA = ((isVertical) ? y - 2 : x - 2) % skipIncrement;
                            int distanceToMainvertexB = skipIncrement - distanceToMainvertexA;
                            float distancePercentFromAToB = distanceToMainvertexA / (float)skipIncrement;

                            float heightMainVertexA = heightMap[(isVertical) ? x : x - distanceToMainvertexA, (isVertical) ? y - distanceToMainvertexA : y];
                            float heightMainVertexB = heightMap[(isVertical) ? x : x + distanceToMainvertexB, (isVertical) ? y + distanceToMainvertexB : y];

                            height = heightMainVertexA * (1-distancePercentFromAToB) + heightMainVertexB * distancePercentFromAToB;
                        }

                        meshData.AddVertex(new Vector3(vertexPosition2D.x, height, vertexPosition2D.y), percent, vertexIndex);

                        bool createTriangle = x < numberVertsPerLine - 1 && y < numberVertsPerLine - 1 && (!isEdgeConnectionVertex || (x != 2 && y != 2));

                        if (createTriangle)
                        {
                            int currentIncrement = (isMainVertex && x != numberVertsPerLine - 3 && y != numberVertsPerLine - 3) ? skipIncrement : 1;
                            int a = vertextIndicesMap[x, y];
                            int b = vertextIndicesMap[x + currentIncrement, y];
                            int c = vertextIndicesMap[x, y + currentIncrement];
                            int d = vertextIndicesMap[x + currentIncrement, y + currentIncrement];
                            meshData.AddTriangle(a, d, c);
                            meshData.AddTriangle(d, a, b);
                        }
                    }

                }
            }

            meshData.ProcessMesh();

            return meshData;
        }

        public struct Coord
        {
            public readonly int x;
            public readonly int y;
        }
    }

    public class MeshData
    {
        Vector3[] vertices;
        int[] triangles;
        Vector2[] uvs;
        Vector3[] bakedNormals;

        Vector3[] outOfMeshVertices;
        int[] outOfMeshTriangles;

        int triangleIndex;
        int outOfMeshTriangleIndex;

        public MeshData(int numberOfVerticesPerLine, int skipIncrement)
        {
            int numberOfMeshEdgeVertices = (numberOfVerticesPerLine - 2) * 4 - 4;
            int numberOfEdgeConnectionVertices = (skipIncrement - 1) * (numberOfVerticesPerLine - 5) / skipIncrement * 4;
            int numberOfMainVerticesPerLine = (numberOfVerticesPerLine - 5) / skipIncrement + 1;
            int numberOfMainVertices = numberOfMainVerticesPerLine * numberOfMainVerticesPerLine;

            vertices = new Vector3[numberOfMeshEdgeVertices + numberOfEdgeConnectionVertices + numberOfMainVertices];
            uvs = new Vector2[vertices.Length];

            int numberOfMeshEdgeTriangles = 8 * (numberOfVerticesPerLine - 4);
            int numberOfMainTriangles = (numberOfMainVerticesPerLine - 1) * (numberOfMainVerticesPerLine - 1) * 2;
            triangles = new int[(numberOfMeshEdgeTriangles + numberOfMainTriangles) * 3];

            outOfMeshVertices = new Vector3[numberOfVerticesPerLine * 4 - 4];
            outOfMeshTriangles = new int[24 * (numberOfVerticesPerLine - 2)];
        }

        public void AddVertex(Vector3 vertexPosition, Vector2 uv, int vertexIndex)
        {
            if (vertexIndex < 0)
            {
                outOfMeshVertices[-vertexIndex - 1] = vertexPosition;
            }
            else
            {
                vertices[vertexIndex] = vertexPosition;
                uvs[vertexIndex] = uv;
            }
        }

        public void AddTriangle(int a, int b, int c)
        {
            if (a < 0 || b < 0 || c < 0)
            {
                outOfMeshTriangles[outOfMeshTriangleIndex] = a;
                outOfMeshTriangles[outOfMeshTriangleIndex + 1] = b;
                outOfMeshTriangles[outOfMeshTriangleIndex + 2] = c;
                outOfMeshTriangleIndex += 3;
            }
            else
            {
                triangles[triangleIndex] = a;
                triangles[triangleIndex + 1] = b;
                triangles[triangleIndex + 2] = c;
                triangleIndex += 3;
            }
        }

        Vector3[] CalculateNormals()
        {
            Vector3[] vertexNormals = new Vector3[vertices.Length];
            int triangleCount = triangles.Length / 3;

            for (int i = 0; i < triangleCount; i++)
            {
                int normalTriangleIndex = i * 3;
                int vertexIndexA = triangles[normalTriangleIndex];
                int vertexIndexB = triangles[normalTriangleIndex + 1];
                int vertexIndexC = triangles[normalTriangleIndex + 2];

                Vector3 triangleNormal = SurfaceNormalFromIndices(vertexIndexA, vertexIndexB, vertexIndexC);
                vertexNormals[vertexIndexA] += triangleNormal;
                vertexNormals[vertexIndexB] += triangleNormal;
                vertexNormals[vertexIndexC] += triangleNormal;
            }


            int borderTriangleCount = outOfMeshTriangles.Length / 3;
            for (int i = 0; i < borderTriangleCount; i++)
            {
                int normalTriangleIndex = i * 3;
                int vertexIndexA = outOfMeshTriangles[normalTriangleIndex];
                int vertexIndexB = outOfMeshTriangles[normalTriangleIndex + 1];
                int vertexIndexC = outOfMeshTriangles[normalTriangleIndex + 2];

                Vector3 triangleNormal = SurfaceNormalFromIndices(vertexIndexA, vertexIndexB, vertexIndexC);
                if (vertexIndexA >= 0)
                {
                    vertexNormals[vertexIndexA] += triangleNormal;
                }
                if (vertexIndexB >= 0)
                {
                    vertexNormals[vertexIndexB] += triangleNormal;
                }
                if (vertexIndexC >= 0)
                {
                    vertexNormals[vertexIndexC] += triangleNormal;
                }
            }

            for (int i = 0; i < vertexNormals.Length; i++)
            {
                vertexNormals[i].Normalize();
            }

            return vertexNormals;
        }

        //void ProcessEdgeConnectionVertices()
        //{
        //    foreach (var item in collection)
        //    {

        //    }
        //}

        Vector3 SurfaceNormalFromIndices(int indexA, int indexB, int indexC)
        {
            Vector3 pointA = (indexA < 0) ? outOfMeshVertices[-indexA - 1] : vertices[indexA];
            Vector3 pointB = (indexB < 0) ? outOfMeshVertices[-indexB - 1] : vertices[indexB];
            Vector3 pointC = (indexC < 0) ? outOfMeshVertices[-indexC - 1] : vertices[indexC];

            Vector3 sideAB = pointB - pointA;
            Vector3 sideAC = pointC - pointA;
            return Vector3.Cross(sideAB, sideAC).normalized;
        }

        public void ProcessMesh()
        {
            bakedNormals = CalculateNormals();
        }

        public Mesh CreateMesh()
        {
            Mesh mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uvs;
            mesh.normals = bakedNormals;
            return mesh;
        }
    }
}
