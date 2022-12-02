using Graphics;
using JetBrains.Annotations;
using Tuile.Graphics.Data;
using UnityEngine;

namespace Tuile.Graphics
{
    [RequireComponent(typeof(Tuile))]
    public class MesheurHexaTuile : Mesheur
    {
        [SerializeField] private Tuile _tuile;
        [SerializeField] private int indexTuile;

        [SerializeField]
        private TuileMeshData _dataMesh; 
        [CanBeNull]
        protected override Mesh MeshAMesher => !_dataMesh ? new Mesh() : _dataMesh.RecupMeshTuileHexa(indexTuile);

        protected override void QuandMAJ()
        {
            if (_tuile) AppliquerMappeHauteur();
        }

        protected override void QuandAssigne()
        {
            if (!_tuile) TryGetComponent(out _tuile);
        }

        private void AppliquerMappeHauteur()
        {
            if (_meshFilter.sharedMesh.vertices.Length == 0) return;
            MeshsTuiles.AppliquerMappeHauteur(_tuile.MappeHauteur, _meshFilter.sharedMesh);
            for (int i = 0; i < _tuile.TriTuiles.Length; i++)
            {
                if (i < _tuile.MappeHauteur.Length && _tuile.TriTuiles[i] is not null)
                    _tuile.TriTuiles[i].AppliquerHauteur(_tuile.MappeHauteur[i]);
            }
        }
    }
}