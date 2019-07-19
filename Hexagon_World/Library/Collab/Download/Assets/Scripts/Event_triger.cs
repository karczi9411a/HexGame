using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_triger : MonoBehaviour
{
	public LevelManager levelManager;
	public EventManager eventManager;

	public string event_t;
	public string quest_mt;
	public bool yes_quest_or_no;
	public string press_t;

	public bool choice_button;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "player")
		{
			//Debug.Log(gameObject.GetComponent<Event_triger>().choice_button);
			eventManager.get_from_ev_triger = gameObject.GetComponent<Event_triger>().choice_button;
			eventManager.Choice_buttons.SetActive(eventManager.get_from_ev_triger);

			levelManager.Event_on_Map(event_t, quest_mt, yes_quest_or_no, press_t);
			Destroy(gameObject);
		}
		
	}
}
