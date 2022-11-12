using System;
using UnityEngine;

namespace Graphics
{
    [RequireComponent(typeof(MeshRenderer))]
    public class MaterialInstancieur : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Material _materialAInstencier;
        [Space] [SerializeField] private bool _reinit;

        private void OnValidate()
        {
            if (!_meshRenderer) TryGetComponent(out _meshRenderer);
            if (!_materialAInstencier) TryGetComponent(out _materialAInstencier);

            if (_meshRenderer && _materialAInstencier && (!_meshRenderer.sharedMaterial || _reinit))
            {
                AssignerMaterialInstancied();
            }
        }

        private void AssignerMaterialInstancied()
        {
            _meshRenderer.sharedMaterial = new Material(_materialAInstencier);
        }
        
        
    } 
}