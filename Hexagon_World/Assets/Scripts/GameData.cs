using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

[System.Serializable]
public class GameData
{
	public ReactiveProperty<int> Health;
	public ReactiveProperty<int> Food;
	public ReactiveProperty<int> Gold;
	public ReactiveProperty<string> Quest;
	public ReactiveProperty<int> Steps;
	public string Level = "";

	public GameData(ReactiveProperty<int> health, ReactiveProperty<int> food, ReactiveProperty<int> gold, ReactiveProperty<string> quest, ReactiveProperty<int> steps, string level)
	{
		Health = health;
		Food = food;
		Gold = gold;
		Quest = quest;
		Steps = steps;
		Level = level;
	}

}