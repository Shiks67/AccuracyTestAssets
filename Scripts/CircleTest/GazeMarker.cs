﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeMarker : MonoBehaviour
{

    public GameObject gazeDotMap;
    public GameObject dotMark;
    public static List<GameObject> listDotMark = new List<GameObject>();
    public static List<GameObject> oldListDotMark = new List<GameObject>();
    private RayCaster rCaster;
    private Vector3 pos;

    void Start()
    {
        // rCaster = GameObject.FindGameObjectWithTag("EditorOnly").GetComponent<RayCastF>();
        rCaster = Camera.main.GetComponent<RayCaster>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject hitObject;
        RaycastHit[] hits;

        // everything that the raycast hit
        hits = Physics.RaycastAll(rCaster.ray);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            hitObject = hit.collider.gameObject;
            pos = gameObject.transform.localPosition;

            // if a circle is hitted and the timer is at 0
            // create a dot at the position on the background's collider
            if (SpawnCircle.targetCircle.Contains(hitObject) &&
            SpawnCircle.targetCircle.Count == 1)
            {
                GameObject newObject = Instantiate(dotMark);
                //set parent so the dots stay at the same place on the grid
                //position from the hit.point on the background
                newObject.transform.localPosition = pos;
                newObject.transform.SetParent(gazeDotMap.transform);
                newObject.GetComponent<Renderer>().material.color = Color.black;
                listDotMark.Add(newObject);
            }
        }
    }
}
