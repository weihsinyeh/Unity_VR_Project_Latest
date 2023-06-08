using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Crossbow : MonoBehaviour
{
    [Header("Input")]
    public SteamVR_Action_Boolean FireVR;
 //   public Camera Cam;
    [Header("Object")]
    public GameObject ArrowPrefab;
    public Transform ArrowLaunch;
    public float ArrowSpeed;
    public float FireRate;
    private float firetimer;
    private Interactable interactable;

    private Vector3 destination;
    void Start()
    {
        ArrowLaunch.rotation = Quaternion.LookRotation(Vector3.forward);
        interactable = this.GetComponent<Interactable>();
    }

    void Update()
    {
        firetimer -= Time.deltaTime;                                                                 //minus 1 per second

        if(FireVR.GetStateDown(SteamVR_Input_Sources.RightHand) && firetimer <=0f && interactable.attachedToHand )          //if left click and fire timer less than zero
        {
            GameObject arrow = Instantiate(ArrowPrefab, ArrowLaunch.position, ArrowLaunch.rotation); //Instantiate the arrow
            
            arrow.GetComponent<Rigidbody>().AddForce()
            arrow.GetComponent<Rigidbody>().velocity = ArrowLaunch.transform.forward * ArrowSpeed;        //Set the velocity of the arrow
            firetimer = FireRate;                                                    // Makes the firetimer go back to the default firerate;     
        }
    }

}
