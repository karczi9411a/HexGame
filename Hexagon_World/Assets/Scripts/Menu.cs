using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public Slider slider_a;
	public Slider slider_b;

	public GameObject Menu_UI;
	public GameObject Option_UI;
	public GameObject End_UI;

	public Text tekst_koncowy;

	//public Texture[] lista_zdjec_pomoc; 
	public List<Sprite> lista_zdjec_pomoc = new List<Sprite>();
	public GameObject obiekt_gdzie_zmieniac;
	public int licznik = 0;

	public void Enter_UI(GameObject a)
	{
		a.SetActive(true);
	}

	public void Exit_UI(GameObject a)
	{
		a.SetActive(false);
	}

	public void LoadScene(string scene)
	{
		SceneManager.LoadScene(scene);
	}

	public void ApllicationExit()
	{
		Application.Quit();
	}

	public void EndFunction(string text)
	{
		Exit_UI(Menu_UI);
		Exit_UI(Option_UI);
		Enter_UI(End_UI);
		tekst_koncowy.text = text;
		GameManager.instance.Koniec = 0;
	}

	private void Update()
	{
		slider_a.value = GameManager.instance.ambientMusic.Value;
		slider_b.value = GameManager.instance.effectMusic.Value;

		if (GameManager.instance.Koniec == 0)
		{
			//nic
		}
		else if (GameManager.instance.Koniec == 1)
		{
			EndFunction("Niestety, umarłeś :( Następnym razem się uda.");
		}
		else if (GameManager.instance.Koniec == 2)
		{
			EndFunction("Gratulacje :) Dziękuję za grę, Karol Żurek.");
		}
	}

	public void Slajdy(int licznik)
	{
		obiekt_gdzie_zmieniac.GetComponent<Image>().sprite = lista_zdjec_pomoc[licznik];
	}

	public void Change_slajd(bool lewa_prawa)
	{
		if (lewa_prawa == false) //lewa
		{
			licznik--;
			if (licznik < 0)
			{
				licznik = lista_zdjec_pomoc.Count-1;
			}
			Slajdy(licznik);
		}
		else if (lewa_prawa == true) //prawa
		{
			licznik++;
			if (licznik > lista_zdjec_pomoc.Count-1)
			{
				licznik = 0;
			}
			Slajdy(licznik);
		}
	}

	public void Slider_control_left(Slider slider)
	{
		GameManager.instance.ambientMusic.Value = slider.value;
	}

	public void Slider_control_right(Slider slider)
	{
		GameManager.instance.effectMusic.Value = slider.value;
	}

	public void Change_music_effect(int which_clip)
	{
		//if (effect_m_obj.clip != effectMusic_list[which_clip])
		//{
		//	Debug.Log(effectMusic_list[which_clip].name);

		GameManager.instance.effect_m_obj.clip = GameManager.instance.effectMusic_list[which_clip];
		GameManager.instance.effect_m_obj.Play();
		//}
	}

}
