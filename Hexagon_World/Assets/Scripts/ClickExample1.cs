using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class ClickExample1 : MonoBehaviour
{
	public int profit;
	public int which_one;
	public Event_triger1 event_Triger1;
	public Player player;

	void Start()
	{
		player = event_Triger1.player;
		AddList(which_one, profit);
	}

	public void AddList(int a,int b)
	{
		gameObject.GetComponent<Button>().onClick.AddListener(() => { TaskOnClick(a,b); });
	}

	public void TaskOnClick(int which_one, int profit)
	{
		GameManager.instance.Change_music_effect(9);
		if (which_one == 1)
		{
			player.GiveLife(profit);
			event_Triger1.can_delete = true;
			Destroy(event_Triger1.obj_Button_UI);
		}
		else if (which_one == 2)
		{
			if (profit > 0)
			{
				player.GiveFood(profit);

				event_Triger1.can_delete = true;
				Destroy(event_Triger1.obj_Button_UI);
			}
			else if (profit < 0)
			{
				if (player.Food.Value >= profit * -1)
				{
					player.GiveFood(profit);

					event_Triger1.can_delete = true;
					Destroy(event_Triger1.obj_Button_UI);
				}
				else
				{
					event_Triger1.questManager.GiveEvent_2("Brak jedzenia");
					event_Triger1.questManager.GivePress("Wybierz inną opcję...");
				}
			}
		}
		else if (which_one == 3)
		{
			if (profit > 0)
			{
				player.GiveGold(profit);

				event_Triger1.can_delete = true;
				Destroy(event_Triger1.obj_Button_UI);
			}
			else if (profit < 0)
			{
				//Debug.Log("Brak złota +1" + profit*-1);
				if (player.Gold.Value >= profit * -1)
				{
					player.GiveGold(profit);

					event_Triger1.can_delete = true;
					Destroy(event_Triger1.obj_Button_UI);
				}
				else if (player.Gold.Value < profit * -1)
				{
					//Debug.Log("Brak złota +2");
					event_Triger1.questManager.GiveEvent_2("Brak złota");
					event_Triger1.questManager.GivePress("Wybierz inną opcję...");
				}
			}
		}
		else if (which_one == 4)
		{
			//Ucieczka
			player.transform.position = event_Triger1.choice.point_exit.transform.position;
			event_Triger1.obj_Button_UI.SetActive(false);
			event_Triger1.can_escape = true;
		}
		else if (which_one == 5)
		{
			//Zapis i nastepna mapa
			if (profit > 0)
			{
				player.GiveGold(profit);

				event_Triger1.can_delete = true;
				Destroy(event_Triger1.obj_Button_UI);

			}
			else if (profit < 0)
			{
				//Debug.Log("Brak złota +1" + profit*-1);
				if (player.Gold.Value >= profit * -1)
				{
					player.GiveGold(profit);

					event_Triger1.can_delete = true;
					Destroy(event_Triger1.obj_Button_UI);

					//zapis i kolejna mapa jesli minus zloto
					player.save.SaveFile(player.Health, player.Food, player.Gold, player.Quest, player.Steps, player.level_name);
					player.save.LoadFile();
				}
				else if (player.Gold.Value < profit * -1)
				{
					//Debug.Log("Brak złota +2");
					event_Triger1.questManager.GiveEvent_2("Brak złota");
					event_Triger1.questManager.GivePress("Wybierz inną opcję...");
				}
			}
		}
		else if (which_one == 6)
		{
			//Ucieczka ze zniszczeniem
			player.transform.position = event_Triger1.choice.point_exit.transform.position;
			event_Triger1.obj_Button_UI.SetActive(false);
			event_Triger1.can_escape = true;
			event_Triger1.can_delete = true;
			Destroy(event_Triger1.obj_Button_UI);
		}
		else if (which_one == 7)
		{
			if (profit < 0)
			{
				if (player.Gold.Value >= profit * -1)
				{
					player.GiveGold(profit);

					event_Triger1.can_delete = true;
					Destroy(event_Triger1.obj_Button_UI);

					GameManager.instance.LoadScene("Menu");
					GameManager.instance.Koniec = 2;
				}
				else if (player.Gold.Value < profit * -1)
				{
					event_Triger1.questManager.GiveEvent_2("Brak złota");
					event_Triger1.questManager.GivePress("Wybierz inną opcję...");
				}
			}
		}
	}
}