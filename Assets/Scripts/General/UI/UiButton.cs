using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace General.UI
{
    public class UiButton : XRSimpleInteractable
    {
        public override bool IsSelectableBy(IXRSelectInteractor interactor)
        {
            return false;
        }

        public void MakeClickable()
        {
            GetComponent<Collider>().enabled = true;
        }
    }
}
