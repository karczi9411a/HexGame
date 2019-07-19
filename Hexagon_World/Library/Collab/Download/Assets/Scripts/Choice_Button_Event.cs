using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice_Button_Event : MonoBehaviour
{
	public int size_button;
	public Event_triger event_Triger;

    void Start()
    {
		event_Triger = this.gameObject.GetComponent<Event_triger>();
    }
}
