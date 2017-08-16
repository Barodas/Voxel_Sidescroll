using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshData
{
    public List<Vector3> vertices = new List<Vector3>();
    public List<int> triangles = new List<int>();
    public List<Vector2> uv = new List<Vector2>();
    public List<Vector2> colVertices = new List<Vector2>();

    public bool useRenderDataForCol;

    public MeshData()
    {

    }

    public void AddVertex(Vector3 vertex)
    {
        vertices.Add(vertex);
    }

    public void AddColliderVertex(Vector2 vertex)
    {
        colVertices.Add(vertex);
    }

    public void AddTriangle(int tri)
    {
        triangles.Add(tri);
    }

    public void AddQuadTriangles()
    {
        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 3);
        triangles.Add(vertices.Count - 2);
        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 2);
        triangles.Add(vertices.Count - 1);
    }
}