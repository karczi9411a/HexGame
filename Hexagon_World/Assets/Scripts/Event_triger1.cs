using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using TMPro;

public class Event_triger1 : MonoBehaviour
{
	GameObject obj;
	public Player player;
	LevelManager levelManager;
	EventManager eventManager;
	public QuestManager questManager;

	public string event_t;
	public string press_t;

	public GameObject prefab_ob_ev_UI;

	public GameObject obj_Event_UI;
	public GameObject obj_Button_UI;
	public GameObject prefab_button;
	

	public Choice_b choice;
	public bool can_delete;
	public bool can_escape;

	int block_duplicate = 0;

	void Start()
	{
		GameObject o = Instantiate(prefab_ob_ev_UI,gameObject.transform);
		obj_Event_UI = o;
		obj_Button_UI = obj_Event_UI.transform.GetChild(1).gameObject;

		gameObject.GetComponent<QuestView>().TextEvent = obj_Event_UI.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
		gameObject.GetComponent<QuestView>().TextPress = obj_Event_UI.transform.GetChild(0).GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
		gameObject.GetComponent<QuestView>().enabled = true;


		obj = GameObject.FindGameObjectWithTag("gameplay");
		levelManager = obj.GetComponent<LevelManager>();
		obj = GameObject.FindGameObjectWithTag("game_ui");
		eventManager = obj.GetComponent<EventManager>();
		obj = GameObject.FindGameObjectWithTag("player");
		player = obj.GetComponent<Player>();

		obj_Event_UI.SetActive(false);
		//questManager = this.gameObject.GetComponent<QuestManager>();
	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "player")
		{
			GameManager.instance.Change_music_effect(7);

			block_duplicate +=1;
			can_escape = false;
			obj_Button_UI.SetActive(true);
			Event2_on_Map(gameObject.GetComponent<Event_triger1>().event_t, gameObject.GetComponent<Event_triger1>().press_t, gameObject.GetComponent<Event_triger1>().obj_Event_UI);

			if (block_duplicate == 1)
			{
				choice.Make();
			}
		}	
	}

	public void Event2_on_Map(string event_t, string press_t, GameObject obj_on)
	{
		questManager.GivePress(press_t);
		obj_on.SetActive(true);
		questManager.GiveEvent_2(event_t);
		GameManager.instance.Pause(0);
	}

	void Update()
	{
		if (CrossPlatformInputManager.GetButtonDown("Action") && can_delete == true)
		{
			GameManager.instance.Pause(1);
			Destroy(this.gameObject);
		}

		if (CrossPlatformInputManager.GetButtonDown("Action") && can_escape == true)
		{
			GameManager.instance.Pause(1);
			obj_Event_UI.SetActive(false);
		}
	}
}
