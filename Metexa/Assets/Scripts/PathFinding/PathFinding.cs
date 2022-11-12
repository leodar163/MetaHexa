using System;
using System.Collections.Generic;
using System.Linq;
using Tuile;
using Unity.VisualScripting;
using UnityEngine;

namespace PathFinding
{
    public class PathFinding
    {
        private class NoeudInfo
        {
            public float coutDepla = 0;
            public float CoutHeuri = 0;
            public float coutTotal => coutDepla + CoutHeuri;
            public Noeud parent;
        }

        public static Stack<Noeud> TrouverChemin(TriTuile debut, TriTuile fin)
        {
            List<Noeud> noeudsAObserver = new List<Noeud>();
            List<Noeud> noeudsFermed = new List<Noeud>();

            Dictionary<Noeud, NoeudInfo> grapheInfo = 
                Graphe.noeudsGeneraux.ToDictionary(noeud => noeud, _ => new NoeudInfo());

            Noeud noeudFin = Graphe.TrouverNoeudParTuile(fin);
            Noeud noeudDebut = Graphe.TrouverNoeudParTuile(debut);
            noeudsAObserver.Add(noeudDebut);

            while (noeudsAObserver.Count > 0)
            {
                Noeud curseurNoeud = noeudsAObserver[0];

                for (int i = 1; i < noeudsAObserver.Count; i++)
                {
                    if (grapheInfo[noeudsAObserver[i]].coutTotal < grapheInfo[curseurNoeud].coutTotal ||
                        Math.Abs(grapheInfo[noeudsAObserver[i]].coutTotal - grapheInfo[curseurNoeud].coutTotal) < 0.001f &&
                        grapheInfo[noeudsAObserver[i]].CoutHeuri < grapheInfo[curseurNoeud].CoutHeuri)
                    {
                        curseurNoeud = noeudsAObserver[i];
                    }
                }

                noeudsAObserver.Remove(curseurNoeud);
                noeudsFermed.Add(curseurNoeud);

                if (curseurNoeud.tuile == fin) return GenererChemin(noeudDebut,noeudFin,grapheInfo);

                foreach (var voisin in curseurNoeud.Voisins)
                {
                    if(noeudsFermed.Contains(voisin)) continue;

                    NoeudInfo infoVoisin = grapheInfo[voisin];
                    float coutMovementVersVoisin = grapheInfo[curseurNoeud].coutDepla + 
                                                   DistanceEntreNoeud(curseurNoeud, voisin);

                    if (coutMovementVersVoisin < infoVoisin.coutDepla ||
                        !noeudsFermed.Contains(voisin))
                    {
                        infoVoisin.coutDepla = coutMovementVersVoisin;
                        infoVoisin.CoutHeuri = DistanceEntreNoeud(voisin, noeudFin);
                        infoVoisin.parent = curseurNoeud;
                        
                        if(!noeudsAObserver.Contains(voisin)) noeudsAObserver.Add(voisin);
                    }
                }
            }
            throw new Exception("Aucun chemin trouvé entre les tuiles " + debut.name +" et " + fin.name);
        }

        private static Stack<Noeud> GenererChemin(Noeud noeudDebut, Noeud noeudFin, 
            Dictionary<Noeud, NoeudInfo> grapheInfo)
        {
            Stack<Noeud> chemin = new Stack<Noeud>();

            Noeud curseurNoeud = noeudFin;

            while (curseurNoeud != noeudDebut)
            {
                chemin.Push(curseurNoeud);
                curseurNoeud = grapheInfo[curseurNoeud].parent;
            }
            
            return chemin;
        }

        private static float DistanceEntreNoeud(Noeud noeudA, Noeud noeudB)
        {
            Vector2 posA = new Vector2
            {
                x = noeudA.tuile.transform.position.x,
                y = noeudA.tuile.transform.position.z
            };
            
            Vector2 posB = new Vector2
            {
                x = noeudB.tuile.transform.position.x,
                y = noeudB.tuile.transform.position.z
            };
            return Vector2.Distance(posA, posB);
        }
    }
}