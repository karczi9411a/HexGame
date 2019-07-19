using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;

public class QuestView : MonoBehaviour
{
	public TextMeshProUGUI TextEvent;
	public TextMeshProUGUI TextPress;
	public QuestManager questManager;

	
	void Start()
    {
		questManager.Event_txt.SubscribeToText(TextEvent);
		questManager.Event_press.SubscribeToText(TextPress);
	}
}
