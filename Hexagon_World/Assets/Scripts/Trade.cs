using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Trade : MonoBehaviour
{
	GameObject obj;

	public bool turn_on;
	Player player;
	public string[] text_array; //tablica nazw przyciskow co za co
	public string[] press_back_dialog;
	public int[] which_one; //ktory surowiec
	public int[] want_profit; //produkt do kupienia
	public int[] sell_profit; //produkt do sprzedania

	public GameObject prefab_button; //gotowy przycisk
	public GameObject in_button; // przyciski w przycisku
	public QuestManager questManager; //do aktualizacji

	public void Make()
	{
		for (int i = 0; i < text_array.Length; i++)
		{
			prefab_button.name = "Q_button_trade" + i;

			prefab_button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text_array[i]; //nazwa przycisku

			///
			prefab_button.GetComponent<ClickTrade>().trade = gameObject.GetComponent<Trade>();
			prefab_button.GetComponent<ClickTrade>().player = player;
			prefab_button.GetComponent<ClickTrade>().which_one = which_one[i];
			prefab_button.GetComponent<ClickTrade>().want_profit = want_profit[i];
			prefab_button.GetComponent<ClickTrade>().sell_profit = sell_profit[i];
			prefab_button.GetComponent<ClickTrade>().press_back_dialog = press_back_dialog[i];
			///
			Instantiate(prefab_button,
			new Vector2(prefab_button.transform.position.x+ Screen.width/1.88f, (prefab_button.transform.position.y + (Screen.height/2.48f - (Screen.height / 9.6f * i)))), //245 przy x, 725 przy central
			Quaternion.Euler(0, 0, 0),
			in_button.transform);

			// tu nie
		}
	}

	void Start()
	{
		obj = GameObject.FindGameObjectWithTag("player");
		player = obj.GetComponent<Player>();

		if (turn_on == true)
		{
			Make();
		}
	}

}
