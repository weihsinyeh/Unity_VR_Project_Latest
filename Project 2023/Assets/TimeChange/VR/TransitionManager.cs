using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public FadeScreenEffect FadeScreen;
    
    public void GoToSceneAsync(int scneneIndex)
    {
        StartCoroutine(GoToSceneAsyncRoutine(scneneIndex));
    }

    IEnumerator GoToSceneAsyncRoutine(int sceneIndex)
    {
        FadeScreen.FadeOut();
        //Launch the new Scene
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        operation.allowSceneActivation = false;
        float timer = 0;
        while(timer <= FadeScreen.fadeDuration && !operation.isDone)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        operation.allowSceneActivation = true;
    }
}
