using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_plant : MonoBehaviour
{
	float czas_zniszczenia = 30f;

	private IEnumerator WaitD(float W_time)
	{
		yield return new WaitForSeconds(W_time);
		Destroy(this.gameObject);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "player" && collision.gameObject.GetComponent<Player>().shield == false)
		{
			collision.gameObject.GetComponent<Player>().GiveLife(-5);
			//StartCoroutine(collision.gameObject.GetComponent<Player>().ShieldOnOf(3f, collision.gameObject.GetComponent<Player>().shield_obj));
			Destroy(this.gameObject);
		}
	}

	void Start()
    {
		StartCoroutine(WaitD(czas_zniszczenia));
	}

	void Update()
	{
		if (gameObject.transform.position.y <= -30f) //out of hexagons
		{
			Destroy(this.gameObject);
		}
	}
}
