using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : MonoBehaviour
{
	public Player player;
	GameObject player_o;
	public Enemy enemy;
	public Move_enemy move_;

	public void Start()
	{
		player_o = GameObject.FindGameObjectWithTag("player");
		player = player_o.GetComponent<Player>();
		move_ = player_o.GetComponent<Move_enemy>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "reka")
		{
			float rot = player.GetComponent<Transform>().rotation.eulerAngles.y;
			Quaternion rotation = Quaternion.Euler(0, rot, 0);
			Vector3 direction = rotation * Vector3.forward;
			StartCoroutine(enemy.Waitscript(0.2f, direction));
			
			enemy.rigidbody_o.AddForce(direction * enemy.strength_on_enemy);

			enemy.GiveLife(-player.Damage.Value);
		}
	}
}
