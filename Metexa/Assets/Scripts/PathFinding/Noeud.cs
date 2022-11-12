using System;
using System.Collections.Generic;
using Tuile;
using UnityEngine;

namespace PathFinding
{
    public class Noeud
        {
            public TriTuile tuile;

            private Noeud[] _voisins ;


            public class PathfindingInfos
            {
                public float coutDepla = 0;
                public float coutHeuri = 0;
                public float coutTotal => coutDepla + coutHeuri;
                public Noeud parent;
            }

            public PathfindingInfos pathFindingInfos => new PathfindingInfos();
            
            public Noeud(TriTuile _tuile)
            {
                tuile = _tuile;
            }
            
            
            public Noeud[] Voisins
            {
                get => _voisins;
                private set
                {
                    _voisins = value;
                    InitialiserPoids();
                }
            }

            private float[] poids;
            public float[] Poids => poids;

            private void InitialiserPoids()
            {
                if (_voisins.Length > 0)
                {
                    poids = new float[_voisins.Length];
                    for (int i = 0; i < poids.Length; i++)
                    {
                        poids[i] = 1;
                    }
                }
            }

            // todo : implémenter ça dans Graphe pour éviter que les changements ne soit répercutés que sur des noeuds temporaires
            // public void ChangerPoid(TriTuile tuileVoisine, float nvPoids, bool reciperoque = true)
            // {
            //     //todo : verifier que la tuile voisine est bien une tuile voisine
            //     int index = Array.LastIndexOf(_voisins, Graphe.TrouverNoeudParTuile(tuileVoisine, _voisins));
            //     ChangerPoid(index, nvPoids, reciperoque);
            // }
            //
            // public void ChangerPoid(int indexTuileVoisine, float nvPoids, bool reciperoque = true)
            // {
            //     poids[indexTuileVoisine] = nvPoids;
            //     if (reciperoque)
            //     {
            //         _voisins[indexTuileVoisine].ChangerPoid(tuile, nvPoids, false);
            //     }
            // }
            
            public void TrouverVoisins()
            {
                List<Noeud> noeudAdjacents = new List<Noeud>();
                float decalageRad = tuile.transform.eulerAngles.y * Mathf.Deg2Rad;
                float cranRad = 2 * Mathf.PI / 3;
            
                for (int i = 0; i < 3; i++)
                {
                    Vector3 decalagePos = new Vector3
                    {
                        x = Mathf.Cos(cranRad * i + decalageRad) * 0.62f,
                        z = Mathf.Sin(cranRad * i + decalageRad) * 0.62f
                    };

                    Collider[] autres = Physics.OverlapBox(tuile.transform.position + decalagePos,
                        Vector3.one * 0.01f, new Quaternion(), TriTuile.maskTuile);
                
                
                    if (autres.Length > 0 && autres[0].TryGetComponent(out TriTuile autreTuile))
                    {
                        noeudAdjacents.Add(autreTuile.noeud);
                    }
                }
            
                Voisins = noeudAdjacents.ToArray();
            }
        }
}