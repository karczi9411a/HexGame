using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;
using System;

public class MusicView : MonoBehaviour
{
	public TextMeshProUGUI ambientMusic;
	public TextMeshProUGUI effectMusic;

	void Start()
	{
		GameManager.instance.ambientMusic.SubscribeToText(ambientMusic);
		GameManager.instance.effectMusic.SubscribeToText(effectMusic);
	}

}
