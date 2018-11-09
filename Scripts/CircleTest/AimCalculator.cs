using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class AimCalculator : MonoBehaviour
{
    private GameObject[] lastDots = new GameObject[10];
    private Vector3 accuracyPoint = new Vector3(0, 0, -0.1f);
    // private Vector3 upL = new Vector3(0, 0, 9);
    // private Vector3 upR = new Vector3(0, 0, 9);
    // private Vector3 downL = new Vector3(0, 0, 9);
    // private Vector3 downR = new Vector3(0, 0, 9);

    private float xScale, yScale;
    private float midX, midY;

    // Use this for initialization
    void Start()
    { }

    // Update is called once per frame
    void Update()
    {
        if (GazeMarker.listDotMark.Count <= 10)
        {
            gameObject.GetComponent<Image>().enabled = false;
            return;
        }
        gameObject.GetComponent<Image>().enabled = true;
        print("before for : ");
        for (int i = 0; i < 10; i++)
        {
            lastDots[i] = GazeMarker.listDotMark
            [(GazeMarker.listDotMark.Count - 10) + i];
        }
        print("after for : ");
        accuracyPoint.x = lastDots.Average(item => item.gameObject.transform.localPosition.x);
        accuracyPoint.y = lastDots.Average(item => item.gameObject.transform.localPosition.y);
        print("AccuPoint : " + accuracyPoint);

        // upL.x = lastDots.Min(item => item.gameObject.transform.localPosition.x);
        // upL.y = lastDots.Max(item => item.gameObject.transform.localPosition.y);

        // upR.x = lastDots.Max(item => item.gameObject.transform.localPosition.x);
        // upR.y = lastDots.Max(item => item.gameObject.transform.localPosition.y);

        // downL.x = lastDots.Min(item => item.gameObject.transform.localPosition.x);
        // downL.y = lastDots.Min(item => item.gameObject.transform.localPosition.y);

        // downR.x = lastDots.Max(item => item.gameObject.transform.localPosition.x);
        // downR.y = lastDots.Min(item => item.gameObject.transform.localPosition.y);

        updateDispersionZone();
    }

    private void updateDispersionZone()
    {
        xScale = -accuracyPoint.x;
        yScale = lastDots.Max(item => item.gameObject.transform.localPosition.y) - accuracyPoint.y;

        if (accuracyPoint.x < 0)
            midX = accuracyPoint.x * -1;
        else
            midX = accuracyPoint.x;
        if (accuracyPoint.y < 0)
            midY = accuracyPoint.y * -1;
        else
            midY = accuracyPoint.y;

        if (lastDots.Max(item => item.gameObject.transform.localPosition.x) >
        (lastDots.Min(item => item.gameObject.transform.localPosition.x) * -1))
        {
            xScale = lastDots.Max(item => item.gameObject.transform.localPosition.x) - midX;
        }
        else
        {
            xScale = (lastDots.Min(item => item.gameObject.transform.localPosition.x) * -1) - midX;
        }

        if (lastDots.Max(item => item.gameObject.transform.localPosition.y) >
        (lastDots.Min(item => item.gameObject.transform.localPosition.y) * -1))
        {
            yScale = lastDots.Max(item => item.gameObject.transform.localPosition.y) - midY;
        }
        else
        {
            yScale = (lastDots.Min(item => item.gameObject.transform.localPosition.y) * -1) - midY;
        }

        print("Dispersion : " + xScale + " ;; " + yScale);
        print("Disp pos : " + accuracyPoint);
        gameObject.transform.localScale = new Vector3(xScale, yScale, 0.1f);
        gameObject.transform.localPosition = accuracyPoint;
    }
}
