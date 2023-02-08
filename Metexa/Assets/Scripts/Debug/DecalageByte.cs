using System;
using UnityEngine;

namespace MetaHexa.Debug
{
    public class DecalageByte : MonoBehaviour
    {
        [SerializeField] private int _resultat;
        [SerializeField] private int _nbrBase;
        [SerializeField] private int _nbrDecalage;
        [Space] 
        [SerializeField] private TypeDecalage _typeDecalage;
        
        public enum TypeDecalage
        {
            droite,
            gauche
        }

        private void OnValidate()
        {
            switch (_typeDecalage)
            {
                case TypeDecalage.droite:
                    _resultat = _nbrBase >> _nbrDecalage;
                    break;
                case TypeDecalage.gauche:
                    _resultat = _nbrBase << _nbrDecalage;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}