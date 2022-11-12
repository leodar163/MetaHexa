using System;
using UnityEngine;

namespace PathFinding
{
    using Tuile;
    public class Mappe : MonoBehaviour
    {
        private static Mappe cela;

        public static Mappe Singleton
        {
            get
            {
                if (!cela) cela = FindObjectOfType<Mappe>();
                if (!cela) throw new NullReferenceException("Il manque un objet Mappe dans la scene");
                return cela;
            }
        }

        private Tuile[,] tuiles;

        public Tuile[,] Tuiles
        {
            get
            {
                if (tuiles.Length == 0) throw new NullReferenceException("La mappe est vide");
                return tuiles;
            }
        }

        private void Start()
        {
            // tuiles = new Tuile[,]
            // {
            //     {FindObjectOfType<Tuile>()}
            // };
            // Graphe.GenererGrapheGeneral(this);
        }
    }
}