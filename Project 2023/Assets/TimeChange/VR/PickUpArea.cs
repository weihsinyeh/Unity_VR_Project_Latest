using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PickUpArea : MonoBehaviour
{
    public AudioManager audioManager;
  
    [Header("PickUpDialogue")]
    public CanvasGroup PickUpCanvas;
    public float fadeTime = 0.5f;
    public string Text = "grip your right controller to pick";
  
    [Header("ItemDialogue")]
    public CanvasGroup ItemDialogue;
  
    public bool Picked = false;
    private bool Take = false;
  
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3 && other.gameObject.tag!= "Weapon")
        {
            if (!Picked)
            {
                PanelFadeIn(PickUpCanvas);
            }
            else
            {
                PanelFadeIn(ItemDialogue);
            }
        }
    }
       private void OnTriggerStay(Collider other)
       {
           if (Picked && !Take)
           {
               PanelFadeOut(PickUpCanvas);
               audioManager.PlayAudio("Pick");
               PanelFadeIn(ItemDialogue);
               Take = true;
           }
     
       }
     
     
     
     
      private void OnTriggerExit(Collider other)
      {
          if (other.gameObject.layer == 3 && other.gameObject.tag != "Weapon")
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
