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

    public void CalibrateFov()
    {
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
