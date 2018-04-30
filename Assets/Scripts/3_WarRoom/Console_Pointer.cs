using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console_Pointer : MonoBehaviour 
{

	public Sprite pointer;
	public Sprite hand;

	public Canvas canvas;

	bool upsidedown;

	Vector3 current_pos, previous_pos;

	public void Init(bool upd, Canvas can)
	{
		upsidedown = upd;
		//canvas = can;
		current_pos = new Vector3 (0.0f, 0.0f, 0.0f);
	}

	void UpdatePosition(Vector3 pos)
	{
		previous_pos = current_pos;
		current_pos = pos;

		if (upsidedown) 
		{
			Vector3 vec = current_pos - canvas.transform.position;
			transform.position = canvas.transform.position - vec;
		} 
		else 
		{
			transform.position = current_pos;
		}
	}

	void Clickable(bool b)
	{
		if (b == true) 
		{
			GetComponent<Image> ().sprite = hand;
		} 
		else 
		{
			GetComponent<Image> ().sprite = pointer;
		}
	}
}
