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
        public float taille { get; private set; }

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
            taille = Mathf.Pow(2, taillePlanete);
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

            for (int i = 0; i < _tuiles.Length; i++)
            {
                
            }
        }
    }
}
