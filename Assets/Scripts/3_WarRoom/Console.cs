using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Console : MonoBehaviour 
{
	[SerializeField]
	protected GameObject pointer;

	[SerializeField]
	protected GameObject option_buttons;

	[SerializeField]
	protected Transform[] buttons;

	protected Vector3 pointer_start_pos;

	public Canvas canvas;

	[SerializeField]
	protected bool upsidedown = false;

	// Use this for initialization
	void Start () 
	{
		pointer_start_pos = pointer.transform.localPosition;
		//buttons = option_buttons.GetComponentsInChildren<Transform> ();
		pointer.GetComponent<Console_Pointer>().Init(upsidedown, canvas);
		Init();
	}

	virtual public void Init()
	{

	}

	public void ActivatePointer(bool toggle)
	{
		if (toggle == false) 
		{
			UpdateConsolePointer (pointer_start_pos);
			buttons = option_buttons.GetComponentsInChildren<Transform> ();

			foreach (Transform obj in buttons) 
			{
				if (obj.tag == "Button" && obj.gameObject.GetComponent<Console_Button> () != null) 
				{

					obj.gameObject.GetComponent<Console_Button> ().ToggleButton (toggle);
				}
			}
		}
		pointer.SetActive (toggle);
		//Debug.Log (toggle);
	}

	void UpdateConsolePointer(Vector3 pos)
	{
		pointer.SendMessage("UpdatePosition", pos);
	}
}
