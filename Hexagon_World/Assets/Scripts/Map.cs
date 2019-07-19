using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
	//lista heksagonow, potrzeba znajomosci rozmiaru
	public Hexagon hexagon;
	public List<GameObject> hexagons = new List<GameObject>();
	public int size_hexagons;
    
    void Update()
    {
		size_hexagons = hexagons.Count;
	}
}
