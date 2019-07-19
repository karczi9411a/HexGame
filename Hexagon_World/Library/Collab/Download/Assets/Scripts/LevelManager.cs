using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public EventManager eventmanager;
	public Map map;
	public Player player;
	public Dialogs dialogs;
	//public Hexagon hexagon;
	/*
	public void Event_on_Map(int player_step,string event_t,string quest_t,bool check,string press_t)
	{
		if (player.Steps.Value == player_step && check == false)
		{
			eventmanager.GivePress(press_t);

			//eventmanager.quests_List.Add(event_t);
			eventmanager.Event_o.SetActive(true);
			eventmanager.GiveEvent(event_t);
			player.GiveQuest(quest_t);
			GameManager.instance.Pause(0);
		}
	}
	*/
	public void Event_on_Map(string event_t, string quest_t, bool quest_y_or_n, string press_t)
	{
			eventmanager.GivePress(press_t);
			//eventmanager.quests_List.Add(event_t);
			eventmanager.Event_o.SetActive(true);
			eventmanager.GiveEvent_2(event_t);
			if(quest_y_or_n == true) player.GiveQuest(quest_t);
			GameManager.instance.Pause(0);
	}

	public void Quest_on_Map(GameObject ob,string quest_txt,string press_txt)
	{
		ob.GetComponent<QuestManager>().GivePress(press_txt);
		ob.GetComponent<QuestManager>().GiveEvent_2(quest_txt);
		ob.GetComponent<Dialogs>().Quest_UI.SetActive(true);
	}
}
