using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminosity.IO;
using DG.Tweening;
using TMPro;
using Valve.VR;

public class EnterAltar : MonoBehaviour
{
    public CanvasGroup BlackCanvas;
    public PickUpArea pickUpArea_new;

    [Header("Input")]
    public SteamVR_Action_Boolean PickVR;

    [Space]
    [Header("Weapon")]
    public GameObject Rocket;
    public GameObject RocketCG;

    public WeaponHandler weaponHandler;
    public GameObject FireBullet;
    public Material RocketFireMat;
    public GameObject timeline;


    private bool put = false;
    private bool enter = false;
    private bool fadeOut = false;

    [Header("PickUpDialogue")]
    public CanvasGroup PickUpCanvas;
    public float fadeTime = 0.5f;

    private TMP_Text Canvas_text;


    private void Start()
    {
        Canvas_text = PickUpCanvas.GetComponentInChildren<TMP_Text>();
    }

    private void Update()
    {
        if (BlackCanvas.alpha == 1f && !fadeOut && put)
        {
            PanelFadeOut(BlackCanvas, 1f);
            timeline.SetActive(true);
            fadeOut = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3 && !enter)
        {
            enter = true;

            Canvas_text.text = "Put the RocketLauncher at the middle (Right Grip)";
            PanelFadeIn(PickUpCanvas, fadeTime);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (enter && other.gameObject.layer == 3)
        {
            if (weaponHandler.hand.currentAttachedObject == Rocket && PickVR.GetStateDown(SteamVR_Input_Sources.RightHand) && !put)
            {
                weaponHandler.ChangeToHand();
                weaponHandler.weaponList.Remove(Rocket);

                Rocket.SetActive(true);
                Rocket.GetComponent<Rigidbody>().useGravity = false;
                Rocket.GetComponent<Rigidbody>().isKinematic = true;
                Rocket.GetComponent<PickWeaponVr>().enabled = false;

                Canvas_text.text = "Danger! Please get away from the Altar";
                put = true;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3 && enter)
        {
            enter = false;
            PanelFadeOut(PickUpCanvas, fadeTime);

            if (put)
            {
                Rocket.SetActive(false);
                PanelFadeIn(BlackCanvas, 1f);
            }


        }
    }
    public void DestroyTheAltar()
    {
        PanelFadeOut(BlackCanvas, 1f);
        Destroy(timeline);
        Destroy(this.gameObject);
    }



    private void PanelFadeIn(CanvasGroup canvasGroup, float fadeTime)
    {
        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1f, fadeTime);
    }

    private void PanelFadeOut(CanvasGroup canvasGroup, float fadeTime)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.DOFade(0f, fadeTime);
    }

    public void changeRocket()
    {
        Rocket.GetComponent<PickWeaponVr>().enabled = true;
        Rocket.GetComponent<PickWeaponVr>().pickUpArea = pickUpArea_new;
        Rocket.GetComponent<Renderer>().material = RocketFireMat;
        Rocket.GetComponent<Crossbow>().ArrowPrefab = FireBullet;
        Rocket.transform.localPosition = RocketCG.transform.localPosition;
        RocketCG.GetComponent<Renderer>().material = RocketFireMat;
    }
}
