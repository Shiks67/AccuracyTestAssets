using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossController : MonoBehaviour
{

    private float timer = 0.25f;
    private float waitTime = 0;
    private Transform oldParent;

    // Use this for initialization
    void Start()
    {
        oldParent = gameObject.transform.parent;
        gameObject.transform.SetParent((gameObject.transform.parent).transform.parent);
    }

    // Update is called once per frame
    void Update()
    {
        if (oldParent == null)
            Destroy(gameObject);
        if (oldParent.gameObject.activeSelf == false)
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        else
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        //switch the color of the trigger
        waitTime += Time.deltaTime;
        if (waitTime < timer)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
        if (waitTime > timer)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
        if (waitTime > timer * 2)
        {
            waitTime = 0;
        }
    }
}
