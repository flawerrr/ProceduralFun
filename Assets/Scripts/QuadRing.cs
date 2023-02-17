using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]

public class QuadRing : MonoBehaviour
{
    [SerializeField]
    float radiusInner = 1f;
    
    [SerializeField]
    float thickness = 1f;
    
    [SerializeField][Range(3, 32)] 
    public int angularSegmentCount;

    private Mesh mesh;

    private float radiusOuter => radiusInner + thickness;
    private int vertexCount => angularSegmentCount * 2;


    /*private void OnDrawGizmos()
    {
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;
        
        GizmosFs.DrawWireCircle(pos, rot, radiusInner, angularSegmentCount);
        GizmosFs.DrawWireCircle(pos, rot, radiusOuter, angularSegmentCount);
    }
    */

    private void Awake()
    {
        mesh = new Mesh();
        mesh.name = "QuadRing";

        GetComponent<MeshFilter>().sharedMesh = mesh;
    }

    private void Update()
    {
        GenerateMesh();
    }

    void GenerateMesh()
    {
        mesh.Clear();

        int vCount = vertexCount;
        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();

        for (int i = 0; i < angularSegmentCount; i++)
        {
            float t = i / (float)angularSegmentCount;
            float angRad = t * MathFs.TAU;
            Vector2 dir = MathFs.GetUnitVectorByAngle(angRad);
            
            vertices.Add(dir * radiusOuter);
            vertices.Add(dir * radiusInner);
            
            normals.Add(Vector3.forward);
            normals.Add(Vector3.forward);
        }

        List<int> triangleIndices = new List<int>();
        for (int i = 0; i < angularSegmentCount; i++)
        {
            int rootIndex = i * 2;
            int rootInnerIndex = rootIndex + 1;
            int rootOuterNext = (rootIndex + 2) % vCount;
            int rootInnerNext = (rootIndex + 3) % vCount;
            
            triangleIndices.Add(rootIndex);
            triangleIndices.Add(rootOuterNext);
            triangleIndices.Add(rootInnerNext);
            
            triangleIndices.Add(rootIndex);
            triangleIndices.Add(rootInnerNext);
            triangleIndices.Add(rootInnerIndex);
        }
        
        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangleIndices, 0);
        mesh.SetNormals(normals);
    }
    
    
}
