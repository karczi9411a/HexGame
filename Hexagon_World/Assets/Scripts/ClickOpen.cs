using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClickOpen : MonoBehaviour
{
	public GameObject open_object; //obiekt przyciskow
	public int which_one_button; // binarnie aktywnego tradeowania

	void Start()
	{
		gameObject.GetComponent<Button>().onClick.AddListener(() => { On_off(which_one_button, open_object); });
	}

	//wlacz u wylacz trada
	void On_off(int which, GameObject obj)
	{
		GameManager.instance.Change_music_effect(9);

		if (which == 1)
		{
			obj.SetActive(true);
		}
		else if (which == 0)
		{
			obj.SetActive(false);
		}
	}
}
