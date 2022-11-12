using UnityEngine;

namespace Tuile.Graphics
{
    [RequireComponent(typeof(Tuile))]
    public class MesheurHexaTuile : MesheurTrituile
    {
        [SerializeField] private Tuile _tuile;
        protected override Mesh MeshAMesher => MeshsTuiles.tuileMetaHexa;
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
            MeshsTuiles.AppliquerMappeHauteur(_tuile.MappeHauteur, _meshFilter.sharedMesh);
            for (int i = 0; i < _tuile.TriTuiles.Length; i++)
            {
                if (i < _tuile.MappeHauteur.Length && _tuile.TriTuiles[i] is not null)
                    _tuile.TriTuiles[i].AppliquerHauteur(_tuile.MappeHauteur[i]);
            }
        }
    }
}