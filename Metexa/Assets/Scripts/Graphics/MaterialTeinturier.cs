using UnityEngine;

namespace Graphics
{
    [RequireComponent(typeof(MeshRenderer))]
    public class MaterialTeinturier : MaterialInstancieur
    {
        protected bool _peutEtreTeint = true;
        [SerializeField] private bool _ecraserAlpha = true;
        [SerializeField] private Color teinteBaseBase = Color.white;

        public Color teinteBase
        {
            get => teinteBaseBase;
            set
            {
                teinteBaseBase = value;
                if (_peutEtreTeint)
                {
                    TeindreMaterial();
                }
            }
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            if (_meshRenderer) TeindreMaterial();
        }

        protected void TeindreMaterial(Color couleurTeinture)
        {
            if (!_meshRenderer.sharedMaterial)
            {
                return;
            }

            if (_nomCouleurShader.Length > 0)
            {
                Color teinte = couleur * couleurTeinture;
                if (_ecraserAlpha) teinte.a = couleurTeinture.a;
                _meshRenderer.sharedMaterial.SetColor('_' + _nomCouleurShader, teinte);
            }
        }

        
        
        protected void TeindreMaterial()
        {
            if(!_peutEtreTeint) return;
            TeindreMaterial(teinteBaseBase);
        }
    }
}