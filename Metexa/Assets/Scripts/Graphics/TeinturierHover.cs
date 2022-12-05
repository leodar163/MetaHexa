using Graphics;
using Interactions;
using UnityEngine;
using UnityEngine.Events;

namespace Graphics
{
    public class TeinturierHover : MaterialTeinturier, IHoverable
    {
        [SerializeField] private Color _teinteHover = Color.white;

        [Header("Events")] 
        public UnityEvent quandHovered = new UnityEvent();
        public UnityEvent quandHoverCommence = new UnityEvent();
        public UnityEvent quandHoverFinit = new UnityEvent();
        
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
            
            quandHoverCommence.Invoke();
        }

        public void QuandHoverFini()
        {
            _peutEtreTeint = true;
            _estHovered = false;
            TeindreMaterial();
            quandHoverFinit.Invoke();
        }

        public void QuandHoverReste()
        {
            quandHovered.Invoke();
        }
    }
}