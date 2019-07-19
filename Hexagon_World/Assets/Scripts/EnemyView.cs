using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;

public class EnemyView : MonoBehaviour
{
	//public TextMeshProUGUI healthText;
	public TextMeshPro pro;
	Enemy enemy;

	void Start()
	{
		enemy = GetComponent<Enemy>();
		//pro.SetText(enemy.Health.Value.ToString());
		enemy.Health.SubscribeToTextM(pro);
	}

}
