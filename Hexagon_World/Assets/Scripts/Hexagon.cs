using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : MonoBehaviour
{
	GameObject obj;
	// Zmienne dostania sie do skryptow
	public Map map;
	public Player player;
	public LevelManager levelManager;
	// jesli wejdziemy na heksagon bedzie odznaczony
	public bool check;
	// wysokosc heksagonu, mozliwe ze bd wykorzystywal do zmiany koloru
	public float scale_height;
	//kolor podstawaowy heksagonu, zielony jasny
	public Color kolor = new Color(0.2f,0.5f,0);
	//public Texture texture;
	public GameObject sprtie;
	
	
	void Start()
	{
	//	if (this.gameObject.active == false) Debug.Log(gameObject.name);//Destroy(this.gameObject);
	//	Debug.Log("bez "+gameObject.name);

		obj = GameObject.FindGameObjectWithTag("player");
		player = obj.GetComponent<Player>();


		//poczatkowe wartosci
		scale_height = gameObject.GetComponent<Transform>().localScale.z;
		check = false;
		//dodanie do listy heksagonow, wiemy o rozmiarze
		map.hexagons.Add(gameObject);
	}

	
	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "player")
		{
			if(player == null) player = collision.gameObject.GetComponent<Player>();
			player.Last_stay_hexagon = this.gameObject;
		}
			
		if (collision.gameObject.tag == "player" && check==false)
		{
			player.GiveFood(-1);
			check = true;
			player.GiveSteps(1);

			sprtie.transform.localPosition = new Vector3(sprtie.transform.localPosition.x, sprtie.transform.localPosition.y, 0.3f);
			sprtie.GetComponent<SpriteRenderer>().color = gameObject.GetComponent<Renderer>().material.color;
			sprtie.SetActive(true);
		}
	}


	/*
			gameObject.GetComponent<Renderer>().material.shaderKeywords = new string[1] { "_NORMALMAP" };
			gameObject.GetComponent<Renderer>().material.SetTexture("_BumpMap", texture);
			
			
			gameObject.GetComponent<Renderer>().material.EnableKeyword("_DETAIL_MULX2");
			gameObject.GetComponent<Renderer>().material.SetTexture("_DetailAlbedoMap", texture);
			*/
	/*
	gameObject.GetComponent<Renderer>().material.EnableKeyword("_DETAIL_MULX2");
	gameObject.GetComponent<Renderer>().material.EnableKeyword("_NORMALMAP");
	gameObject.GetComponent<Renderer>().material.SetTexture("_DetailNormalMap", texture);
	*/
	//gameObject.GetComponent<Renderer>().material.color = kolor;

}
