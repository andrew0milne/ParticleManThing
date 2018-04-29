using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeBuilder : MonoBehaviour 
{

	public Transform player_location;

	public GameObject bridge_segment;

	public GameObject parent;

	public int[] bridge_size = new int[2];

	public GameObject[,] bridge;

	// Use this for initialization
	void Start () 
	{
		bridge = new GameObject[bridge_size [0], bridge_size [1]];

		for (int i = 0; i < bridge_size [0]; i++) 
		{
			for (int j = 0; j < bridge_size [1]; j++) 
			{
				Vector3 segment_pos = new Vector3 (-j, 0.0f, i);
				bridge [i, j] = Instantiate (bridge_segment, segment_pos, Quaternion.identity, gameObject.transform);
				bridge[i,j].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
