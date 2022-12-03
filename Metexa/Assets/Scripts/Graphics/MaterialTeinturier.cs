using System;
using UnityEngine;

namespace Graphics
{
    [RequireComponent(typeof(MeshRenderer))]
    public class MaterialTeinturier : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private string nomCouleurShader = "BaseColor";

        protected bool _peutEtreTeint = true; 
        
        [SerializeField] private Color _teinte = Color.white;

        public Color teinte
        {
            get => _teinte;
            set
            {
                _teinte = value;
                if (_peutEtreTeint)
                {
                    TeindreMaterial();
                }
            }
        }

        private void OnValidate()
        {
            if (!_meshRenderer) TryGetComponent(out _meshRenderer);
            if (_meshRenderer) TeindreMaterial();
        }

        public void TeindreMaterial(Color couleurTeinture)
        {
            if (!_meshRenderer.sharedMaterial)
            {
                return;
            }
           
            if(nomCouleurShader.Length > 0)
                _meshRenderer.sharedMaterial.SetColor('_' + nomCouleurShader, couleurTeinture);
        }

        protected void TeindreMaterial()
        {
            if(!_peutEtreTeint) return;
            TeindreMaterial(_teinte);
        }
    }
}