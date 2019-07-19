using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;

public class EventView : MonoBehaviour
{
	public TextMeshProUGUI TextEvent;
	public TextMeshProUGUI TextPress;
	public EventManager eventmanager;

	
	void Start()
    {
		eventmanager.Event_txt.SubscribeToText(TextEvent);
		eventmanager.Event_press.SubscribeToText(TextPress);
	}
}
