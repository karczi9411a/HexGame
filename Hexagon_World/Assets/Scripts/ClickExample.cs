using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClickExample : MonoBehaviour
{
	public string text1;
	public string text2;
	public QuestManager questManager;

	void Start()
	{
		AddList(text1 , text2);
	}

	public void AddList(string a,string b)
	{
		gameObject.GetComponent<Button>().onClick.AddListener(() => { TaskOnClick(a,b); });
	}

	public void TaskOnClick(string a, string b)
	{
		GameManager.instance.Change_music_effect(9);
		questManager.GiveEvent_2(a);
		questManager.GivePress(b);
	}

}