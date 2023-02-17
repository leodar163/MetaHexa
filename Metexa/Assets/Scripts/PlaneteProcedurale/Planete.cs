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

        private List<TuilePlanete> _tuiles = new List<TuilePlanete>();

        [FormerlySerializedAs("_tuileTemplate")] [SerializeField] private TuilePlanete tuilePlaneteTemplate;

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.G))
            {
                GenererPlanete(3);
            }
        }

        public void GenererPlanete(uint taillePlanete)
        {
            taille = Mathf.Pow(2, taillePlanete);
            List<Mesh> meshesTuile = MeshCollection.GenererSphereATuilesExtrudables(taillePlanete);

            nettoyerTuiles();

            foreach (var mesh in meshesTuile)
            {
                AjouterTuile(mesh);
            }
        }

        public void RandomiserHauteurTuiles()
        {
            foreach (var tuile in _tuiles)
            {
                tuile.Hauteur = Random.Range(-0.5f, 0.5f);
            }
        }
        
        private void AjouterTuile(Mesh meshTuile)
        {
            if (Instantiate(tuilePlaneteTemplate, transform).TryGetComponent(out TuilePlanete nvlleTuile))
            {
                nvlleTuile.Init(meshTuile,this);
                nvlleTuile.name = $"tuile {_tuiles.Count}";
                _tuiles.Add(nvlleTuile);
            }
        }

        private void nettoyerTuiles()
        {
            foreach (var tuile in _tuiles)
            {
                Destroy(tuile.gameObject);
            }
            _tuiles.Clear();
        }
    }
}
