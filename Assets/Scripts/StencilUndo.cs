using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StencilUndo : MonoBehaviour {

	public Material new_mat;
	public GameObject obj;

	Renderer rend;
	BoxCollider box;

	// Use this for initialization
	void Start () 
	{
		rend = obj.GetComponent<Renderer>();
		box = obj.GetComponent<BoxCollider> ();
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") 
		{
			
			box.enabled = true;
			rend.material = new_mat;
		}
	}

	// Update is called once per frame
	void Update () 
	{
		
	}
}
