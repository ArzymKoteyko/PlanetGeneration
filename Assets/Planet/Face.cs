using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face
{
    Mesh mesh;
    Vector3[] initialVertices;
    Vector3[] verticesOfIcosahedronFace;
    Vector3[] verticesOfShereSector;
    int[] triangles;
    float radius;
    int resolution;

    public Face(Mesh mesh, Vector3[] initialVertices, float radius, int resolution)
    {
        this.mesh = mesh;
        this.initialVertices = initialVertices;

        this.radius = radius;
        this.resolution = resolution;

        Vector3 firstConstructingVector = (initialVertices[2] - initialVertices[0]);
        Vector3 secondConstructingVector = (initialVertices[1] - initialVertices[0]);
        firstConstructingVector /= (resolution - 1);
        secondConstructingVector /= (resolution - 1);


        int ammoutOfVerticies = 0;
        int ammountOfTriangles = 0; 
        for (int i = resolution + 2; i > 0; i--)
        {
            ammoutOfVerticies += i;
        }
        for (int i = resolution - 1; i > 0; i--)
        {
            ammountOfTriangles += 2 * i - 1;
        }
        verticesOfIcosahedronFace = new Vector3[ammoutOfVerticies];
        triangles = new int[ammountOfTriangles * 3];


        int firstIndexOfRowCounter = 0;
        for (int i = 0; i < resolution + 2; i++)
        {
            verticesOfIcosahedronFace[firstIndexOfRowCounter] = initialVertices[0] + (firstConstructingVector * i);
            for (int j = 1; j < resolution - i; j++)
            {
                verticesOfIcosahedronFace[firstIndexOfRowCounter + j] = verticesOfIcosahedronFace[firstIndexOfRowCounter] + (secondConstructingVector * j);
            }
            firstIndexOfRowCounter += resolution - i;
        }
        
        firstIndexOfRowCounter = 0;
        int IndexCounter = 0;
        int row = resolution - 1;
        int currow = 0;
        for (int i = 0; i < 2*(resolution - 2) + 1; i++)
        {
            if (i % 2 == 0)
            {
                for (int j = 0; j < row; j++) //
                {
                    triangles[(IndexCounter) * 3]     = j + firstIndexOfRowCounter;
                    triangles[(IndexCounter) * 3 + 1] = j + 1 + firstIndexOfRowCounter;
                    triangles[(IndexCounter) * 3 + 2] = j + (resolution - currow) + firstIndexOfRowCounter;
                    IndexCounter++;
                }
            }
            else
            {
                row -= 1;
                for (int j = 0; j < row; j++)
                {
                    triangles[(IndexCounter) * 3]     = j + 1 + firstIndexOfRowCounter;
                    triangles[(IndexCounter) * 3 + 1] = j + 1 + (resolution - currow) + firstIndexOfRowCounter;
                    triangles[(IndexCounter) * 3 + 2] = j + (resolution - currow) + firstIndexOfRowCounter;
                    IndexCounter++;
                }
                firstIndexOfRowCounter += resolution - currow;
                currow++;
            }
        }
        
        /*
        triangles = new int[] { 0, 1, 4,
                                1, 2, 5,
                                2, 3, 6,
                                4, 1, 5,
                                5, 2, 6,
                                4, 5, 7, 
                                5, 6, 8,
                                7, 5, 8, 
                                7, 8, 9};
        */
        //verticesOfIcosahedronFace = this.initialVertices;

        // applying hight functions and projecting points on sphere
        

        //triangles = new int[] { 0, 1, 4 };

        this.radius = radius;
        verticesOfShereSector = new Vector3[ammoutOfVerticies];
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
        //mesh.vertices = verticesOfIcosahedronFace;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
