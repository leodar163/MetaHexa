using System;
using UnityEngine;

namespace Graphics
{
    [RequireComponent(typeof(MeshRenderer))]
    public class MaterialTeinturier : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Color _teinte = Color.white;
        [SerializeField] private string nomCouleurShader = "BaseColor";

        private void OnValidate()
        {
            if (!_meshRenderer) TryGetComponent(out _meshRenderer);
            if (_meshRenderer) TeindreMaterial(_teinte);
        }

        public void TeindreMaterial(Color teinte)
        {
            if (!_meshRenderer.sharedMaterial)
            {
                return;
            }
            _teinte = teinte;
            if(nomCouleurShader.Length > 0)
                _meshRenderer.sharedMaterial.SetColor('_'+nomCouleurShader, _teinte);
        }
    }
}