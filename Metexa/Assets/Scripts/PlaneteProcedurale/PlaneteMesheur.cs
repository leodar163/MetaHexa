using System;
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

        private void OnValidate()
        {
            if (_refreshMesh)
            {
                _refreshMesh = false;
                _meshFilter.sharedMesh = MeshCollection.GenererSphere(_definitionSphere);
            }
        }
    }
}
