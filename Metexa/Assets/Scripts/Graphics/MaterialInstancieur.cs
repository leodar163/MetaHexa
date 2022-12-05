using System;
using UnityEngine;

namespace Graphics
{
    [RequireComponent(typeof(MeshRenderer))]
    public class MaterialInstancieur : MonoBehaviour
    {
        [SerializeField] protected MeshRenderer _meshRenderer;
        [SerializeField] private Material _materialAInstencier;
        [Space] [SerializeField] private bool _reinit;

        [SerializeField] protected string _nomCouleurShader = "BaseColor";
        
        [SerializeField] private Color _couleur = Color.white;

        public Color couleur
        {
            get => _couleur;
            set
            {
                _couleur = value;
                AppliquerCouleur();
            }
        }
        
        protected virtual void OnValidate()
        {
            if (!_meshRenderer) TryGetComponent(out _meshRenderer);
            if (!_materialAInstencier) TryGetComponent(out _materialAInstencier);

            if (_meshRenderer && (!_meshRenderer.sharedMaterial || _reinit))
            {
                _reinit = false;
                AssignerMaterialInstancied();
            }
            
            AppliquerCouleur();
        }

        private void AppliquerCouleur()
        {
            if (_meshRenderer && _meshRenderer.sharedMaterial)
            {
                _meshRenderer.sharedMaterial.SetColor('_' + _nomCouleurShader, couleur);
            }
        }

        private void AssignerMaterialInstancied()
        {
            if (_meshRenderer && _materialAInstencier)
            {
                _meshRenderer.sharedMaterial = new Material(_materialAInstencier);
            }
        }
        
        
    } 
}