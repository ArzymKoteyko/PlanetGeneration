using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    Face[]       faces;
    [Range(.1f, 4f)]
    public float radius = 1f;

    Vector3[] verticesOfIcosahedron;
    float phi = 1.61803399f;
    int[] triangles;


    private void OnValidate()
    {
        Initialize();
        for (int i = 0; i < 20; i++)
        {
            faces[i].generateFace(radius);
        }
    }

    void Initialize()
    {

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

        // Creating array of MeshFilters if dosen't exit already
        if (meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[20];
        }
        // Creating array of faces if dosen't exit already
        if (faces == null || faces.Length == 0)
        {
            faces = new Face[20];
        }

        // For each MeshFilter creating a Game object that contains it
        for (int i = 0; i < 20; i++)
        {
            // cheking if mesh filter already exists
            if (meshFilters[i] == null)
            {
                GameObject gameObject = new GameObject($"mesh_{i}");
                gameObject.transform.parent = transform;


                gameObject.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                meshFilters[i] = gameObject.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }

            // chosing one of 20 triangles to be a face
            int[] representableTriangle = new int[] {
                triangles[3 * i],
                triangles[3 * i + 1],
                triangles[3 * i + 2]
            };

            // searching for points of triangle vertices
            Vector3[] triangleVertices = new Vector3[] {
                verticesOfIcosahedron[representableTriangle[0]],
                verticesOfIcosahedron[representableTriangle[1]],
                verticesOfIcosahedron[representableTriangle[2]]
            };

            // creating a face from triangle
            faces[i] = new Face(meshFilters[i].sharedMesh, triangleVertices, representableTriangle, radius);
        }
    }

}
