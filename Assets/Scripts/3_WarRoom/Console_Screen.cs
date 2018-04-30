using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console_Screen : Console 
{
	
	public GameObject win_screen;

	public AudioSource wrong;
	public AudioSource correct;
	public AudioSource win;

	public GameObject[] portals = new GameObject[2];

	public int[] answer = new int[3];
	int current_answer_pos = 0;
	int answer_step = -1;

    // LUKE ADDED THIS
    //[SerializeField]
    //private ScenarioHandler scenario;

    [SerializeField]
    private Text consoleHeaderText;
	// ---------------

	// Use this for initialization
	override public void Init () 
	{
		win_screen.SetActive (false);
		//option_buttons.SetActive (true);
	}

	public bool InputNumber (int num)
	{
		if (answer [current_answer_pos] == num && answer_step == current_answer_pos && pointer.activeSelf == true) 
		{
			Debug.Log ("Answer Correct");
			//correct.Play ();
			current_answer_pos++;
            //consoleHeaderText.text = "Enter Symbols " + current_answer_pos + "/3";


//			if ((current_answer_pos - 1) < 2) 
//			{
//				portals [current_answer_pos - 1].SendMessage ("Activate");
//			}

			if (current_answer_pos >= answer.Length) 
			{
				Finished ();
			}

			return true;
		} 
		else 
		{
			Debug.Log ("Answer Wrong");
			//wrong.Play ();

			return false;
		}
	}

	void UpdateAnswerStep()
	{
		answer_step++;
	}

	void Finished()
	{
		win_screen.SetActive (true);
		option_buttons.SetActive (false);
		//win.Play ();
		Debug.Log ("WELL DONE");

        //scenario.CompletedInput();
	}
}
