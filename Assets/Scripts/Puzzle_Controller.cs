using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_Controller : MonoBehaviour 
{
	
	public GameObject pLayer1;
	public GameObject pLayer2;
	public GameObject pLayer3;
	public GameObject pLayer4;

	public GameObject FinalMirror;

	public int finishAngle;

	void Check()
	{
		if (Mathf.RoundToInt(pLayer1.transform.localEulerAngles.y) == finishAngle && 
			Mathf.RoundToInt(pLayer2.transform.localEulerAngles.y) == finishAngle && 
			Mathf.RoundToInt(pLayer3.transform.localEulerAngles.y) == finishAngle && 
			Mathf.RoundToInt(pLayer4.transform.localEulerAngles.y) == finishAngle) 
		{
			FinalMirror.SetActive (true);
		}
	}
}
