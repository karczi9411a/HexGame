using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogs : MonoBehaviour
{
	GameObject obj;
	EventManager eventManager;

	public bool talk_juz;

	public string quest_txt;
	public string press_txt;

	public string[] text_sq_array;
	public string[] text_q_array;
	public string[] text_p_array;
	public int[] which_buttons;

	public GameObject Quest_UI;
	public GameObject Buttons_UI;
	public GameObject Quest_B_prefab;
	public QuestManager QuestManager;
	public GameObject in_button_trade;

	void Start()
	{
		obj = GameObject.FindGameObjectWithTag("game_ui");
		eventManager = obj.GetComponent<EventManager>();

		Quest_UI.SetActive(false);

		if (gameObject.tag == "npc")
		{
			for (int i = 0; i < text_sq_array.Length; i++)
			{
				Quest_B_prefab.name = "Q_button" + i;

				Quest_B_prefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text_sq_array[i];

				Quest_B_prefab.GetComponent<ClickExample>().questManager = QuestManager;
				Quest_B_prefab.GetComponent<ClickExample>().text1 = text_q_array[i];
				Quest_B_prefab.GetComponent<ClickExample>().text2 = text_p_array[i];
				Quest_B_prefab.GetComponent<ClickOpen>().open_object = in_button_trade;
				Quest_B_prefab.GetComponent<ClickOpen>().which_one_button = which_buttons[i];

				Instantiate(Quest_B_prefab,
				new Vector2(Quest_B_prefab.transform.position.x + Screen.width/5.5f, (Quest_B_prefab.transform.position.y + (Screen.height/2.2f - (Screen.height/9.6f * i)))), //245, -440 (1366/5.5f) i (768) (350,80) , dla lewego dolnego
				Quaternion.Euler(0, 0, 0),
				Buttons_UI.transform);
			}
		}
	}
}
