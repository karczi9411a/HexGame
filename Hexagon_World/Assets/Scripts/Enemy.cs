using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;

public class Enemy : MonoBehaviour
{
	public ReactiveProperty<int> Health = new ReactiveProperty<int>();
	public ReactiveProperty<int> Damage = new ReactiveProperty<int>();
	Player player;
	EnemyView enemyView;
	public int Health_M;
	public int Damage_M;
	public int Gold_to_player;
	public Rigidbody rigidbody_o;
	public float strength_on_enemy;
	public float stength_on_player;
	GameObject player_o;


	private IEnumerator WaitChangeColor(float W_time, TextMeshPro text, Color color_v)
	{
		//print("Zmiana na czerwony");
		text.color = color_v;
		yield return new WaitForSeconds(W_time);
		text.color = new Color(1, 1, 1);
		//print("Zmiana na biały");
		//print("czas " + Time.time);
	}

	public void Color_change(int var, float W_time, TextMeshPro text)
	{
		if (var > 0)
		{
			StartCoroutine(WaitChangeColor(W_time, text, new Color(0, 1, 0)));
		}
		else if (var < 0)
		{
			StartCoroutine(WaitChangeColor(W_time, text, new Color(1, 0, 0)));
		}
	}

	private void Start()
	{
		player_o = GameObject.FindGameObjectWithTag("player");
		player = player_o.GetComponent<Player>();
		enemyView = this.gameObject.GetComponent<EnemyView>();

		rigidbody_o = GetComponent<Rigidbody>();
		GiveLife(Health_M);
		GiveDamage(Damage_M);
	}

	public IEnumerator Waitscript(float W_time, Vector3 direction)
	{
		gameObject.GetComponent<AudioSource>().Play();
		gameObject.GetComponent<Animator>().enabled = false;
		rigidbody_o.AddForce(direction * strength_on_enemy);
		yield return new WaitForSeconds(W_time);
		gameObject.GetComponent<Animator>().enabled = true;
	}
	/*
	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log(collision.gameObject.name);
	}
	*/
	/*
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "reka")
		{
			// rigidbody2D.AddForce(new Vector2(1,Mathf.Cos(angle))*force);

			float rot = player.GetComponent<Transform>().rotation.eulerAngles.y;
			Quaternion rotation = Quaternion.Euler(0, rot, 0);
			Vector3 direction = rotation * Vector3.forward;
			StartCoroutine(Waitscript(0.2f, direction));//rigidbody_o.AddForce(direction * strength_on_enemy); 
			GiveLife(-player.Damage.Value);
		}
	}
	*/

	public void GiveLife(int life)
	{
		Health.Value += life;
		Color_change(life, 0.5f, enemyView.pro);
	}

	public void GiveDamage(int damage)
	{
		Damage.Value += damage;
	}

	private void Update()
	{
		gameObject.GetComponent<AudioSource>().volume = (GameManager.instance.effectMusic.Value / 100f);

		if (enemyView.pro.transform.rotation.x != 30)
		{
			enemyView.pro.transform.rotation = Quaternion.Euler(30, 0, 0);
		}

		if (Health.Value <= 0)
		{
			player.GiveGold(Gold_to_player);
			Destroy(gameObject);
		}

		if (gameObject.transform.position.y <= -30f) //out of hexagons
		{
			Health.Value = 0;
		}
	}
}
