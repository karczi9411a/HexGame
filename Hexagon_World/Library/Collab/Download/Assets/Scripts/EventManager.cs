using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityStandardAssets.CrossPlatformInput;

public class EventManager : MonoBehaviour
{
	public LevelManager levelManager;
	public GameObject Event_o;
	public Player player;
	public bool get_from_ev_triger;
	public GameObject Choice_buttons;

	public ReactiveProperty<string> Event_txt = new ReactiveProperty<string>("");
	public ReactiveProperty<string> Event_press = new ReactiveProperty<string>("");
	//public List<string> quests_List = new List<string>();
	/*
	public void GiveEvent(string txt)
	{
		TextAsset loading_event = (TextAsset)Resources.Load(txt);
		Event_txt.Value = loading_event.text;
	}
	*/
	public void GiveEvent_2(string txt)
	{
		Event_txt.Value = txt;
	}

	public void GivePress(string txt)
	{
		Event_press.Value = txt;
	}

	void Start()
	{
		Event_o.SetActive(false);
	}

	void Update()
	{
		//jesli event
		if (CrossPlatformInputManager.GetButtonDown("Action") && Event_o.active == true && player.Marker_trig.active == false && get_from_ev_triger == false)//&& GameManager.instance.isPause == false)
		{
			Event_o.SetActive(false);
			//usun colidera
			GameManager.instance.Pause(1);
		}
	}
}
