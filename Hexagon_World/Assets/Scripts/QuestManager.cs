using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityStandardAssets.CrossPlatformInput;

public class QuestManager : MonoBehaviour
{
	public ReactiveProperty<string> Event_txt = new ReactiveProperty<string>("");
	public ReactiveProperty<string> Event_press = new ReactiveProperty<string>("");

	public void GiveEvent_2(string txt)
	{
		Event_txt.Value = txt;
	}

	public void GivePress(string txt)
	{
		Event_press.Value = txt;
	}
}
