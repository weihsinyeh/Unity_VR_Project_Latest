﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class PickWeaponVr : MonoBehaviour
{
    private Interactable interactable;
    public WeaponHandler weaponHandler;
    public PickUpArea pickUpArea;

    public bool Grabbed = false;
    public Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.ParentToHand | Hand.AttachmentFlags.DetachFromOtherHand | Hand.AttachmentFlags.TurnOnKinematic;

    void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    private void HandHoverUpdate(Hand hand)
    {
        if (!Grabbed && weaponHandler.weaponNum == 3)
        {
            GrabTypes grabType = hand.GetGrabStarting();

            if (interactable.attachedToHand == null && grabType == GrabTypes.Grip)
            {
                hand.AttachObject(gameObject, grabType, attachmentFlags);
                hand.HoverLock(interactable);           //锁定手部对物体的悬停（hover），防止其他物体的悬停操作
                                                        //  hand.HideGrabHint();
                weaponHandler.weaponList.Add(this.gameObject);
                weaponHandler.grab = true;
                weaponHandler.weaponNum = weaponHandler.weaponList.Count - 1;

                Grabbed = true;

                this.gameObject.GetComponent<Rigidbody>().useGravity = true;
                this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                pickUpArea.Picked = true;


            }
 
        }

    }







}
