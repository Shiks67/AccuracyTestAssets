using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SwitchToFov : MonoBehaviour
{
    public void StartFovScene()
    {
        StartCoroutine(LoadCurrentScene("Field of view"));
        gameObject.SetActive(false);
        SceneManager.UnloadSceneAsync("CircleTest");
    }

    IEnumerator LoadCurrentScene(string sceneName)
    {
        AsyncOperation asyncScene = SceneManager.LoadSceneAsync(sceneName
            , LoadSceneMode.Additive);

        while (!asyncScene.isDone)
        {
            yield return null;
        }
    }
}
