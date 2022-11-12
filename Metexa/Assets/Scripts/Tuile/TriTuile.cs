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
        public static LayerMask maskTuile;

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
            if (maskTuile == 0) maskTuile = gameObject.layer;
        }

        private void Start()
        {
            noeud.TrouverVoisins();            
        }

        private void Update()
        {
            FixerHauteur();
        }

        public void DevenirVisible(bool devenirVisible = true)
        {
            
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