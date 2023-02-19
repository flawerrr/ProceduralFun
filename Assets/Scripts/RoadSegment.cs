using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(MeshFilter))]

public class RoadSegment : MonoBehaviour
{
    [SerializeField] Transform[] controlPoints = new Transform[4];

    Vector3 GetPos(int i) => controlPoints[i].position;

    [Range(0, 1)] [SerializeField] private float tTest = 0;


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
        
        Gizmos.DrawLine(GetPos(0), GetPos(1));
        Gizmos.DrawLine(GetPos(2), GetPos(3));
        Gizmos.DrawSphere(GetBezierPoint(tTest), 0.05f);
        
    }

    Vector3 GetBezierPoint(float t)
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

        return Vector3.Lerp(d, e, t);
    }
}
