using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FovCalibration : MonoBehaviour
{

    public GameObject UpLeft;
    public GameObject UpRight;
    public GameObject DownLeft;
    public GameObject DownRight;
    private RayCaster rCaster;
    private int speed = 5;
    public List<Text> verticalTextList = new List<Text>();
    public List<Text> horizontalTextList = new List<Text>();

    // Use this for initialization
    void Start()
    {
        // rCaster = GameObject.FindGameObjectWithTag("EditorOnly").GetComponent<RayCastF>();
        rCaster = Camera.main.GetComponent<RayCaster>();
        InitTargetScale();

        if (FovStatic.upLeftPos != new Vector3(0, 0, 0))
            ResumeLastCalibration();
    }

    private void InitTargetScale()
    {
        foreach (var text in verticalTextList)
        {
            var goParent = text.gameObject.transform.parent;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
            RandomizeText();
        if (Input.GetKeyUp(KeyCode.UpArrow))
            ExtendTextSize();
        if (Input.GetKeyUp(KeyCode.DownArrow))
            ReduceTextSize();

        RaycastHit hit;
        if (Physics.Raycast(rCaster.ray, out hit))
        {
            var obj = hit.transform.gameObject;

            if (obj == UpLeft)
            {
                hit.transform.localPosition = new Vector3(hit.transform.localPosition.x - speed * Time.deltaTime,
                    hit.transform.localPosition.y + speed * Time.deltaTime, hit.transform.localPosition.z);
            }
            else if (obj == DownLeft)
            {
                hit.transform.localPosition = new Vector3(hit.transform.localPosition.x - speed * Time.deltaTime,
                    hit.transform.localPosition.y - speed * Time.deltaTime, hit.transform.localPosition.z);
            }
            else if (obj == DownRight)
            {
                hit.transform.localPosition = new Vector3(hit.transform.localPosition.x + speed * Time.deltaTime,
                    hit.transform.localPosition.y - speed * Time.deltaTime, hit.transform.localPosition.z);
            }
            else if (obj == UpRight)
            {
                hit.transform.localPosition = new Vector3(hit.transform.localPosition.x + speed * Time.deltaTime,
                    hit.transform.localPosition.y + speed * Time.deltaTime, hit.transform.localPosition.z);
            }
            else if (obj.name == "Up")
            {
                hit.transform.parent.localPosition = new Vector3(hit.transform.parent.localPosition.x,
                    hit.transform.parent.localPosition.y + speed * Time.deltaTime, hit.transform.parent.localPosition.z);
            }
            else if (obj.name == "Left")
            {
                hit.transform.parent.localPosition = new Vector3(hit.transform.parent.localPosition.x - speed * Time.deltaTime,
                    hit.transform.parent.localPosition.y, hit.transform.parent.localPosition.z);
            }
            else if (obj.name == "Right")
            {
                hit.transform.parent.localPosition = new Vector3(hit.transform.parent.localPosition.x + speed * Time.deltaTime,
                    hit.transform.parent.localPosition.y, hit.transform.parent.localPosition.z);
            }
            else if (obj.name == "Down")
            {
                hit.transform.parent.localPosition = new Vector3(hit.transform.parent.localPosition.x,
                    hit.transform.parent.localPosition.y - speed * Time.deltaTime, hit.transform.parent.localPosition.z);
            }
        }
    }

    private void ResumeLastCalibration()
    {
        UpLeft.transform.localPosition = FovStatic.upLeftPos;
        UpRight.transform.localPosition = FovStatic.upRightPos;
        DownLeft.transform.localPosition = FovStatic.downLeftPos;
        DownRight.transform.localPosition = FovStatic.downRightPos;
    }


    public void ApplyToStatics()
    {
        FovStatic.upLeftPos = UpLeft.transform.localPosition;
        FovStatic.upRightPos = UpRight.transform.localPosition;
        FovStatic.downLeftPos = DownLeft.transform.localPosition;
        FovStatic.downRightPos = DownRight.transform.localPosition;
    }

    public void ResetFov()
    {
        FovStatic.upLeftPos = new Vector3(0, 0, 0);
        FovStatic.upRightPos = new Vector3(0, 0, 0);
        FovStatic.downLeftPos = new Vector3(0, 0, 0);
        FovStatic.downRightPos = new Vector3(0, 0, 0);
    }

    private string randomText = "";

    private void RandomizeText()
    {
        foreach (var htext in horizontalTextList)
        {
            for (int i = 0; i < 5; i++)
            {
                randomText += (char)Random.Range(65, 91) + " ";
            }
            htext.text = randomText;
            randomText = "";
        }
        foreach (var vtext in verticalTextList)
        {
            for (int i = 0; i < 5; i++)
            {
                randomText += (char)Random.Range(65, 91) + "\n";
            }
            vtext.text = randomText;
            randomText = "";
        }
    }

    private void ExtendTextSize()
    {
        foreach (var htext in horizontalTextList)
        {
            htext.transform.localScale = new Vector3(htext.transform.localScale.x + 0.1f,
            htext.transform.localScale.y + 0.1f,
            htext.transform.localScale.z + 0.1f);
        }
        foreach (var vtext in verticalTextList)
        {
            vtext.transform.localScale = new Vector3(vtext.transform.localScale.x + 0.1f,
            vtext.transform.localScale.y + 0.1f,
            vtext.transform.localScale.z + 0.1f);
        }
    }

    private void ReduceTextSize()
    {
        foreach (var htext in horizontalTextList)
        {
            if (htext.transform.localScale.x > 0)
                htext.transform.localScale = new Vector3(htext.transform.localScale.x - 0.1f,
            htext.transform.localScale.y - 0.1f,
            htext.transform.localScale.z - 0.1f);
        }
        foreach (var vtext in verticalTextList)
        {
            if (vtext.transform.localScale.x > 0)
                vtext.transform.localScale = new Vector3(vtext.transform.localScale.x - 0.1f,
            vtext.transform.localScale.y - 0.1f,
            vtext.transform.localScale.z - 0.1f);
        }
    }
}
