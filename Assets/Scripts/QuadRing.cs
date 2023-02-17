using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadRing : MonoBehaviour
{
    public float radiusInner = 1f;
    public float thickness = 1f;

    private float radiusOuter => radiusInner + thickness;
    
    [Range(3, 32)] public int angularSegment;

    private float vertexCount => angularSegment * 2;
    



    private void OnDrawGizmos()
    {
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;
        
        GizmosFs.DrawWireCircle(pos, rot, radiusInner, angularSegment);
        GizmosFs.DrawWireCircle(pos, rot, radiusOuter, angularSegment);
    }

    

    
}
