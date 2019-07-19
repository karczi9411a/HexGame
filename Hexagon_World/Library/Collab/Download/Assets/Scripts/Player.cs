using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
	//Zmienne reaktywne do dynamiczne zmiany, bez aktualizacji poprzez szereg procedur
	public ReactiveProperty<int> Health = new ReactiveProperty<int>(100);
	public ReactiveProperty<int> Food = new ReactiveProperty<int>(51);
	public ReactiveProperty<int> Gold = new ReactiveProperty<int>(10);
	public ReactiveProperty<string> Quest = new ReactiveProperty<string>("");
	public ReactiveProperty<int> Steps = new ReactiveProperty<int>(0);
	public ReactiveProperty<int> Damage = new ReactiveProperty<int>(10);
	public GameObject weapon_belt;
	public GameObject weapon_hand;
	public GameObject Last_stay_hexagon;
	public GameObject Marker_trig;
	public PlayerView PlayerView;
	//public Profits profits;
	//public Dialogs dialogs;
	public LevelManager levelManager;
	public EventManager eventManager;
	public Animator animator;
	public bool shield;
	float time_sec = 1;
	public Enemy enemy;
	public Dialogs dialogs;
	bool moge_atakowac;
	//inumerator, dla przejrzystego intefejsu zmiany podstawowych statystyk gracza
	private IEnumerator WaitChangeColor(float W_time, TextMeshProUGUI text,Color color_v)
	{
		//print("Zmiana na czerwony");
		text.color = color_v;
		yield return new WaitForSeconds(W_time);
		text.color = new Color(1, 1, 1);
		//print("Zmiana na biały");
		//print("czas " + Time.time);
	}

	public void Color_change(int var, float W_time, TextMeshProUGUI text)
	{
		if (var > 0)
		{	
			StartCoroutine(WaitChangeColor(W_time,text,new Color(0,1,0)));
		}
		else if (var < 0)
		{
			StartCoroutine(WaitChangeColor(W_time,text,new Color(1,0,0)));	
		}
	}

	public void GiveQuest(string quest)
	{
		Quest.Value = quest;
	}
	
	public void GiveLife(int life)
	{
		Health.Value += life;
		Color_change(life, time_sec, PlayerView.healthText);
	}

	public void GiveFood(int food)
	{
		Food.Value += food;
		Color_change(food, time_sec, PlayerView.foodText);
	}

	public void GiveGold(int gold)
	{
		Gold.Value += gold;
		Color_change(gold, time_sec, PlayerView.goldText);
	}

	public void GiveSteps(int steps)
	{
		Steps.Value += steps;
		Color_change(steps, time_sec, PlayerView.stepsText);
	}

	private IEnumerator Waitscript(float W_time, Vector3 direction)
	{
		(gameObject.GetComponent("ThirdPersonCharacter") as MonoBehaviour).enabled = false;
		gameObject.GetComponent<Rigidbody>().AddForce(direction * enemy.stength_on_player);
		yield return new WaitForSeconds(W_time);
		(gameObject.GetComponent("ThirdPersonCharacter") as MonoBehaviour).enabled = true;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "enemy" && shield == false)
		{
			float rot = gameObject.GetComponent<Transform>().rotation.eulerAngles.y;
			Quaternion rotation = Quaternion.Euler(0, rot, 0);
			Vector3 direction = rotation * -Vector3.forward;
			//gameObject.GetComponent<Rigidbody>().AddForce(transform.up * 250);

			if (animator.GetBool("OnGround") == true)
			{
				StartCoroutine(Waitscript(0.2f, direction));
				//gameObject.GetComponent<Rigidbody>().AddForce(direction * enemy.stength_on_player);
			}
			else gameObject.GetComponent<Rigidbody>().AddForce(direction * enemy.stength_on_player);
			GiveLife(-enemy.Damage.Value);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "food" || other.tag == "npc" || other.tag == "treasure")
		{
			Marker_trig.SetActive(true);
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "food")
		{
			if (CrossPlatformInputManager.GetButton("Action"))
			{
				GiveFood(other.GetComponent<Profits>().profit);
				Destroy(other.gameObject);
				Marker_trig.SetActive(false);
			}
		}
		else if (other.tag == "treasure")
		{
			if (CrossPlatformInputManager.GetButton("Action"))
			{
				other.transform.GetChild(0).gameObject.SetActive(true);
				other.transform.GetChild(3).gameObject.SetActive(true);
				other.transform.GetChild(2).GetComponent<Animator>().Play("chest_open");
				other.GetComponent<SphereCollider>().enabled = false;
				GiveGold(other.GetComponent<Profits>().profit);
				Marker_trig.SetActive(false);
			}
		}
		else if (other.tag == "npc")
		{
			//jesli nacisne akcje i event jest wylaczony i marker wlaczony wtedy quest, dodaje ze z nim gadalem, jesli bedzie jakis quest u niego
			if (CrossPlatformInputManager.GetButton("Action") && eventManager.Event_o.active == false && Marker_trig.active == true)
			{
				levelManager.Quest_on_Map(other.gameObject,other.GetComponent<Dialogs>().quest_txt, other.GetComponent<Dialogs>().press_txt);
			//	if (other.GetComponent<Dialogs>().talk_juz == false) eventManager.quests_List.Add(other.name);
				other.GetComponent<Dialogs>().talk_juz = true;
				moge_atakowac = true;
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "food" || other.tag == "npc" || other.tag == "treasure")
		{
			Marker_trig.SetActive(false);
		}
		
		if (other.tag == "npc")
		{
			other.GetComponent<Dialogs>().Quest_UI.SetActive(false);
			moge_atakowac = false;
		}
	}

	void Start()
	{
		animator = gameObject.GetComponent<Animator>();
	}

	void Update()
	{
		if (CrossPlatformInputManager.GetButtonDown("Fire1") && eventManager.Event_o.active == false && moge_atakowac == false && animator.GetBool("OnGround") == true && animator.GetBool("Crouch") == false)
		{
			//Debug.Log("Animacja "+ animator.parameters[6].name);
			//animator.SetTrigger("Attack");
			weapon_hand.SetActive(true);
			weapon_belt.SetActive(false);
			animator.Play("Attack1");
		}
		else
		{
				//animator.ResetTrigger("Attack");
		}

		if (Marker_trig.transform.rotation.y != 0)
		{
			Marker_trig.transform.rotation = Quaternion.Euler(0, 0, 0);
		}

		if (Food.Value < 0)
		{
			//Food empty
			Food.Value = 0;
			GiveLife(-1);
		}

		if (Health.Value < 0)
		{
			Health.Value = 0;
			//Game over
		}
		else if (Health.Value > 100) Health.Value = 100;

		if (gameObject.transform.position.y <= -30f) //out of hexagons
		{
			GiveLife(-10);
			gameObject.transform.position = Last_stay_hexagon.transform.position + new Vector3(0, 1, 0);
		}
	}
}