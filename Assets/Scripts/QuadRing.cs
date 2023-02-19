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
    int angularSegmentCount;

    public enum UvProjection
    {
        AngularRadial,
        TopDown
    };

    public UvProjection uvProjection = UvProjection.AngularRadial;

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
        List<Vector2> uvs = new List<Vector2>();

        for (int i = 0; i < angularSegmentCount + 1; i++)
        {
            float t = i / (float)angularSegmentCount;
            float angRad = t * MathFs.TAU;
            Vector2 dir = MathFs.GetUnitVectorByAngle(angRad);
            
            vertices.Add(dir * radiusOuter);
            vertices.Add(dir * radiusInner);
            
            normals.Add(Vector3.forward);
            normals.Add(Vector3.forward);

            switch (uvProjection)
            {
                case UvProjection.AngularRadial:
                    uvs.Add(new Vector2(t, 1));
                    uvs.Add(new Vector2(t, 0));
                    break;
                    
                case UvProjection.TopDown: 
                    uvs.Add(dir * 0.5f + Vector2.one * 0.5f);
                    uvs.Add(dir * (radiusInner/radiusOuter) * 0.5f + Vector2.one * 0.5f);
                    break;
            }
            
            
        }

        List<int> triangleIndices = new List<int>();
        for (int i = 0; i < angularSegmentCount; i++)
        {
            int rootIndex = i * 2;
            int rootInnerIndex = rootIndex + 1;
            int rootOuterNext = (rootIndex + 2);
            int rootInnerNext = (rootIndex + 3);
            
            triangleIndices.Add(rootIndex);
            triangleIndices.Add(rootOuterNext);
            triangleIndices.Add(rootInnerNext);
            
            triangleIndices.Add(rootIndex);
            triangleIndices.Add(rootInnerNext);
            triangleIndices.Add(rootInnerIndex);
        }
        
        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangleIndices, 0);
        mesh.SetUVs(0, uvs);
        mesh.SetNormals(normals);
    }
    
    
}
