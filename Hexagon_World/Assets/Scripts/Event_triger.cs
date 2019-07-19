using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_triger : MonoBehaviour
{
	GameObject obj;
	LevelManager levelManager;
	EventManager eventManager;

	public string event_t;
	public string quest_mt;
	public bool yes_quest_or_no;
	public string press_t;

	private void Start()
	{
		obj = GameObject.FindGameObjectWithTag("gameplay");
		levelManager = obj.GetComponent<LevelManager>();
		obj = GameObject.FindGameObjectWithTag("game_ui");
		eventManager = obj.GetComponent<EventManager>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "player")
		{
			levelManager.Event_on_Map(event_t, quest_mt, yes_quest_or_no, press_t,eventManager.Event_o);
			GameManager.instance.Change_music_effect(7);
			Destroy(gameObject);
		}
		
	}
}
