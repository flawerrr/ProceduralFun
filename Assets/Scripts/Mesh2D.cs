using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Mesh2D : ScriptableObject
{
    [System.Serializable]
    public class Vertex
    {
        public Vector2 points;
        public Vector2 normals;
        public float u; 
    }

    private Vertex[] _vertices;
    private int[] lineIndices;
}
