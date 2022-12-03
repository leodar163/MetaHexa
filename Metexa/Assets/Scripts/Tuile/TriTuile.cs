using System;
using System.Collections.Generic;
using Graphics;
using Interactions;
using PathFinding;
using Tuile.Graphics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Tuile
{
    [RequireComponent(typeof(MesheurTrituile))]
    public class TriTuile : MonoBehaviour, IClicable, ISelectionable
    {
        [SerializeField] private Tuile _tuileBase;
        [Space]
        
        public Noeud noeud;
        public static LayerMask maskTuile;

        private float _hauteur;
        

        private void OnValidate()
        {

            
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

        public void QuandCliqueGauche()
        {
            print("Clique gauche sur " + name);
        }

        public void QuandCliqueDroit()
        {
            print("Clique droit sur " + name);
        }

        public void QuandSelectionned()
        {
            print(name + " sélectionné");
        }

        public void QuandDeselectionned()
        {
            print(name + " désélectionné");
        }
    }
}