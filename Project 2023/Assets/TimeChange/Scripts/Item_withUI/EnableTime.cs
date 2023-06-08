using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminosity.IO;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using Valve.VR;


public class EnableTime : MonoBehaviour
{
    private TimeShiftingController timeShiftingController;
    private DesaturateController desaturateController;
    public AudioManager audioManager;
    public bool Picked = false;

    [Header("PickUpDialogue")]
    public CanvasGroup PickUpCanvas;
    public float fadeTime = 0.5f;
    public string Text = "grip your right controller to pick";

    [Header("ItemDialogue")]
    public CanvasGroup ItemDialogue;

    private TMP_Text Canvas_text;

    private bool TimeBoxDestroy = false;

    // Start is called before the first frame update
    void Start()
    {
        desaturateController = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<DesaturateController>();
        timeShiftingController = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeShiftingController>();
        Canvas_text = PickUpCanvas.GetComponentInChildren<TMP_Text>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            if (!Picked)
            {
            Canvas_text.text = Text;
            PanelFadeIn(PickUpCanvas);
            }

        }

    }
    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            if (Picked && !TimeBoxDestroy)
            {
                PanelFadeOut(PickUpCanvas);
                audioManager.PlayAudio("Pick");
                desaturateController.CanStop = true;
                timeShiftingController.CanChange = true;
                PanelFadeIn(ItemDialogue);

                Destroy(this.transform.GetChild(0).gameObject);
                TimeBoxDestroy = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            if (!Picked)
                PanelFadeOut(PickUpCanvas);
            else
                PanelFadeOut(ItemDialogue);
        }

    }

    private void PanelFadeIn(CanvasGroup canvas)
    {
        canvas.alpha = 0f;
        canvas.DOFade(1f, fadeTime);
    }

    private void PanelFadeOut(CanvasGroup canvas)
    {
        canvas.alpha = 1f;
        canvas.DOFade(0f, fadeTime);
    }



}
