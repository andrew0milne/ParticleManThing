using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console_Button : MonoBehaviour 
{
	public int num;
	public string button_text;

	public GameObject screen;
	Console_Screen screen_script;
	Console_Answer answer_script;

	bool activated = false;

	bool finished = false;

	bool main_console;

	// Use this for initialization
	void Start () 
	{
		GetComponentInChildren<Text> ().text = button_text;

		if (screen.GetComponent<Console_Screen> () != null) 
		{
			screen_script = screen.GetComponent<Console_Screen> ();
			main_console = true;
		}
		else if (screen.GetComponent<Console_Answer> () != null) 
		{
			answer_script = screen.GetComponent<Console_Answer> ();
			main_console = false;
		}
	}
		
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Pointer")
		{
			Vector3 tempPos = col.gameObject.GetComponent<Image>().transform.position;
			activated = true;
			ToggleButton (activated);
			col.gameObject.SendMessage ("Clickable", activated);
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.tag == "Pointer")
		{
			activated = false;
			ToggleButton (activated);
			col.gameObject.SendMessage ("Clickable", activated);
		}
	}

	public void ToggleButton(bool toggle)
	{
		if (toggle == true) 
		{
			this.GetComponent<Image> ().color = Color.green;
		}
		else 
		{
			this.GetComponent<Image> ().color = Color.red;
		}
	}

	public void Activate()
	{
		if (main_console) 
		{
			finished = screen_script.InputNumber (num);
		} 
		else 
		{
			finished = answer_script.InputNumber ();
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if (activated == true && Input.GetMouseButtonDown(0) || activated == true && Input.GetButton("A")) 
		{
			Activate ();
		}

		if (finished) 
		{
			GetComponentInChildren<Text> ().color = new Color (1.0f, 1.0f, 1.0f, 0.5f);
			this.GetComponent<Image> ().color = Color.green;
			Destroy (this);
		}

	}
}
