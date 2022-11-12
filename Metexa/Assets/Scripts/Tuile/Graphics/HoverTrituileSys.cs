using System;
using System.Collections.Generic;
using PathFinding;
using UnityEditor;
using UnityEngine;

namespace Tuile.Graphics
{
    public class HoverTrituileSys : MonoBehaviour
    {
        [SerializeField] private bool debugVoisins;
        private LayerMask masqueTrituile;

        private static TriTuile tuileHovered;

        // Start is called before the first frame update
        void Start()
        {
           masqueTrituile = LayerMask.GetMask("Trituile");
        }

        // Update is called once per frame
        void Update()
        {
            HoverTriTuile();
            InteractionTriTuiles();
        }
    
        private void HoverTriTuile()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 2000f, Color.green);
        
            if (Physics.Raycast(ray, out RaycastHit hit,2000f, masqueTrituile) 
                && hit.collider.TryGetComponent(out TriTuile triTuile))
            {
                if (triTuile != tuileHovered)
                {
                    
                    if (tuileHovered)
                    {
                        tuileHovered.DevenirVisible(false);
                        
                        if (debugVoisins)
                        {
                            foreach (var triTuileAdj in
                                Graphe.TrouverNoeudParTuile(tuileHovered).Voisins)
                            {
                                triTuileAdj.tuile.DevenirVisible(false);
                            }
                        }
                    }
                    
                    tuileHovered = triTuile;
                    tuileHovered.DevenirVisible();
                    
                    if (debugVoisins)
                    {
                        foreach (var triTuileAdj in
                            Graphe.TrouverNoeudParTuile(tuileHovered).Voisins)
                        {
                            triTuileAdj.tuile.DevenirVisible();
                        }
                    }
                }
                return;
            }
            if (tuileHovered)
            {
                
                tuileHovered.DevenirVisible(false);

                if (debugVoisins)
                {
                    foreach (var triTuileAdj in
                        Graphe.TrouverNoeudParTuile(tuileHovered).Voisins)
                    {
                        triTuileAdj.tuile.DevenirVisible(false);
                    }
                }
                
            }
            tuileHovered = null;
        }

        private TriTuile tuileSelectionned;
        
        private void InteractionTriTuiles()
        {
            if(tuileHovered)
            {
                if (Input.GetMouseButtonUp(0)) tuileSelectionned = tuileHovered;
                if (Input.GetMouseButtonUp(1)) if(tuileSelectionned) 
                    DessinerChemin(PathFinding.PathFinding.TrouverChemin(tuileSelectionned, tuileHovered));
                //if (Input.GetMouseButtonUp(2)) tuileHovered.hauteur = 0; //deprecié
            }
        }

        private void DessinerChemin(Stack<Noeud> chemin)
        {
            foreach (var noeud in chemin)
            {
                print(chemin.Count);
                noeud.tuile.DevenirVisible();
            }
        }
    }
}
