using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CircleLife : MonoBehaviour
{
    public float TTFF;
    public bool isTTFF;
    private int index;
    public void Init(int index)
    {
        this.index = index;
    }

    // Use this for initialization
    void Start()
    {
        isTTFF = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if there is more than 1 circle, return
        if (SpawnCircle.targetCircle.Count > 1)
            return;
        if (isTTFF) //update TTFF time until the circle is focused by the gaze point
            TTFF += Time.deltaTime;
        if (gameObject.transform.localScale.x < 30)
            isTTFF = false;
        if (Input.GetKeyUp(KeyCode.Space))
        {
            //save the circle's scale and destroy it
            SpawnCircle.circleFinalSize[index] = gameObject.transform.localScale.x;
            SpawnCircle.targetCircle.Remove(gameObject);
            Destroy(gameObject);
            GazeMarker.oldListDotMark.AddRange(GazeMarker.listDotMark);
            GazeMarker.listDotMark.Clear();
        }
    }
}
