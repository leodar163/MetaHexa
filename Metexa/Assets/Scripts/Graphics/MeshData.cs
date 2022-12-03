using System;
using UnityEngine;

namespace Graphics
{
    [Serializable]
    public struct MeshData
    {
        [SerializeField] private Vector3[] _vectices;
        [SerializeField] private int[] _triangles;
        [SerializeField] private string _nom;
        [SerializeField] private bool _aEteInit;

        public Vector3[] vertices => _vectices;
        public int[] triangles => _triangles;
        public string nom => _nom;
        public bool aEteInit => _aEteInit;
        
        public MeshData(Mesh mesh)
        {
            _vectices = new Vector3[mesh.vertexCount];
            mesh.vertices.CopyTo(_vectices, 0);

            _triangles = new int[mesh.triangles.Length];
            mesh.triangles.CopyTo(_triangles, 0);

            _nom = mesh.name;

            _aEteInit = true;
        }

        public Mesh ConvertirEnMesh()
        {
            Vector3[] verticesCopie = new Vector3[vertices.Length];
            vertices.CopyTo(verticesCopie,0);

            int[] trianglesCopie = new int[triangles.Length];
            triangles.CopyTo(trianglesCopie, 0);

            return new Mesh
            {
                vertices = verticesCopie,
                triangles = trianglesCopie,
                name = nom
            };
        }
    }
}