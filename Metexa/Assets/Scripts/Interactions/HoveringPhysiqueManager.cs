using System;
using UnityEngine;

namespace Interactions
{
    public class HoveringPhysiqueManager : MonoBehaviour
    {
        private static IHoverable s_actuelHover;
        
        private void LateUpdate()
        {
            DetecterHoverable();   
        }

        private void DetecterHoverable()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitTest))
            {
                print(hitTest.collider.gameObject);
            }
            
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.TryGetComponent(out IHoverable hover))
            {
                if(hover == s_actuelHover) hover.QuandHoverReste();
                else
                {
                    hover.QuandHoverCommence();
                    s_actuelHover?.QuandHoverFini();
                    s_actuelHover = hover;
                }
            }
            else
            {
                if (s_actuelHover != null)
                {
                    s_actuelHover.QuandHoverFini();
                    s_actuelHover = null;
                }
            }
        }
    }
}