using System;
using Graphics;
using Tuile.Graphics.Data;
using UnityEngine;

namespace Tuile.Graphics
{
    [RequireComponent(typeof(MeshCollider))]
    public class MesheurTrituile : Mesheur
    {
        [SerializeField] private MeshCollider _col;
        [SerializeField]
        private TuileMeshData _dataMesh;

        protected override Mesh MeshAMesher => _dataMesh? _dataMesh.MeshDataTrituile : new Mesh();

        protected override void QuandAssigne()
        {
            base.QuandAssigne();
            if (!_col) TryGetComponent(out _col);
            {
                _col.sharedMesh = MeshAMesher;
                if (_col.sharedMesh) _col.sharedMesh.RecalculateNormals();
            }   
        }
    }
}