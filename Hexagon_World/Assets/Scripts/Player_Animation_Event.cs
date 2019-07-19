using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation_Event : MonoBehaviour
{
	public Player player;
	public GameObject hand_o;
	//public BoxCollider collider_hand;
	public MeshCollider sword_effect;
	public SpriteRenderer sprite;
	public bool hit;

	/*
	private IEnumerator Wait(float W_time)
	{
		yield return new WaitForSeconds(W_time);
		hit = false;
		//collider_hand.enabled = false;
		sword_effect.enabled = false;
		sprite.enabled = false;
	//	hand_o.transform.GetChild(0).gameObject.SetActive(false);
	}
	*/

	void Start()
	{
		player.weapon_hand.SetActive(false);
		sword_effect = hand_o.GetComponent<MeshCollider>();
		sprite = hand_o.GetComponent<SpriteRenderer>();
		sword_effect.enabled = false;
		sprite.enabled = false;
	}

	public void Attack_event()
	{
		hit = true;
		sword_effect.enabled = true;
		sprite.enabled = true;
		GameManager.instance.Change_music_effect(4);
	}

	/*
	private IEnumerator Wait2(float W_time)
	{
		yield return new WaitForSeconds(W_time);
		player.weapon_belt.SetActive(true);
	}
	*/

	public void Off_sword()
	{
		sword_effect.enabled = false;
		player.weapon_hand.SetActive(false);
		sprite.enabled = false;
		hit = false;
		player.Marker_trig.SetActive(false);
	}

	public void Get_sword()
	{
		player.weapon_belt.SetActive(true);
		player.weapon_hand.SetActive(false);
	}
}
