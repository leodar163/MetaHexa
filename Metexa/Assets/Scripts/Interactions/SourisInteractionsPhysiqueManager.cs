using System;
using UnityEngine;

namespace Interactions
{
    public class SourisInteractionsPhysiqueManager : MonoBehaviour
    {
        private static IHoverable s_actuelHover;
        private static ISelectionable s_selectionActuelle;
        
        private void LateUpdate()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                DetecterHoverable(hit);
                DetecterClique(hit);
                DetecterSelection(hit);
            }

            if (Input.GetKeyUp(KeyCode.Escape))
            {
                Deselectionner();
            }
        }

        private void DetecterHoverable(RaycastHit hit)
        {
            if (hit.collider.TryGetComponent(out IHoverable hover))
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

        private void DetecterSelection(RaycastHit hit)
        {
            if (Input.GetMouseButtonUp(0) && hit.collider.TryGetComponent(out ISelectionable selection))
            {
                if (s_selectionActuelle == null)
                {
                    selection.QuandSelectionned();
                    s_selectionActuelle = selection;
                }
                else if (s_selectionActuelle != selection)
                {
                    s_selectionActuelle.QuandDeselectionned();
                    selection.QuandSelectionned();
                    s_selectionActuelle = selection;
                }
            }
        }

        private void Deselectionner()
        {
            if (s_selectionActuelle == null) return;
            s_selectionActuelle.QuandDeselectionned();
            s_selectionActuelle = null;
        }

        private void DetecterClique(RaycastHit hit)
        {
            if (hit.collider.TryGetComponent(out IClicable clique))
            {
                if (Input.GetMouseButtonUp(0))
                {
                    clique.QuandCliqueGauche();
                }

                if (Input.GetMouseButtonUp(1))
                {
                    clique.QuandCliqueDroit();
                }
            }
        }
    }
}