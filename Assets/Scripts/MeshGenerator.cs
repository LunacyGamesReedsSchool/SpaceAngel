using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    private Mesh mesh;

    private Vector3[] vertices;

    private int[] triangles;

    private int xSize = 40;
    private int zSize = 40;
    private float xPerlin = 0.3f;
    private float ExPerlin = 2f;
    private MeshCollider MeshCollider;

    // Start is called before the first frame update
    public void Start()
    {
        MeshCollider = GetComponent<MeshCollider>();
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }
    /// Array of vertices that we want to create
    /// Assigns each of the vertices a place (Loops through x column)
    /// Once done with x column, it moves to the next z column
    private void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * xPerlin, z * xPerlin) * ExPerlin;//Perlin noise changes the height of the terrain
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        // Array that hold the connections between each vertice
        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;
        // populates the triangles array with the connections between vertices
        for(int z = 0; z < zSize; z++)
        {
            for(int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }


        

        
    }

    //Shows the lighting of the mesh
    public void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        MeshCollider.sharedMesh = mesh;
    }

}
