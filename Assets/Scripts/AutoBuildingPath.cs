using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBuildingPath : MonoBehaviour 
{
	public GameObject PathSegment;
	public GameObject waveSegment;
	public GameObject StartPosition;

	GameObject[,] path;

	public GameObject player;
	public float player_height;
	public float wave_scale;
	public float cube_scale;
	public float speed;
	public float max, min;

	public float noise_scale;

	[SerializeField]
	int pathwidth = 5;

	[SerializeField]
	int pathLength = 15;

	public bool groundWave = true;
	public bool start_low = false;

	float time = 0.0f;

	// Use this for initialization
	void Start () 
	{
		path = new GameObject[pathwidth, pathLength];

		cube_scale = waveSegment.transform.localScale.x;

		for (int i = 0; i < pathwidth; i++) 
		{
			for (int j = 0; j < pathLength; j++) 
			{
				Vector3 pos = new Vector3 (StartPosition.transform.position.x + j * cube_scale, StartPosition.transform.position.y, StartPosition.transform.position.z - i * cube_scale);
				if (groundWave) 
				{
					path[i,j] = Instantiate(waveSegment, pos, StartPosition.transform.rotation) as GameObject;
					path [i, j].GetComponent<GroundWave> ().Init (StartPosition.transform, player.transform, player_height, wave_scale, speed, min, max, i, j, start_low, noise_scale);
				}
				else
				{
					path [i, j] = Instantiate (PathSegment, pos, StartPosition.transform.rotation) as GameObject;
				}

			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
//		if (groundWave) 
//		{
//			time += Time.deltaTime;
//
//			for (int i = 0; i < pathwidth; i++) 
//			{
//				for (int j = 0; j < pathLength; j++) 
//				{					
//					//path [i, j].SendMessage ("SetHeight", (Mathf.PerlinNoise ((float)i/10.0f + time, (float)j/10.0f + time)));
//				}
//			}
//		}
	}
}
