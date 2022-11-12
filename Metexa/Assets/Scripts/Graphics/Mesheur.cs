using System;
using UnityEngine;

namespace Graphics
{
    [ExecuteInEditMode, RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public abstract class Mesheur : MonoBehaviour
    {
        [SerializeField] protected MeshFilter _meshFilter;

        protected virtual Mesh MeshAMesher => new Mesh();

        [SerializeField] private bool ReinitMesh;
        
        protected void OnValidate()
        {
            if (!_meshFilter) TryGetComponent(out _meshFilter);
            QuandAssigne();
        }

        private void Update()
        {
            if (_meshFilter)
            {
                if(!_meshFilter.sharedMesh) AssignerMesh();
                if (ReinitMesh)
                {
                    AssignerMesh();
                    ReinitMesh = false;
                }
            }
            QuandMAJ();
        }

        private void AssignerMesh()
        {
            _meshFilter.sharedMesh = MeshAMesher;
            MeshAMesher.RecalculateNormals();
            MeshAMesher.RecalculateBounds();
        }
        
        /// <summary>
        /// Permet d'exécuter du code dans Update sans y avoir accès directement
        /// </summary>
        protected virtual void QuandMAJ()
        {
            
        }

        /// <summary>
        /// Permet d'exécuter du code dans OnValidate sans y avoir accès directement
        /// </summary>
        protected virtual void QuandAssigne()
        {
            
        }
    }
}