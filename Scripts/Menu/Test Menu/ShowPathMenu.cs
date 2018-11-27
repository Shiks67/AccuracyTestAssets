using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPathMenu : MonoBehaviour {

	public void DisplayPathMenu()
	{
		gameObject.SetActive(!gameObject.activeSelf);
	}
}
