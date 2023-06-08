using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminosity.IO;

public class PlayerCamVR : MonoBehaviour
{
    //public Transform headpoint;
    public Transform playerBody;
    public Transform playerVR;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MouseControl();
        MoveCam();
    }

    void MouseControl()
    {
        if (!PlayerMovement.isTransport)
        {
            PlayerMovement.onWall = false;
            if (!PlayerMovement.onWall)
            {
                playerBody.rotation = Quaternion.Euler(playerBody.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, playerBody.rotation.eulerAngles.z); //¤Hª«ªºrotation  yRotation
                                                                                                                                  // Debug.Log("CameraControl");
            }
        }
    }

    void MoveCam()
    {

        playerVR.transform.localPosition = playerBody.localPosition;
    }







}
