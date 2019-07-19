using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	GameData gameData;
	GameObject obj;
	public Save save;
	public string level_count;
	public string level_name;
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
	public GameObject shield_obj;
	public ThirdPersonCharacter thirdPerson;

	public IEnumerator ShieldOnOf(float W_time, GameObject shield_obj)
	{
		gameObject.GetComponent<AudioSource>().Play();
		shield = true;
		shield_obj.SetActive(true);
		yield return new WaitForSeconds(W_time);
		shield = false;
		shield_obj.SetActive(false);
	}


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
		GameManager.instance.Change_music_effect(1);
		Health.Value += life;
		Color_change(life, time_sec, PlayerView.healthText);
	}
	public void GiveFood(int food)
	{
		if (food > 0) GameManager.instance.Change_music_effect(2);
		Food.Value += food;
		Color_change(food, time_sec, PlayerView.foodText);
	}
	public void GiveGold(int gold)
	{
		if(gold > 0) GameManager.instance.Change_music_effect(0);
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

	private void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject.tag == "enemy" && shield == false)
		{
			float rot = collision.gameObject.GetComponent<Transform>().rotation.eulerAngles.y;
			Quaternion rotation = Quaternion.Euler(0, rot, 0);
			Vector3 direction = rotation * Vector3.forward;
			//gameObject.GetComponent<Rigidbody>().AddForce(transform.up * 250);

			if (animator.GetBool("OnGround") == true)
			{
				StartCoroutine(Waitscript(0.2f, direction));
				//gameObject.GetComponent<Rigidbody>().AddForce(direction * enemy.stength_on_player);
			}
			else gameObject.GetComponent<Rigidbody>().AddForce(direction * enemy.stength_on_player);
			GiveLife(-enemy.Damage.Value);

			StartCoroutine(ShieldOnOf(3f, shield_obj));
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "enemy" && shield == false)
		{
			float rot = collision.gameObject.GetComponent<Transform>().rotation.eulerAngles.y;
			Quaternion rotation = Quaternion.Euler(0, rot, 0);
			Vector3 direction = rotation * Vector3.forward;
		
			if (animator.GetBool("OnGround") == true)
			{
				StartCoroutine(Waitscript(0.2f, direction));
			}
			else gameObject.GetComponent<Rigidbody>().AddForce(direction * enemy.stength_on_player);

			GiveLife(-enemy.Damage.Value);

			StartCoroutine(ShieldOnOf(3f, shield_obj));
		}
		if (collision.gameObject.tag == "pocisk" && shield == false)
		{
			StartCoroutine(ShieldOnOf(3f, shield_obj));
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "food" || other.tag == "npc" || other.tag == "treasure" || other.tag == "gem")
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
		else if (other.tag == "gem")
		{
			if (CrossPlatformInputManager.GetButton("Action"))
			{
				GiveGold(other.GetComponent<Profits>().profit);
				Destroy(other.gameObject);
				Marker_trig.SetActive(false);
			}
		}
		else if (other.tag == "npc")
		{
			if (CrossPlatformInputManager.GetButton("Action") && eventManager.Event_o.activeSelf == false && Marker_trig.activeSelf == true)
			{
				GameManager.instance.Change_music_effect(8);
				levelManager.Quest_on_Map(other.gameObject,
					other.GetComponent<Dialogs>().quest_txt,
					other.GetComponent<Dialogs>().press_txt);
				other.GetComponent<Dialogs>().talk_juz = true;
				other.GetComponent<Trade>().in_button.SetActive(false);
				moge_atakowac = true;
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "food" || other.tag == "npc" || other.tag == "treasure" || other.tag == "gem")
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
		GameManager.instance.Pause(1);
		animator = gameObject.GetComponent<Animator>();
		Marker_trig = gameObject.transform.GetChild(0).gameObject;

		//Zabezpieczenie przed zapisem gry
		obj = GameObject.FindGameObjectWithTag("gameplay");
		PlayerView = obj.GetComponent<PlayerView>();
		obj = GameObject.FindGameObjectWithTag("gameplay");
		levelManager = obj.GetComponent<LevelManager>();
		obj = GameObject.FindGameObjectWithTag("game_ui");
		eventManager = obj.GetComponent<EventManager>();

		obj = GameObject.FindGameObjectWithTag("enemy");
		enemy = obj.GetComponent<Enemy>();
		//if (obj == null) enemy = null; else enemy = obj.GetComponent<Enemy>();
		obj = GameObject.FindGameObjectWithTag("npc");
		//if (obj == null) enemy = null; else  dialogs = obj.GetComponent<Dialogs>();
		dialogs = obj.GetComponent<Dialogs>();

		obj = GameObject.FindGameObjectWithTag("MainCamera");
		obj.GetComponent<CompleteCameraController>().player = gameObject.transform.GetChild(3).gameObject;

		obj = GameObject.FindGameObjectWithTag("GameManager");
		save = obj.GetComponent<Save>();

		level_name = SceneManager.GetActiveScene().name+level_count;

		if (SceneManager.GetActiveScene().name != "Game")
		{
			save.LoadDatatoData(Health, Food, Gold, Quest, Steps);
		}

		//Debug.Log(level_name);

		//	obj = GameObject.FindGameObjectWithTag("hexagon");
		//	obj.GetComponent<Hexagon>().player = gameObject.GetComponent<Player>();
	}

	void Update()
	{
		gameObject.GetComponent<AudioSource>().volume = (GameManager.instance.effectMusic.Value / 100f);

		if (thirdPerson.skoczylem == true)
		{
			GameManager.instance.Change_music_effect(5);
			thirdPerson.skoczylem = false;

		}

		if (CrossPlatformInputManager.GetButtonDown("Fire1") && eventManager.Event_o.active == false && moge_atakowac == false && animator.GetBool("OnGround") == true  && animator.GetBool("Crouch") == false  && GameManager.instance.isPause == true)
		{
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
		if (Health.Value <= 0)
		{
			Health.Value = 0;
			//Game over
			GameManager.instance.LoadScene("Menu");
			GameManager.instance.Koniec = 1;
		}
		else if (Health.Value > 100) Health.Value = 100;

		
		if (Gold.Value < 0)
		{
			Gold.Value = 0;
			//gold empty
		}
		
		if (gameObject.transform.position.y <= -30f) 
		{
			StartCoroutine(ShieldOnOf(3f, shield_obj));
			GiveLife(-10);
			gameObject.transform.position =
				Last_stay_hexagon.transform.position + new Vector3(0, 1, 0);
		}
	}



}