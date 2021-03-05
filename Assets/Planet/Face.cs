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

    public Face(Mesh mesh, Vector3[] initialVertices, int[] initialTriangles)
    {
        this.mesh = mesh;
        this.initialVertices = initialVertices;
        this.initialTriangles = initialTriangles;

        triangles = initialTriangles;
        verticesOfIcosahedronFace = initialVertices;

        for (int i = 0; i < verticesOfIcosahedronFace.Length; i++)
        {
            verticesOfShereSector[i] = verticesOfIcosahedronFace[i].normalized;
        }


    }

    public void generateFace()
    {
        mesh.Clear();
        mesh.vertices = verticesOfShereSector;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
