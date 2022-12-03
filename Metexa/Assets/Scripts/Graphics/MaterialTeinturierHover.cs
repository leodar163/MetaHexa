using Interactions;
using UnityEngine;

namespace Graphics
{
    public class MaterialTeinturierHover : MaterialTeinturier, IHoverable
    {
        [SerializeField] private Color _teinteHover = Color.white;

        public Color teinteHover
        {
            get => _teinteHover;
            set
            {
                _teinteHover = value;
                
                if(estHovered) TeindreMaterial(_teinteHover);
            }
        }

        private bool _estHovered;
        public bool estHovered => _estHovered;

        public void QuandHoverCommence()
        {
            _peutEtreTeint = false;
            _estHovered = true;
            TeindreMaterial(_teinteHover);
        }

        public void QuandHoverFini()
        {
            _peutEtreTeint = true;
            _estHovered = false;
            TeindreMaterial();
        }

        public void QuandHoverReste()
        {
            
        }
    }
}