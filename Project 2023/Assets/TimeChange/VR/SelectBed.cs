using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SelectBed : MonoBehaviour
{
    private Interactable interactable;

    public TransitionManager transition;
    public Color FadeColor;
    public int sceneIndex;
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
            transition.FadeScreen.fadeColor = FadeColor;
            transition.GoToSceneAsync(sceneIndex);
        }
    }
}



