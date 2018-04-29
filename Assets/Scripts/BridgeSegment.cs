using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeSegment : MonoBehaviour 
{

	public Transform player_location;

	public float range, min, max;

	Vector3 full_scale, no_scale, startPos, endPos;

	// Use this for initialization
	void Start () 
	{
		full_scale = new Vector3 (1.0f, 1.0f, 1.0f);
		no_scale = new Vector3 (0.0f, 0.0f, 0.0f);
		player_location = GameObject.FindGameObjectWithTag ("Player").transform;
		startPos = new Vector3 (this.transform.position.x, this.transform.position.y - 1.0f, this.transform.position.z);
		endPos = this.transform.position;

	}

	float GetDistance()
	{
		float dist = Vector3.Distance (player_location.position, this.transform.position);

		dist = (dist - min) / max;

		//dist /= range;

		//dist = dist * dist;
	

		return dist;
	}

	// Update is called once per frame
	void Update () 
	{
		this.transform.localScale = Vector3.Lerp (full_scale, no_scale, GetDistance () - 0.2f);
		this.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(Color.black, Color.white, GetDistance ()));
		this.transform.localPosition = Vector3.Lerp (endPos, startPos, GetDistance());
	}
}
