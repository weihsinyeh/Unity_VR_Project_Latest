using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MoveStage : MonoBehaviour
{
    public TransitionManager transition;
    public int sceneIndex;
    public Color color;

     private void OnTriggerEnter(Collider other){
                 if(other.gameObject.layer == 3 ){
                    color = transition.FadeScreen.fadeColor;
                    transition.GoToSceneAsync(sceneIndex);
                 }
    }
}
