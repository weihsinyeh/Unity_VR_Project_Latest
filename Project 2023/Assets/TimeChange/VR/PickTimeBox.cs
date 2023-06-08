using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PickTimeBox : MonoBehaviour
{
    private Interactable interactable;
    private EnableTime enableTime;

    // Start is called before the first frame update
    void Start()
    {

        interactable = GetComponent<Interactable>();
        enableTime = GetComponentInParent<EnableTime>();
    }

    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes grabType = hand.GetGrabStarting();

        if (interactable.attachedToHand == null && grabType == GrabTypes.Grip)
        {
            enableTime.Picked = true;
        }
    }
}
