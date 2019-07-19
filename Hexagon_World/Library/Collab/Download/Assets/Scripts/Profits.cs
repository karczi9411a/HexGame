using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Profits : MonoBehaviour
{
	public EventManager eventManager;

	public int profit;
	public bool talk_juz;

	public string quest_txt;
	public string press_txt;

	public string[] text_sq_array;
	public string[] text_q_array;
	public string[] text_p_array;

	public void TaskOnClick(string a, string b)
	{
		Debug.Log("dziala");
		eventManager.GiveEvent_2(a);
		eventManager.GivePress(b);
	}

	void Start()
	{
		if (gameObject.tag == "npc")
		{
			for (int i = 0; i < text_sq_array.Length; i++)
			{
				eventManager.Quest_B_prefab.name = "Q_button" + i;

				eventManager.Quest_B_prefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text_sq_array[i];

				eventManager.Quest_B_prefab.GetComponent<ClickExample>().eventManager = eventManager;
				eventManager.Quest_B_prefab.GetComponent<ClickExample>().text1 = text_q_array[i];
				eventManager.Quest_B_prefab.GetComponent<ClickExample>().text2 = text_p_array[i];

				

				Instantiate(eventManager.Quest_B_prefab,
				new Vector2(eventManager.Quest_B_prefab.transform.position.x + 245, (eventManager.Quest_B_prefab.transform.position.y + (350 - (80 * i)))),
				Quaternion.Euler(0, 0, 0),
				eventManager.Quest_buttons.transform);

				

				// tu nie

			}
		}
	}

	void TaskOnClick()
	{
		Debug.Log("You have clicked the button!");
	}
}
