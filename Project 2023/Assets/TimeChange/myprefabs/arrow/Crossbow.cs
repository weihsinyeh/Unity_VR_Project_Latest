using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Crossbow : MonoBehaviour
{
    [Header("Input")]
    public SteamVR_Action_Boolean FireVR;
    public Camera cam;

    [Header("Object")]
    public GameObject ArrowPrefab;
    public Transform ArrowLaunch;
    public float ArrowSpeed;
    public float FireRate;
    private float firetimer;

    private Vector3 destination;
    void Start()
    {
        ArrowLaunch.rotation = Quaternion.LookRotation(Vector3.forward);
    }

    void Update()
    {
        firetimer -= Time.deltaTime;                                                                 //minus 1 per second

        if((FireVR.GetStateDown(SteamVR_Input_Sources.RightHand)) && firetimer <=0f)          //if left click and fire timer less than zero
        {
            Vector3 middleofScreen = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 80f));   //Find the middle of the screen with z offset of 100f (fakes shooting to the middle)
            ArrowLaunch.LookAt(middleofScreen);                                                       //makes the launchtransform look at it
            GameObject arrow = Instantiate(ArrowPrefab, ArrowLaunch.position, ArrowLaunch.rotation); //Instantiate the arrow
                                                                                                  
            arrow.GetComponent<Rigidbody>().velocity = ArrowLaunch.transform.forward * ArrowSpeed;        //Set the velocity of the arrow
            firetimer = FireRate;                                                    // Makes the firetimer go back to the default firerate;     
        }
    }

}
