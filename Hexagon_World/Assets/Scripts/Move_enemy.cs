using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move_enemy : MonoBehaviour
{
	//[SerializeField]
	Transform _destination; //player

	NavMeshAgent _navMeshAgent;
	//[SerializeField]
	Vector3[] array_way;
	LineRenderer lineRenderer;
	public bool Attack;
	SphereCollider sphereCollider;
	int start =0;
	Animator animator;

	public float speed_walk;
	public float speed_run;

	GameObject player_o;

    void Start()
    {
		player_o = GameObject.FindGameObjectWithTag("player");
		_destination = player_o.transform;

		sphereCollider = this.GetComponent<SphereCollider>();
		animator = this.GetComponent<Animator>();
		_navMeshAgent = this.GetComponent<NavMeshAgent>();
		lineRenderer = this.GetComponent<LineRenderer>();
	
		array_way = new Vector3[lineRenderer.positionCount];

		for (int i = 0; i < lineRenderer.positionCount; i++)
		{
			array_way[i] = lineRenderer.GetPosition(i); 
		}
		gameObject.GetComponent<LineRenderer>().enabled = false; 
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "player")
		{
			Attack = true;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "player")
		{
			Attack = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "player")
		{
			Attack = false;
		}
	}

	private void SetDestination(Transform transform1)
	{
	//	if (_destination != null)
	//	{
			Vector3 targetVector = transform1.transform.position;
			_navMeshAgent.SetDestination(targetVector);
	//	}
		//throw new NotImplementedException();
	}

	private void SetDestination2(Vector3[] vector3s)
	{
		if (start == vector3s.Length)
		{
			start = 0;
		}
		else if (gameObject.transform.position.x == vector3s[start].x && gameObject.transform.position.z == vector3s[start].z)
		{
			start++;
		}
		else if (gameObject.transform.position != vector3s[start])
		{
			_navMeshAgent.SetDestination(vector3s[start]);
		}
	}

	void Update()
    {
		if (Attack == true)
		{
			SetDestination(_destination);
			animator.SetFloat("Forward",1);
			_navMeshAgent.speed = speed_run;
		}
		else
		{
			SetDestination2(array_way);
			animator.SetFloat("Forward", 0.5f);
			_navMeshAgent.speed = speed_walk;
		}
	}
	
}
