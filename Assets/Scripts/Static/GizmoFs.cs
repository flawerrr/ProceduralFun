using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GizmosFs
{
    public static void DrawWireCircle(Vector3 poso, Quaternion roto, float radius, int detail = 32)
    {

        Vector3[] points3D = new Vector3[detail];
        for (int i = 0; i < detail; i++)
        {
            float t = i / (float)detail;
            float angRad = t * MathFs.TAU;

            Vector2 point2D = MathFs.GetUnitVectorByAngle(angRad);

            points3D[i] = poso + roto * point2D;

        }

        for (int i = 0; i < detail - 1; i++)
        {
            Gizmos.DrawLine(points3D[i], points3D[i+1]);
        }
        Gizmos.DrawLine(points3D[detail-1], points3D[0]);
        
    }
}