using System;
using System.Collections.Generic;
using Graphics;
using UnityEngine;

namespace MetaHexa.PlaneteProcedurale
{
    public class PlaneteMesheur : MonoBehaviour
    {
        [SerializeField] private MeshFilter _meshFilter;
        [Space] 
        [SerializeField][Range(0,6)] private int _definitionSphere;
        [Space]
        [SerializeField] private bool _refreshMesh;
        [SerializeField] private bool _drawDebug;

        private void OnDrawGizmos()
        {
            if(!_drawDebug) return;
            if (_meshFilter.sharedMesh)
            {
                for (int i = 0; i < _meshFilter.sharedMesh.vertices.Length; i++)
                {
                    Gizmos.color = Color.Lerp(Color.white, Color.magenta, 
                        (float)i / _meshFilter.sharedMesh.vertices.Length);
                    Gizmos.DrawSphere(_meshFilter.sharedMesh.vertices[i],0.01f);
                }
            }
        }

        /*
        private void OnValidate()
        {
            if (_refreshMesh)
            {
                _refreshMesh = false;
                _meshFilter.sharedMesh = MeshCollection.GenererSphere(_definitionSphere);
            }
        }

        
        public void GenererPlanete(int definitionSphere)
        {
            NettoyerPlanete();

            List<Mesh> meshTuiles = MeshCollection.GenererSphereTuileExtrudables(definitionSphere);
            
            foreach (var mesh in meshTuiles)
            {
                AjouterTuiles(mesh);
            }
        }

        private void AjouterTuiles(Mesh mesh)
        {
            GameObject nvlleTuile = new GameObject($"Tuile {_tuiles.Count}")
            {
                transform =
                {
                    parent = transform,
                    localPosition = Vector3.zero
                }
            };

            nvlleTuile.AddComponent<MeshRenderer>();
            MeshFilter filter = nvlleTuile.AddComponent<MeshFilter>();
            filter.sharedMesh = mesh;
            
            _tuiles.Add(filter);
        }

        private void NettoyerPlanete()
        {
            foreach (var tuile in _tuiles)
            {
                Destroy(tuile.gameObject);
            }
            
            _tuiles.Clear();
        }
        */
    }
}
