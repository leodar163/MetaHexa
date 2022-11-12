using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tuile;
using Unity.VisualScripting;
using UnityEngine;

namespace PathFinding
{
    public class PathFinder : MonoBehaviour
    {
        private static Queue<Action<Stack<Noeud>>> s_demandesChemins = new();

        private static IEnumerator s_trouvageChemin;
        
        public static void TrouverChemin(TriTuile debut, TriTuile fin, Action<Stack<Noeud>> rappel)
        {
            s_demandesChemins.Enqueue(rappel);

            if (s_trouvageChemin == null)
            {
                s_trouvageChemin = TrouvageChemin(debut, fin);
                Mappe.Singleton.StartCoroutine(s_trouvageChemin);
            }
        }

        private static IEnumerator TrouvageChemin(TriTuile debut, TriTuile fin)
        {
            while (s_demandesChemins.Count > 0)
            {
                Action<Stack<Noeud>> rappel = s_demandesChemins.Dequeue();
                rappel.Invoke(TrouverChemin(debut, fin));
                yield return new WaitForEndOfFrame();
            }

            s_trouvageChemin = null;
        }
        
        private static Stack<Noeud> TrouverChemin(TriTuile debut, TriTuile fin)
        {
            List<Noeud> noeudsAObserver = new List<Noeud>();
            List<Noeud> noeudsFermed = new List<Noeud>();

            Noeud noeudFin = fin.noeud;
            Noeud noeudDebut = debut.noeud;
            noeudsAObserver.Add(noeudDebut);

            while (noeudsAObserver.Count > 0)
            {
                Noeud curseurNoeud = noeudsAObserver[0];

                for (int i = 1; i < noeudsAObserver.Count; i++)
                {
                    if (noeudsAObserver[i].pathFindingInfos.coutTotal < curseurNoeud.pathFindingInfos.coutTotal ||
                        Math.Abs(noeudsAObserver[i].pathFindingInfos.coutTotal - curseurNoeud.pathFindingInfos.coutTotal) 
                        < 0.0001f &&
                        noeudsAObserver[i].pathFindingInfos.coutHeuri < curseurNoeud.pathFindingInfos.coutHeuri)
                    {
                        curseurNoeud = noeudsAObserver[i];
                    }
                }

                noeudsAObserver.Remove(curseurNoeud);
                noeudsFermed.Add(curseurNoeud);

                if (curseurNoeud.tuile == fin) return GenererChemin(noeudDebut,noeudFin);

                foreach (var voisin in curseurNoeud.Voisins)
                {
                    if(noeudsFermed.Contains(voisin)) continue;
                    
                    float coutMovementVersVoisin = curseurNoeud.pathFindingInfos.coutDepla + 
                                                   DistanceEntreNoeud(curseurNoeud, voisin);

                    if (coutMovementVersVoisin < voisin.pathFindingInfos.coutDepla ||
                        !noeudsFermed.Contains(voisin))
                    {
                        voisin.pathFindingInfos.coutDepla = coutMovementVersVoisin;
                        voisin.pathFindingInfos.coutHeuri = DistanceEntreNoeud(voisin, noeudFin);
                        voisin.pathFindingInfos.parent = curseurNoeud;
                        
                        if(!noeudsAObserver.Contains(voisin)) noeudsAObserver.Add(voisin);
                    }
                }
            }
            throw new Exception("Aucun chemin trouvé entre les tuiles " + debut.name +" et " + fin.name);
        }

        private static Stack<Noeud> GenererChemin(Noeud noeudDebut, Noeud noeudFin)
        {
            Stack<Noeud> chemin = new Stack<Noeud>();

            Noeud curseurNoeud = noeudFin;

            while (curseurNoeud != noeudDebut)
            {
                chemin.Push(curseurNoeud);
                curseurNoeud = curseurNoeud.pathFindingInfos.parent;
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