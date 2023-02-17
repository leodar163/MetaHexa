using System;
using Graphics;
using UnityEngine;

namespace MetaHexa.PlaneteProcedurale
{
    public class TuilePlanete : MonoBehaviour
    {
        [SerializeField] private MeshFilter _meshFilter;
        private Vector3 _ancreLocale;
        private float _hauteur;

        public Vector3 AncreAbsolue => _ancreLocale + transform.position;
        public Vector3 AncreLocale => _ancreLocale;
        public Planete planete { get; private set; }
        
        public float Hauteur
        {
            get => _hauteur;
            set
            {
                _hauteur = value;
                _hauteur = Mathf.Clamp(_hauteur, -planete.taille, planete.taille);
                AppliquerHauteur(_hauteur);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            //Gizmos.DrawSphere(AncreAbsolue,0.05f);

            // for (int i = 0; i < _meshFilter.mesh.normals.Length; i++)
            // {
            //     Gizmos.DrawLine(_meshFilter.mesh.vertices[i], _meshFilter.mesh.vertices[i] + _meshFilter.mesh.normals[i]);
            // }
        }

        public void Init(Mesh meshTuile, Planete planete)
        {
            _meshFilter.sharedMesh = meshTuile;
            this.planete = planete;
            CalculerAncre();   
        }

        private void CalculerAncre()
        {
            if (_meshFilter.mesh)
            {
                Vector3 a = _meshFilter.sharedMesh.vertices[0];
                Vector3 b = _meshFilter.sharedMesh.vertices[1];
                Vector3 c = _meshFilter.sharedMesh.vertices[2];

                _ancreLocale = new Vector3
                {
                    x = (a.x + b.x + c.x) / 3,
                    y = (a.y + b.y + c.y) / 3,
                    z = (a.z + b.z + c.z) / 3
                };
            }
        }

        private float _multiplicateurTaille;
        private void AppliquerHauteur(float hauteur)
        {
            if (_multiplicateurTaille != 0)
            {
                _meshFilter.mesh = MeshCollection.GenererMeshAgrandi(_meshFilter.mesh, 1/_multiplicateurTaille);
                _multiplicateurTaille = 0;
            }
            _multiplicateurTaille += (planete.taille + hauteur) / planete.taille;
            _meshFilter.mesh = MeshCollection.GenererMeshAgrandi(_meshFilter.mesh, _multiplicateurTaille);

            CalculerAncre();
        }
    }
}