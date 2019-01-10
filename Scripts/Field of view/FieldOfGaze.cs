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

        if (FovStatic.upLeftPos == new Vector3(0, 0, 0) && FovStatic.upLeftPos != upLeft.transform.localPosition)
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
        
        var ulLine = upLeft.GetComponent<LineRenderer>();
        ulLine.startWidth = 0.008f;
        ulLine.endWidth = 0.008f;
        ulLine.SetPosition(0,upLeft.transform.position);
        ulLine.SetPosition(1,upRight.transform.position);

        var urLine = upRight.GetComponent<LineRenderer>();
        urLine.startWidth = 0.008f;
        urLine.endWidth = 0.008f;
        urLine.SetPosition(0,upRight.transform.position);
        urLine.SetPosition(1,downRight.transform.position);

        var drLine = downRight.GetComponent<LineRenderer>();
        drLine.startWidth = 0.008f;
        drLine.endWidth = 0.008f;
        drLine.SetPosition(0,downRight.transform.position);
        drLine.SetPosition(1,downLeft.transform.position);

        var dlLine = downLeft.GetComponent<LineRenderer>();
        dlLine.startWidth = 0.008f;
        dlLine.endWidth = 0.008f;
        dlLine.SetPosition(0,downLeft.transform.position);
        dlLine.SetPosition(1,upLeft.transform.position);
    }
}
