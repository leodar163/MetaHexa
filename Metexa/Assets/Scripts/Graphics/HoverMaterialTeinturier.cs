using Interactions;
using UnityEngine;

namespace Graphics
{
    public class HoverMaterialTeinturier : MaterialTeinturier, IHoverable
    {
        [SerializeField] private Color _teinteHover = Color.white;
        private Color _teinteBuffer;
        
        public void QuandHoverCommence()
        {
            _teinteBuffer = teinte;
            TeindreMaterial(_teinteHover);
        }

        public void QuandHoverFini()
        {
            TeindreMaterial(_teinteBuffer);
        }

        public void QuandHoverReste()
        {
            _teinteBuffer = teinte;
        }
    }
}