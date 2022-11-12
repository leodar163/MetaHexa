using System;
using Tuile;

namespace PathFinding
{
    public class Noeud
        {
            public TriTuile tuile;

            private Noeud[] voisins ;
           

            public Noeud(TriTuile _tuile)
            {
                tuile = _tuile;
            }
            
            
            public Noeud[] Voisins
            {
                get => voisins;
                set
                {
                    voisins = value;
                    InitialiserPoids();
                }
            }

            private float[] poids;
            public float[] Poids => poids;

            private void InitialiserPoids()
            {
                if (voisins.Length > 0)
                {
                    poids = new float[voisins.Length];
                    for (int i = 0; i < poids.Length; i++)
                    {
                        poids[i] = 1;
                    }
                }
            }

            // todo : implémenter ça dans Graphe pour éviter que les changements ne soit répercutés que sur des noeuds temporaires
            public void ChangerPoid(TriTuile tuileVoisine, float nvPoids, bool reciperoque = true)
            {
                //todo : verifier que la tuile voisine est bien une tuile voisine
                int index = Array.LastIndexOf(voisins, Graphe.TrouverNoeudParTuile(tuileVoisine, voisins));
                ChangerPoid(index, nvPoids, reciperoque);
            }
            
            public void ChangerPoid(int indexTuileVoisine, float nvPoids, bool reciperoque = true)
            {
                poids[indexTuileVoisine] = nvPoids;
                if (reciperoque)
                {
                    voisins[indexTuileVoisine].ChangerPoid(tuile, nvPoids, false);
                }
            }
        }
}