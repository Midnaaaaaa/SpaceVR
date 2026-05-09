using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabInteractableGravity : XRGrabInteractable
{
    private IXRSelectInteractor hand;
    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);
        GetComponent<Rigidbody>().useGravity = GravityController.Instance.GetGravity();
        hand = null;
    }
    
    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        hand = args.interactorObject;
    }

    public IXRSelectInteractor GetHand()
    {
        return hand;
    }
}
