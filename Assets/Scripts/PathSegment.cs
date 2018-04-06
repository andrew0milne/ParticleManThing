using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSegment : MonoBehaviour 
{
	GameObject player;

	public float min;
	public float max;

	Vector3 maxScale, minScale, startPos, endPos;

	float distance;

	// Use this for initialization
	void Start () 
	{
		maxScale = new Vector3 (1.0f, 0.2f, 1.0f);
		minScale = new Vector3 (0.0f, 0.0f, 0.0f);
		startPos = new Vector3 (this.transform.position.x, this.transform.position.y - 1.0f, this.transform.position.z);
		endPos = this.transform.position;
		distance = 0.0f;
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		distance = Vector3.Distance (player.transform.position, this.transform.position);
		this.transform.localScale = Vector3.Lerp (maxScale, minScale, (distance-min) / max);
		this.transform.localPosition = Vector3.Lerp (endPos, startPos, (distance - min) / max);
	}
}
