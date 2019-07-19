using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrthoScaling : MonoBehaviour
{

	//float horizontalResolution = ;

	private void Update()
	{
		Camera.main.orthographicSize = 14 - (Screen.width / (Screen.width/Screen.height) / 180); //dziala to dobrze
		//size dla 1366x768 = 6
		//size dla 800x600 = 4 a powinno byc 8
	}
	
}
