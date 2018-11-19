using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class AimCalculator : MonoBehaviour
{
    private Vector3[] lastDots = new Vector3[10];
    private Vector3 accuracyPoint = new Vector3(0, 0, -0.2f);

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

        UpdateAccuracy();
        updateDispersionZone();
    }

    private void UpdateAccuracy()
    {
        gameObject.GetComponent<Image>().enabled = true;

        for (int i = 0; i < 10; i++)
        {
            lastDots[i] = GazeMarker.listDotMark[(GazeMarker.listDotMark.Count - 10) + i];
            print(GazeMarker.listDotMark[(GazeMarker.listDotMark.Count - 10) + i]);
        }

        accuracyPoint.x = lastDots.Average(item => item.x);
        accuracyPoint.y = lastDots.Average(item => item.y);
        accuracyPoint.z = 0;
    }

    private void updateDispersionZone()
    {
        midX = accuracyPoint.x;
        midX = midX > 0 ? midX : midX * -1;

        midY = accuracyPoint.y;
        midY = midY > 0 ? midY : midY * -1;

        var maxX = lastDots.Max(item => item.x);
        maxX = maxX > 0 ? maxX : maxX * -1;

        var minX = lastDots.Min(item => item.x);
        minX = minX > 0 ? minX : minX * -1;

        xScale = maxX > minX ? maxX - midX : minX - midX;

        var maxY = lastDots.Max(item => item.y);
        maxY = maxY > 0 ? maxY : maxY * -1;

        var minY = lastDots.Min(item => item.y);
        minY = minY > 0 ? minY : minY * -1;

        yScale = maxY > minY ? maxY - midY : minY - midY;

        gameObject.transform.localScale = new Vector3(xScale, yScale, 0.1f);
        gameObject.transform.localPosition = accuracyPoint;
    }

    private float Angle(Vector2 vec1, Vector2 vec2)
    {
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, diference) * sign;
    }
}
