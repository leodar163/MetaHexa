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
        
        [Header("graphique")]
        [SerializeField] private MaterialTeinturier _teinturier;

        [SerializeField] private Color _couleurDefaut;
        [SerializeField] private Color _couleurSelectionned;
        [SerializeField] private Color _couleurChemin;
        [SerializeField] private Color _couleurFinChemin;
        
        private void OnValidate()
        {
            if (!_teinturier) TryGetComponent(out _teinturier);
            
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

        public void AppliquerCouleur(Color couleur)
        {
            _teinturier.teinteBase = couleur;
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

        private static TriTuile s_triTuileSelectionned;
        
        public void QuandCliqueGauche()
        {
           
        }

        public void QuandCliqueDroit()
        {
            if (s_triTuileSelectionned)
            {
                PathFinder.TrouverChemin(s_triTuileSelectionned,this, ColorerChemin);
            }
        }

        private void ColorerChemin(Stack<Noeud> chemin)
        {
            while (chemin.Count > 1)
            {
                noeud = chemin.Peek();
                noeud.tuile.AppliquerCouleur(noeud.tuile._couleurChemin);
            }

            noeud = chemin.Peek();
            noeud.tuile.AppliquerCouleur(noeud.tuile._couleurFinChemin);
        }
        
        public void QuandSelectionned()
        {
            if (s_triTuileSelectionned != null && s_triTuileSelectionned != this)
            {
                s_triTuileSelectionned.QuandDeselectionned();
                AppliquerCouleur(_couleurSelectionned);
            }
            s_triTuileSelectionned = this;
        }

        public void QuandDeselectionned()
        {
            s_triTuileSelectionned = null;
            AppliquerCouleur(_couleurDefaut);
        }
    }
}