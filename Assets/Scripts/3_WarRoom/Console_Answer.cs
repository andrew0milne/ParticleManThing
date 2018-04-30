using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console_Answer : Console 
{
	public GameObject screen;

	public bool InputNumber ()
	{
		if (pointer.activeSelf == true) 
		{
			screen.SendMessage ("UpdateAnswerStep");
			return true;
		}
		return false;
	}

	// Update is called once per frame
	void Update () 
	{
		
	}
}
