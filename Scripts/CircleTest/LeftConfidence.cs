﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LeftConfidence : MonoBehaviour
{
	//Update current confidence of the left eye text    
    void Update()
    {
        this.gameObject.GetComponent<Text>().text = "Left confidence\n" + (PupilLabData.confidence1 * 100) + "%";
    }
}
