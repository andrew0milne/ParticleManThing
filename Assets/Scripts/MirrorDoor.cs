using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorDoor : MonoBehaviour 
{

	public GameObject door;

	public Material NoTex;

	public Camera mainCamera;

	bool toggle;

	// Use this for initialization
	void Start () 
	{
		toggle = true;
	}

	void OnTriggerEnter(Collider col)
	{
		toggle = !toggle;

		if (col.tag == "Player") 
		{
			//door.GetComponent<Renderer> ().material = NoTex;
			door.SetActive (toggle);
			//Destroy(door);
		}
	}

	// Update is called once per frame
	void Update () 
	{
		
	}
}
