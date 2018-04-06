using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset_Button : MonoBehaviour 
{

	public GameObject layer1;
	public GameObject layer2;
	public GameObject layer3;
	public GameObject layer4;

	// Use this for initialization
	void Start () 
	{
		
	}

	void Reset()
	{
		layer1.SendMessage ("SetDirection", 1);
		layer2.SendMessage ("SetDirection", 1);
		layer3.SendMessage ("SetDirection", 1);
		layer4.SendMessage ("SetDirection", 1);

		layer1.SendMessage ("Reset");
		layer2.SendMessage ("Reset");
		layer3.SendMessage ("Reset");
		layer4.SendMessage ("Reset");
	}

	// Update is called once per frame
	void Update () 
	{
		
	}
}
