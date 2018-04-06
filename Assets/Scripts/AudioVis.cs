using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVis : MonoBehaviour 
{

	public GameObject sampleCube;
	GameObject[] cubes = new GameObject[512];
	public float max_scale;

	// Use this for initialization
	void Start () 
	{
		for(int i = 0; i < 8; i++)
		{
			GameObject tempCube = (GameObject)Instantiate (sampleCube);
			tempCube.transform.position = this.transform.position;
			tempCube.transform.parent = this.transform;
			tempCube.name = "TempCube" + i;
			//this.transform.eulerAngles = new Vector3 (0, -0.703125f * i, 0);
			tempCube.transform.position = new Vector3(this.transform.position.x * i, 0.0f, 0.0f);
			cubes [i] = tempCube;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		for (int i = 0; i < 8; i++) 
		{
			if (cubes != null) 
			{
				//cubes [i].transform.localScale = new Vector3 (1.0f, AudioEffect.band_buffer [i] * max_scale+0.2f, 1.0f);
			}
		}
	}
}
