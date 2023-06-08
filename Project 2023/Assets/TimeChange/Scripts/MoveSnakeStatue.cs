using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MoveSnakeStatue : MonoBehaviour
{
    public FadeScreenEffect FadeScreen;
    public GameObject Grave;

    public int gravenum = 0;
    public Animator snake;
    public GameObject graveSide;
    public GameObject Audio;
    private StageAudio stA;
    private bool NotChange = true;

    private void Start()
    {
        stA = FindObjectOfType<StageAudio>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Grave.activeInHierarchy && NotChange)
        {
            FadeScreen.FadeIn();
            NotChange = false;
        }
      
        if(gravenum == 2)
        {
            graveSide.SetActive(false);
            FadeScreen.FadeOut();
            Audio.SetActive(true);
            snake.Play("Open");
            stA.stage = 1;
        }
    }
}
