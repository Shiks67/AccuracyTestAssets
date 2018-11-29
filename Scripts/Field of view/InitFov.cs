﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InitFov : MonoBehaviour
{

    public List<GameObject> horizontalTarget;
    public List<GameObject> verticalTarget;
    public List<GameObject> middleTarget;


    // Use this for initialization
    void Start()
    {
        SceneManager.UnloadSceneAsync("CircleTest");

        foreach (var go in horizontalTarget)
        {
            var subText = go.transform.Find("Canvas").gameObject;
            subText.transform.SetParent(null);

            go.transform.localScale = new Vector3(
                go.transform.localScale.x,
                go.transform.localScale.y,
                go.transform.localScale.z + FovStatic.horizontalSize
            );
            subText.transform.SetParent(go.transform);

            if (go.transform.parent.name.Contains("Left"))
            {
                go.transform.localPosition = new Vector3(
                    go.transform.localPosition.x + FovStatic.verticalSize * 5,
                    go.transform.localPosition.y,
                    go.transform.localPosition.z
                );
            }
            else
            {
                go.transform.localPosition = new Vector3(
                    go.transform.localPosition.x - FovStatic.verticalSize * 5,
                    go.transform.localPosition.y,
                    go.transform.localPosition.z
                );
            }
        }
        foreach (var go in verticalTarget)
        {
            var subText = go.transform.Find("Canvas").gameObject;
            subText.transform.SetParent(null);

            go.transform.localScale = new Vector3(
                go.transform.localScale.x + FovStatic.verticalSize,
                go.transform.localScale.y,
                go.transform.localScale.z
            );
            subText.transform.SetParent(go.transform);

            if (go.transform.parent.name.Contains("Up"))
            {
                go.transform.localPosition = new Vector3(
                    go.transform.localPosition.x,
                    go.transform.localPosition.y,
                    go.transform.localPosition.z - FovStatic.horizontalSize * 5
                );
            }
            else
            {
                go.transform.localPosition = new Vector3(
                    go.transform.localPosition.x,
                    go.transform.localPosition.y,
                    go.transform.localPosition.z + FovStatic.horizontalSize * 5
                );
            }
        }
        foreach (var go in middleTarget)
        {
            go.transform.localScale = new Vector3(
                go.transform.localScale.x + FovStatic.verticalSize,
                go.transform.localScale.y,
                go.transform.localScale.z + FovStatic.horizontalSize
            );
        }
    }
}
