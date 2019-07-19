using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation_object_imersion : MonoBehaviour
{
    void Start()
    {
		//this.gameObject.transform.localEulerAngles = new Vector2 (this.gameObject.GetComponent<Transform>().localEulerAngles.x,Random.Range(0, 359));
		this.gameObject.transform.eulerAngles = new Vector2(this.gameObject.GetComponent<Transform>().eulerAngles.x, Random.Range(0, 359));
	}
}
