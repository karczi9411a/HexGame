using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;

public class PlayerView : MonoBehaviour
{
	public TextMeshProUGUI questText;
	public TextMeshProUGUI healthText;
	public TextMeshProUGUI foodText;
	public TextMeshProUGUI goldText;
	public TextMeshProUGUI stepsText;
	public Player player;

	void Start()
	{
		player.Health.SubscribeToText(healthText);
		player.Food.SubscribeToText(foodText);
		player.Gold.SubscribeToText(goldText);
		player.Quest.SubscribeToText(questText);
		player.Steps.SubscribeToText(stepsText);
	}

}
