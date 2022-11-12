using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace PathFinding
{
    using Tuile;
    public class Graphe
    {
        public static List<Noeud> noeudsGeneraux = new List<Noeud>();
        public static Dictionary<Tuile, Tuile[]> noeudsPrincipaux = new Dictionary<Tuile, Tuile[]>();
        

        // public static Noeud TrouverNoeudParTuile(TriTuile tuile)
        // {
        //     return TrouverNoeudParTuile(tuile, noeudsGeneraux);
        // }
        //
        // public static Noeud TrouverNoeudParTuile(TriTuile tuile, ICollection<Noeud> noeuds)
        // {
        //     foreach (var noeud in noeuds)
        //     {
        //         if (noeud.tuile == tuile) return noeud;
        //     }
        //
        //     return null;
        // }
        //
        
        
        // public static void GenererGrapheGeneral(Mappe mappe)
        // {
        //     bool noeudsCreed = false;
        //     bool voisinsRenseigned = false;
        //     while (!voisinsRenseigned)
        //     {
        //         for (int j = 0; j < mappe.Tuiles.GetLength(1); j++)
        //         {
        //             for (int i = 0; i < mappe.Tuiles.GetLength(0); i++)
        //             {
        //                 for (int k = 0; k < mappe.Tuiles[i,j].TriTuiles.Length; k++)
        //                 {
        //                     if (!noeudsCreed) noeudsGeneraux.Add(new Noeud(mappe.Tuiles[i,j].TriTuiles[k]));
        //                     else TrouerVoisinNoeud(TrouverNoeudParTuile(mappe.Tuiles[i,j].TriTuiles[k]), k);
        //                 }
        //             }
        //         }
        //         voisinsRenseigned = noeudsCreed;
        //         noeudsCreed = true;
        //     }
        //     
        // }
        //
        // private static void TrouerVoisinNoeud(Noeud noeud, int indexTriTuile)
        // {
        //     List<Noeud> noeudAdjacents = new List<Noeud>();
        //     float decalageRad = indexTriTuile is < 5 or > 18 && indexTriTuile % 2 != 0 ||
        //         indexTriTuile is >= 5 and <= 18 && indexTriTuile % 2 == 0 ? 0 : Mathf.PI;
        //     float cranRad = 2 * Mathf.PI / 3;
        //     
        //     for (int i = 0; i < 3; i++)
        //     {
        //         Vector3 decalagePos = new Vector3
        //         {
        //             x = Mathf.Cos(cranRad * i + decalageRad) * 0.62f,
        //             z = Mathf.Sin(cranRad * i + decalageRad) * 0.62f
        //         };
        //
        //         Collider[] autres = Physics.OverlapBox(noeud.tuile.transform.position + decalagePos,
        //             Vector3.one * 0.01f, new Quaternion(), LayerMask.GetMask("Trituile"));
        //
        //         if(indexTriTuile == 0) Debug.DrawLine(noeud.tuile.transform.position + decalagePos, 
        //             noeud.tuile.transform.position + decalagePos + Vector3.up * 10, Color.black, 1000);
        //         
        //         if (autres.Length > 0 && autres[0].TryGetComponent(out TriTuile autreTuile))
        //         {
        //             noeudAdjacents.Add(TrouverNoeudParTuile(autreTuile));
        //         }
        //     }
        //     noeud.Voisins = noeudAdjacents.ToArray();
        // }
    }
}