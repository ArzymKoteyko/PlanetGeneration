using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    Mesh mesh;

    [SerializeField]
    MeshFilter meshFilter;
    [Range(.1f, 4f)]
    public float radius = 1f;

    Vector3[] vertices;
    Vector3[] verticesOfIcosahedron;
    float phi = 1.61803399f;
    int[] triangles;



    private void OnValidate()
    {
        Initialize();
    }

    void Initialize()
    {
        if (meshFilter == null)
        {
            GameObject meshObj = new GameObject("mesh");
            meshObj.transform.parent = transform;

            meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
            meshFilter = meshObj.AddComponent<MeshFilter>();
            meshFilter.sharedMesh = new Mesh();
        }

        //Creating array of Icosahedron's vertices
        verticesOfIcosahedron = new Vector3[]
        {
            //plane XZ
            new Vector3(  phi,  0,    1  ),
            new Vector3( -phi,  0,    1  ),
            new Vector3( -phi,  0,   -1  ),
            new Vector3(  phi,  0,   -1  ),
            //plane XY
            new Vector3(  1,    phi,  0  ),
            new Vector3(  1,   -phi,  0  ),
            new Vector3( -1,   -phi,  0  ),
            new Vector3( -1,    phi,  0  ),
            //plane YZ
            new Vector3(  0,    1,    phi),
            new Vector3(  0,   -1,    phi),
            new Vector3(  0,   -1,   -phi),
            new Vector3(  0,    1,   -phi),

        };

        // Translating vertices to Sphere 
        // Also aplying some height functions
        Vector3[] verticesOfShere = new Vector3[12];
        for (int i = 0; i < 12; i++)
        {
            verticesOfShere[i] = verticesOfIcosahedron[i].normalized * radius;
        }

        // Creating array of Icosahedron's triangle faces
        triangles = new int[]
        {
            3,  4,  0,      11, 2,  7,
            0,  8,  9,      11, 10, 2,
            0,  4,  8,      10, 6,  2,
            8,  4,  7,      5,  6,  10,
            0,  9,  5,      9,  6,  5,
            0,  5,  3,      9,  1,  6,
            3,  5,  10,     1,  9,  8,
            3,  10, 11,     1,  8,  7,
            3,  11, 4,      1,  7,  2,
            11, 7,  4,      1,  2,  6
        };

        meshFilter.sharedMesh.Clear();
        meshFilter.sharedMesh.vertices = verticesOfShere;
        meshFilter.sharedMesh.triangles = triangles;
        meshFilter.sharedMesh.RecalculateNormals();

    }

}
