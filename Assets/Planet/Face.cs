using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face
{
    Mesh mesh;
    Vector3[] initialVertices;
    Vector3[] verticesOfIcosahedronFace;
    Vector3[] verticesOfShereSector;
    int[] initialTriangles;
    int[] triangles;
    float radius;

    public Face(Mesh mesh, Vector3[] initialVertices, int[] initialTriangles, float radius)
    {
        this.mesh = mesh;
        this.initialVertices = initialVertices;
        this.initialTriangles = initialTriangles;

        triangles = new int[] { 0, 1, 2 };
        verticesOfIcosahedronFace = this.initialVertices;
        verticesOfShereSector = new Vector3[3];
        this.radius = radius;
        recanculateHights(radius);
    }

    private void recanculateHights(float radius)
    {
        for (int i = 0; i < verticesOfIcosahedronFace.Length; i++)
        {
            verticesOfShereSector[i] = verticesOfIcosahedronFace[i].normalized * radius;
        }
    }

    public void generateFace(float radius)
    {
        this.radius = radius;
        recanculateHights(radius);
        mesh.Clear();
        mesh.vertices = verticesOfShereSector;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
