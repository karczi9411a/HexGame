using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
	public GameObject pocisk;
	public GameObject pozycja_wyrzutu;

	public float czestotliwosc_czekania;
	public float sila_wyrzutu;

	private IEnumerator WaitAttack(float W_time)
	{
		gameObject.GetComponent<Animator>().SetBool("RangeAttack", false);
		gameObject.GetComponent<Animator>().SetBool("Idle", true);
		yield return new WaitForSeconds(W_time);
		gameObject.GetComponent<Animator>().SetBool("Idle", false);
		gameObject.GetComponent<Animator>().SetBool("RangeAttack", true);
		//Create_bullet();
		yield return new WaitForSeconds(W_time);
		StartCoroutine(WaitAttack(W_time));
	}

	void Start()
	{
		StartCoroutine(WaitAttack(czestotliwosc_czekania));
	}

	public void Create_bullet()
	{
		GameObject obj = Instantiate(pocisk, pozycja_wyrzutu.transform.position, Quaternion.Euler(gameObject.GetComponent<Transform>().eulerAngles.x, gameObject.GetComponent<Transform>().eulerAngles.y, 0), gameObject.transform);
		obj.GetComponent<Rigidbody>().AddForce(transform.up * sila_wyrzutu);
	}
}
