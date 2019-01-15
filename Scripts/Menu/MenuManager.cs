using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public GameObject menu;
    private string accuracyTest = "CircleTest";
    private string fovCalibration = "Field of view";
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            menu.gameObject.SetActive(!menu.activeSelf);
    }

    public void StartAccuTest()
    {
        if (SceneManager.GetSceneByName("Field of view").isLoaded)
            StopFovCalibration();
        if (SceneManager.GetSceneByName("CircleTest").isLoaded)
            return;
        menu.gameObject.SetActive(false);
        mainCamera.enabled = false;
        StartCoroutine(LoadCurrentScene(accuracyTest));
    }

    public void StopAccuTest()
    {
        SceneManager.UnloadSceneAsync(accuracyTest);
        menu.gameObject.SetActive(false);
        mainCamera.enabled = true;
    }

    public void StartFovCalibration()
    {
        if (SceneManager.GetSceneByName("CircleTest").isLoaded)
            StopAccuTest();
        if (SceneManager.GetSceneByName("Field of view").isLoaded)
            return;
        menu.gameObject.SetActive(false);
        mainCamera.enabled = false;
        StartCoroutine(LoadCurrentScene(fovCalibration));
    }

    public void StopFovCalibration()
    {
        SceneManager.UnloadSceneAsync(fovCalibration);
        menu.gameObject.SetActive(false);
        mainCamera.enabled = true;
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
