using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class WeaponHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public Hand.AttachmentFlags attachmentFlags;
    private Interactable currentInteractable;
    public Hand hand;
    public List<GameObject> weaponList;
    public RadialMenu radialMenu;
    public int weaponNum;
    public bool grab = false;


    void Start()
    {
        grab = false;
    }


    // Update is called once per frame
    public void ChangeToHand()
    {
        if (grab)
        {
             hand.DetachObject(weaponList[weaponNum]);
             hand.HoverUnlock(currentInteractable);
             weaponList[weaponNum].SetActive(false);
             weaponNum = 3;
             grab = false;
        }

    }
    public void ChangeWeapon()
    {
        if (radialMenu.index >= weaponList.Count) return;

        if (grab)
        {
            hand.DetachObject(weaponList[weaponNum]);
            hand.HoverUnlock(currentInteractable);
            weaponList[weaponNum].SetActive(false);
        }
        weaponNum = radialMenu.index;
        weaponList[weaponNum].SetActive(true);
        currentInteractable = weaponList[weaponNum].GetComponent<Interactable>();
        hand.AttachObject(weaponList[weaponNum], GrabTypes.Grip, attachmentFlags);
        grab = true;
    }
}
