using System;
using MetaHexa.PlaneteProcedurale;
using UnityEngine;

namespace MetaHexa.Debug
{
    public class CoordonneesTest : MonoBehaviour
    {
        [SerializeField] private bool _refresh;
        [SerializeField] private Vector3 _positionToTest;


        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(Vector3.zero, 1);
            Gizmos.DrawSphere(_positionToTest, 0.1f);
        }

        private void OnValidate()
        {
            _positionToTest.Normalize();

            if (_refresh)
            {
                _refresh = false;
                Tests();   
            }
        }

        private void Tests()
        {
            Vector3 position1 = _positionToTest;
            Coordonnee coordonnee1 = new Coordonnee(position1);
            
            print(position1 + " => " + coordonnee1);

            Vector3 position2 = coordonnee1.VersPosition();

            print(coordonnee1 + " => " + position2);

            Coordonnee coordonnee2 = new Coordonnee(position2);

            print(position2 + " => " + coordonnee2);
            
            print("Equivalence réussite =" + (position1 == position2));
        }
    }
}