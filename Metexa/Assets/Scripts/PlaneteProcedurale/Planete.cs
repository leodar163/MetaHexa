using System;
using System.Collections;
using System.Collections.Generic;
using Graphics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace MetaHexa.PlaneteProcedurale
{
    public class Planete : MonoBehaviour
    {
        public float rayon { get; private set; }
        private int _definition;
        private int _latitudes;

        private TuilePlanete[] _tuiles;
        private int[,] graph;

        [FormerlySerializedAs("_tuileTemplate")] [SerializeField] private TuilePlanete tuilePlaneteTemplate;

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                GenererPlanete(0);
            }
            if (Input.GetKeyUp(KeyCode.G))
            {
                GenererPlanete(1);
            }
            if (Input.GetKeyUp(KeyCode.H))
            {
                GenererPlanete(2);
            }
            if (Input.GetKeyUp(KeyCode.J))
            {
                GenererPlanete(3);
            }
        }

        public void GenererPlanete(uint taillePlanete)
        {
            rayon = Mathf.Pow(2, taillePlanete);
            _definition = (int)taillePlanete;
            _latitudes = 3 * Mathf.RoundToInt(Mathf.Pow(2, _definition));
            
            List<Mesh> meshesTuile = MeshCollection.GenererSphereATuilesExtrudables(taillePlanete);

            nettoyerTuiles();

            _tuiles = new TuilePlanete[meshesTuile.Count];
            
            for (int i = 0; i < meshesTuile.Count; i++)
            {
                AjouterTuile(meshesTuile[i], i);
            }
        }

        public void RandomiserHauteurTuiles()
        {
            if(_tuiles == null) return;
            
            foreach (var tuile in _tuiles)
            {
                tuile.Hauteur = Random.Range(-0.5f, 0.5f);
            }
        }
        
        private void AjouterTuile(Mesh meshTuile, int index)
        {
            if (Instantiate(tuilePlaneteTemplate, transform).TryGetComponent(out TuilePlanete nvlleTuile))
            {
                nvlleTuile.Init(meshTuile,this);
                nvlleTuile.name = $"tuile {index}";
                _tuiles[index] = nvlleTuile;
            }
        }

        private void nettoyerTuiles()
        {
            if(_tuiles == null) return;
            
            foreach (var tuile in _tuiles)
            {
                Destroy(tuile.gameObject);
            }

            _tuiles = Array.Empty<TuilePlanete>();
        }

        private void GenererGraphe()
        {
            graph = new int[_tuiles.Length, 3];

            int[] coord = new int[20 * Mathf.RoundToInt(Mathf.Pow(4,_definition))];

            for (int latitude = 0; latitude < _latitudes; latitude++) 
            {
                for (int longitude = 0; longitude < NbrTuilePourLatitude(latitude); longitude++)
                {
                    
                }   
            }
        }

        private int NbrTuilePourLatitude(int latitude)
        {
            switch (latitude / (_latitudes / 3))
            {
                case 0:
                    return latitude % (_latitudes / 3) * 10 + 5;
                case 1:
                    return 10 * Mathf.RoundToInt(Mathf.Pow(2, _definition));
                case 2:
                    return 10 * (_latitudes / 3) - latitude % (_latitudes / 3) * 10 + 5;
                default:
                    return -1;
            }
        }
        
        private int TrouverTuilePlusProche()
        {
            return -1;
        }
    }
}
