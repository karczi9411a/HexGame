using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Choice_b : MonoBehaviour
{
	public string[] text_se_array;
	public string[] text_e_array;
	public string[] text_p_array;

	public int[] profits_q;
	public int[] which_one;

	public Event_triger1 event_Triger1;
	public GameObject point_exit;
	Player player;

	private void Start()
	{
		//event_Triger1 = this.gameObject.GetComponent<Event_triger1>();
		point_exit.SetActive(false);
		player = event_Triger1.player;
	}

	public void Make()
	{
		for (int i = 0; i < text_se_array.Length; i++)
		{
			event_Triger1.prefab_button.name = "Q_button_e" + i;

			event_Triger1.prefab_button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text_se_array[i];

			event_Triger1.prefab_button.GetComponent<ClickExample>().questManager = event_Triger1.questManager;
			event_Triger1.prefab_button.GetComponent<ClickExample>().text1 = text_e_array[i];
			event_Triger1.prefab_button.GetComponent<ClickExample>().text2 = text_p_array[i];
			
			event_Triger1.prefab_button.GetComponent<ClickExample1>().event_Triger1 = event_Triger1;
			event_Triger1.prefab_button.GetComponent<ClickExample1>().which_one = which_one[i];
			event_Triger1.prefab_button.GetComponent<ClickExample1>().profit = profits_q[i];
			
			Instantiate(event_Triger1.prefab_button,
			new Vector2(event_Triger1.prefab_button.transform.position.x + Screen.width/5.5f, (event_Triger1.prefab_button.transform.position.y + (Screen.height/2.2f - (Screen.height / 9.6f * i)))), //245
			Quaternion.Euler(0, 0, 0),
			event_Triger1.obj_Button_UI.transform);
		}
	}


}
