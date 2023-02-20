using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[RequireComponent(typeof(MeshFilter))]

public class RoadSegment : MonoBehaviour
{
    public bool drawLine = false;

    [SerializeField] private Mesh2D shape2D;
    
    [Range(2,20)][SerializeField] private int edgeRingCount = 3;

    [SerializeField] Transform[] controlPoints = new Transform[4];
    Vector3 GetPos(int i) => controlPoints[i].position;

    [Range(0, 1)] [SerializeField] private float tTest = 0;
    
    Mesh mesh;


    private void Awake()
    {
        mesh = new Mesh();
        mesh.name = "RoadMesh";

        GetComponent<MeshFilter>().sharedMesh = mesh;
    }

    private void Update()
    {
        GenerateMesh();
    }

    void GenerateMesh()
    {
        mesh.Clear();
        
        //________
        //VERTICES
        
        List<Vector3> verts = new List<Vector3>();

        for (int ring = 0; ring < edgeRingCount; ring++)
        {
            float t = ring / (edgeRingCount - 1f);
            OrientedPoint op = GetBezierOP(t);
            
            for (int i = 0; i < shape2D.vertexCount; i++)
            {
                verts.Add(op.LocalToWorldPos(shape2D.vertices[i].point));
            }
        }
        
        //_________
        //TRIANGLES

        for (int ring = 0; ring < edgeRingCount-1; ring++)
        {
            int rootIndex = ring * shape2D.vertexCount;
            int rootIndexNext = rootIndex + 1;
        }
    }

    public void OnDrawGizmos()
    {
        for (int i = 0; i < controlPoints.Length; i++)
        {
            Gizmos.DrawSphere(GetPos(i), 0.05f);
        }

        Handles.DrawBezier(
            GetPos(0),
            GetPos(3),
            GetPos(1),
            GetPos(2), Color.white, EditorGUIUtility.whiteTexture, 1f
        );

        OrientedPoint testPoint = GetBezierOP(tTest);
        
        Gizmos.DrawLine(GetPos(0), GetPos(1));
        Gizmos.DrawLine(GetPos(2), GetPos(3));
        //Gizmos.DrawSphere(testPoint.pos, radius);

        Handles.PositionHandle(testPoint.pos, testPoint.rot);
        
        //void DrawPoint(Vector3 localPos) => Gizmos.DrawSphere(testPoint.LocalToWorld(localPos), radius);
        
        //DrawPoint(Vector3.right * 0.5f);
        //DrawPoint(Vector3.right * - 0.5f);

        Vector3[] verts = shape2D.vertices.Select(v => testPoint.LocalToWorldPos(v.point)).ToArray();

        for (int i = 0; i < shape2D.vertices.Length; i+=2)
        {
            Vector3 a = verts[shape2D.lineIndices[i]];
            Vector3 b = verts[shape2D.lineIndices[i+1]];
            
            Gizmos.DrawLine(a,b);
        }
        
        // Draw Bezier Lines
        if (drawLine)
        {

            Vector3 p0 = GetPos(0);
            Vector3 p1 = GetPos(1);
            Vector3 p2 = GetPos(2);
            Vector3 p3 = GetPos(3);

            Gizmos.DrawLine(p0, p1);
            Gizmos.DrawLine(p1, p2);
            Gizmos.DrawLine(p3, p3);

            Vector3 a = Vector3.Lerp(p0, p1, tTest);
            Vector3 b = Vector3.Lerp(p1, p2, tTest);
            Vector3 c = Vector3.Lerp(p2, p3, tTest);

            Vector3 d = Vector3.Lerp(a, b, tTest);
            Vector3 e = Vector3.Lerp(b, c, tTest);

            Gizmos.DrawLine(a, b);
            Gizmos.DrawLine(b, c);
            Gizmos.DrawLine(d, e);
        }
    }

    OrientedPoint GetBezierOP(float t)
    {
        Vector3 p0 = GetPos(0);
        Vector3 p1 = GetPos(1);
        Vector3 p2 = GetPos(2);
        Vector3 p3 = GetPos(3);

        Vector3 a = Vector3.Lerp(p0, p1, t);
        Vector3 b = Vector3.Lerp(p1, p2, t);
        Vector3 c = Vector3.Lerp(p2, p3, t);

        Vector3 d = Vector3.Lerp(a, b, t);
        Vector3 e = Vector3.Lerp(b, c, t);

        Vector3 pos = Vector3.Lerp(d, e, t);

        Vector3 tangent = (d - e).normalized;

        return new OrientedPoint(pos, tangent);
    }
    
}
