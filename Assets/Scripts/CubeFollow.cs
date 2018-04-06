using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFollow : MonoBehaviour 
{

	public GameObject parent;

	float num = 0.0f;

	public float follow_rate = 1.0f;

	// Use this for initialization
	void Start () 
	{


	}

	void GetDistance()
	{
		num = Vector3.Distance (this.transform.position, parent.transform.position);
	}

	// Update is called once per frame
	void Update () 
	{
		num += Time.deltaTime;

		this.transform.position = Vector3.Lerp (this.transform.position, parent.transform.position, num/follow_rate); 
	}
}
