using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FieldOfGaze : MonoBehaviour
{

    public GameObject upLeft;
    public GameObject upRight;
    public GameObject downLeft;
    public GameObject downRight;

    void Update()
    {
        if (SceneManager.GetSceneByName("Field of view").isLoaded)
            gameObject.GetComponent<Camera>().enabled = false;
        else
        { gameObject.GetComponent<Camera>().enabled = true; }

        if (FovStatic.upLeftPos == new Vector3(0, 0, 0))
        {
            upLeft.gameObject.SetActive(false);
            upRight.gameObject.SetActive(false);
            downLeft.gameObject.SetActive(false);
            downRight.gameObject.SetActive(false);
            return;
        }

        upLeft.gameObject.SetActive(true);
        upRight.gameObject.SetActive(true);
        downLeft.gameObject.SetActive(true);
        downRight.gameObject.SetActive(true);

        upLeft.transform.localPosition = FovStatic.upLeftPos;
        upRight.transform.localPosition = FovStatic.upRightPos;
        downLeft.transform.localPosition = FovStatic.downLeftPos;
        downRight.transform.localPosition = FovStatic.downRightPos;
    }
}
