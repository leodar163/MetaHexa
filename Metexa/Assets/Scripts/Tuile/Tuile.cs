using System;
using Tuile.Graphics;
using UnityEngine;

namespace Tuile
{
    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
    public class Tuile : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRend;
        [SerializeField] private MeshFilter _meshFilt;

        [SerializeField] private float[] _mappeHauteur = new float[24];
        [SerializeField] private TriTuile[] _trituiles = new TriTuile[24];
        public TriTuile[] TriTuiles => _trituiles;
        public float[] MappeHauteur => _mappeHauteur;
        
        private void OnValidate()
        {
            if (!_meshRend && TryGetComponent(out _meshRend))
            {
                
            }
            if (!_meshFilt && TryGetComponent(out _meshFilt))
            {
                
            }
        }
        
    }
}