using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : MonoBehaviour
{
	public Map map;
	public Player player;
	
	public GameObject hex_neighbour_up;
	public GameObject hex_neighbour_down;

	public GameObject hex_neighbour_up_left;
	public GameObject hex_neighbour_up_right;

	
	public GameObject hex_neighbour_down_left;
	public GameObject hex_neighbour_down_right;

	public float shift_x;
	public float shift_z;

	public int hex_x_number;
	public int hex_y_number;

	public bool check;

	public int scale_height;

	public Color color; //= new Color(0.1f, 0.65f, 0.1f);

	void MakeNeighbour(GameObject neighbour, float shift_x, float shift_z, int x, int y)
	{
		if (neighbour == null)
		{
			GameObject NewHexagon = Instantiate(gameObject, new Vector3(
				gameObject.transform.position.x + shift_x,
				gameObject.transform.position.y + 0f,
				gameObject.transform.position.z + shift_z
				), Quaternion.Euler(-90f,0f, 0f), transform.parent);

			neighbour = NewHexagon;

			NewHexagon.GetComponent<Hexagon>().hex_x_number += x;
			NewHexagon.GetComponent<Hexagon>().hex_y_number += y;

			scale_height = Random.Range(1, 4); 
			NewHexagon.GetComponent<Transform>().localScale = new Vector3(1, 1, scale_height);

			NewHexagon.GetComponent<Renderer>().material.color = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f,1f), Random.Range(0.5f, 1f));
		}
		else neighbour = null;
	}

	void Start()
	{
		shift_x = 2.03f;
		shift_z = 2.45f;

		gameObject.name = "hexagon" + hex_x_number + hex_y_number;
		check = false;
		map.hexagons.Add(gameObject);
	}

	 void FixedUpdate()//sprawdzanie co 1s, uptade co klatke
	{
		hex_neighbour_up = GameObject.Find("hexagon" + hex_x_number + (hex_y_number+1));
		hex_neighbour_down = GameObject.Find("hexagon" + hex_x_number + (hex_y_number-1));

		hex_neighbour_up_left = GameObject.Find("hexagon" + (hex_x_number-1) + (hex_y_number + 1));
		hex_neighbour_up_right = GameObject.Find("hexagon" + (hex_x_number + 1) + hex_y_number);

		hex_neighbour_down_left = GameObject.Find("hexagon" + (hex_x_number - 1) + hex_y_number);
		hex_neighbour_down_right = GameObject.Find("hexagon" + (hex_x_number +1) + (hex_y_number - 1));
	}


	void OnCollisionEnter(Collision collision)
	{
		//Debug.Log(collision.gameObject.name);
		if (collision.gameObject.tag == "player" && check==false)
		{
			player.GiveFood(-1);
			check = true;
			player.Steps.Value++;
			//Debug.Log(player.Steps.Value);

			MakeNeighbour(hex_neighbour_up, 0, shift_z, 0, 1);
			MakeNeighbour(hex_neighbour_down, 0, -shift_z, 0, -1);
			
			MakeNeighbour(hex_neighbour_up_left, -shift_x, shift_z/2, -1, 1);
			MakeNeighbour(hex_neighbour_up_right, shift_x, shift_z/2, 1, 0);

			MakeNeighbour(hex_neighbour_down_left, -shift_x, -shift_z/2, -1, 0);
			MakeNeighbour(hex_neighbour_down_right, shift_x, -shift_z/2, 1, -1);

		}
	}
	
}
