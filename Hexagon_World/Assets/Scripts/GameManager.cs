using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public bool isPause;
	public int Koniec; //0 - nic, 1 - smierc, 2 - wygrana

	public ReactiveProperty<float> ambientMusic = new ReactiveProperty<float>(100);
	public ReactiveProperty<float> effectMusic = new ReactiveProperty<float>(100);

	public AudioSource ambient_m_obj;
	public AudioSource effect_m_obj;

	public AudioClip[] ambientMusic_list;
	public AudioClip[] effectMusic_list;

	public void Pause(int pause)
	{
		Time.timeScale = pause;
		if (pause == 1) isPause = true; else isPause = false;
	}

	void Awake()
	{
		MakeSingleton();
	}

	private void MakeSingleton()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	public void LoadScene(string scene)
	{
		SceneManager.LoadScene(scene);
	}

	public void ApllicationExit()
	{
		Application.Quit();
	}

	void Update()
	{
		ambient_m_obj.volume = (ambientMusic.Value/100f);
		effect_m_obj.volume = (effectMusic.Value / 100f);

		if (SceneManager.GetActiveScene().name == "Menu")
		{
			Change_music_ambient(0);
			if(GameManager.instance.isPause == false) GameManager.instance.Pause(1);
		}
		if (SceneManager.GetActiveScene().name == "Game") Change_music_ambient(1);
		if (SceneManager.GetActiveScene().name == "Game1") Change_music_ambient(2);
		if (SceneManager.GetActiveScene().name == "Game12") Change_music_ambient(3);

		//Debug.Log(ambientMusic.Value);
		//Debug.Log(effectMusic.Value);
		if (SceneManager.GetActiveScene().name != "Menu")
		{
			//Change_music_ambient(1); //muzyka 

			if (Input.GetKeyUp(KeyCode.Escape))
			{
				LoadScene("Menu");
				//GameManager.instance.Pause(1);
			//	Change_music_ambient(0); //muzyka 
			}
		}
	}

	public void Change_music_ambient(int which_clip)
	{
		if (ambient_m_obj.clip != ambientMusic_list[which_clip])
		{
		//	Debug.Log(ambientMusic_list[which_clip].name);

			ambient_m_obj.clip = ambientMusic_list[which_clip];
			ambient_m_obj.Play();
		}
	}

	public void Change_music_effect(int which_clip)
	{
		//if (effect_m_obj.clip != effectMusic_list[which_clip])
		//{
		//	Debug.Log(effectMusic_list[which_clip].name);

			effect_m_obj.clip = effectMusic_list[which_clip];
			effect_m_obj.Play();
		//}
	}

}
