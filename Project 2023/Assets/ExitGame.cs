using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
public class ExitGame : MonoBehaviour
{
    private Interactable interactable;

    // Start is called before the first frame update
    void Start()
    {
    interactable = GetComponent<Interactable>();
    }

    // Update is called once per frame
private void HandHoverUpdate(Hand hand){
        GrabTypes grabType = hand.GetGrabStarting();

        if (interactable.attachedToHand == null && grabType == GrabTypes.Grip)
        {
            Application.Quit();
        }
}
}
