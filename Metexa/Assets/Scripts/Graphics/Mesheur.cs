﻿using System;
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
                if (!_meshFilter.sharedMesh)
                {
                    print(name + " a assigné un mesh de TriTuile");
                    
                    AssignerMesh();
                }
                if (ReinitMesh)
                {
                    print(name + " a réinit son mesh");
                    ReinitMesh = false;
                    AssignerMesh();
                }
            }
            QuandMAJ();
        }

        private void AssignerMesh()
        {
            _meshFilter.sharedMesh = MeshAMesher;
            _meshFilter.sharedMesh.RecalculateNormals();
            _meshFilter.sharedMesh.RecalculateBounds();
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