using System;
using System.Collections.Generic;
using PathFinding;
using Tuile.Graphics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Tuile
{
    [RequireComponent(typeof(MeshCollider), typeof(MesheurTrituile))]
    public class TriTuile : MonoBehaviour
    {
        [SerializeField] private Tuile tuileBase;
        [Space]
        [SerializeField] private MeshCollider col;
        public Noeud noeud;
        private static LayerMask maskTuile;

        private float _hauteur;

        private void OnValidate()
        {
            if (!col && TryGetComponent(out col))
            {
                col.sharedMesh = MeshsTuiles.TriTuile;
                if (col.sharedMesh) col.sharedMesh.RecalculateNormals();
            }   
            FixerHauteur();   
        }

        private void Awake()
        {
            noeud = new Noeud(this);
        }

        private void Start()
        {
            if (maskTuile == 0) maskTuile = gameObject.layer;
            TrouverVoisins();
        }

        private void Update()
        {
            FixerHauteur();
        }

        public void DevenirVisible(bool devenirVisible = true)
        {
            
        }

        private void TrouverVoisins()
        {
            List<Noeud> noeudAdjacents = new List<Noeud>();
            float decalageRad = transform.eulerAngles.y * Mathf.Deg2Rad;
            float cranRad = 2 * Mathf.PI / 3;
            
            for (int i = 0; i < 3; i++)
            {
                Vector3 decalagePos = new Vector3
                {
                    x = Mathf.Cos(cranRad * i + decalageRad) * 0.62f,
                    z = Mathf.Sin(cranRad * i + decalageRad) * 0.62f
                };

                Collider[] autres = Physics.OverlapBox(noeud.tuile.transform.position + decalagePos,
                    Vector3.one * 0.01f, new Quaternion(), maskTuile);
                
                
                if (autres.Length > 0 && autres[0].TryGetComponent(out TriTuile autreTuile))
                {
                    noeudAdjacents.Add(autreTuile.noeud);
                }
            }
            
            noeud.Voisins = noeudAdjacents.ToArray();
        }

        public void AppliquerHauteur(float hauteur)
        {
            _hauteur = hauteur;
            FixerHauteur();
        }
        
        private void FixerHauteur()
        {
            if (transform.position.y != _hauteur)
            {
                transform.position = new Vector3
                {
                    x = transform.position.x,
                    y = _hauteur,
                    z = transform.position.z
                };
            }
        }
    }
}