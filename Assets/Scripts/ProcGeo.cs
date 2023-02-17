using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]


public class ProcGeo : MonoBehaviour
{
    private void Awake()
    {
        Mesh mesh = new Mesh();
        mesh.name = "ProcQuad";

        List<Vector3> points = new List<Vector3>()
        {
            new Vector3(-1, 1),
            new Vector3(1, 1),
            new Vector3(-1, -1),
            new Vector3(1, -1),
            
            // 1st Triangle: [2,0,1] // 2nd: [2,1,3]
        };

        int[] triIndices = new int[]
        {
            1, 0, 2,
            3, 1, 2
        };

        List<Vector2> uvs = new List<Vector2>()
        {
            new Vector2(1,1),
            new Vector2(0,1),
            new Vector2(1,0),
            new Vector2(0,0)
        };

        List<Vector3> normals = new List<Vector3>()
        {
            Vector3.forward,
            Vector3.forward,
            Vector3.forward,
            Vector3.forward,
        };

        mesh.SetVertices(points);
        mesh.triangles = triIndices;
        mesh.SetNormals(normals);
        mesh.SetUVs(0, uvs);
        //mesh.RecalculateNormals();

        GetComponent<MeshFilter>().sharedMesh = mesh;

    }
}
