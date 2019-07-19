using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClickTrade : MonoBehaviour
{
	public Trade trade;
	public Player player;
	public int which_one; //ktory surowiec
	public int want_profit; //produkt do kupienia
	public int sell_profit; //produkt do sprzedania
	public string press_back_dialog; //tekst powrotu

	void Start()
	{
		AddList(which_one, want_profit,sell_profit);
	}

	public void AddList(int a, int b, int c)
	{
		gameObject.GetComponent<Button>().onClick.AddListener(() => { TaskOnClick(a, b, c); });
	}

	public void TaskOnClick(int which_one, int want_profit, int sell_profit)
	{
		GameManager.instance.Change_music_effect(9);
		//Debug.Log("Dziala");
		if (which_one == 1) //jedzenie za zycie 5 -> 1
		{
			//Debug.Log("Wybralem pierwszy");
			if (player.Food.Value >= sell_profit)
			{
				player.GiveFood(sell_profit * -1); //-1
				player.GiveLife(want_profit); //5
				trade.questManager.GivePress(press_back_dialog);
			}
			else
			{
				trade.questManager.GivePress("Brak jedzenia...");
			}
		}
		else if (which_one == 2) //złoto za jedzenie 1 -> 5
		{
			//Debug.Log("Wybralem drugi");
			if (player.Gold.Value >= sell_profit)
			{
				player.GiveGold(sell_profit * -1); //-1
				player.GiveFood(want_profit); //5
				trade.questManager.GivePress(press_back_dialog);
			}
			else
			{
				trade.questManager.GivePress("Brak złota...");
			}
		}
		else if (which_one == 3) //jedzenie za złoto 7 -> 1
		{
			//Debug.Log("Wybralem trzeci");
			if (player.Food.Value >= sell_profit)
			{
				player.GiveFood(sell_profit * -1); //-7
				player.GiveGold(want_profit); //1
				trade.questManager.GivePress(press_back_dialog);
			}
			else
			{
				trade.questManager.GivePress("Brak jedzenia...");
			}
		}
	}
}

