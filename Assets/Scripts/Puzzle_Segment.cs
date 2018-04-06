using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_Segment : MonoBehaviour 
{

	public GameObject puzzleLayer;
	public GameObject worldLayer;

	public GameObject OtherLayer;

	public GameObject PuzzleController;

	public float rotateAmount;
	public float startAngle;
	//float currentRotate;
	int rotateTarget;

	public float speed;

	bool reseting;
	bool rotating;
	int rotateDirection;

	// Use this for initialization
	void Start () 
	{
		rotating = false;
		reseting = false;
		//currentRotate = 0.0f;
	}

	void SetDirection(int dir)
	{
		if (rotating == false) {
			rotateDirection = dir;
		}
	}

	void Rotate(bool clicked)
	{
		if (rotating == false) 
		{
			if (clicked) 
			{
				OtherLayer.SendMessage ("SetDirection", -rotateDirection);
				OtherLayer.SendMessage ("Rotate", false);
			} 

			rotateTarget = Mathf.RoundToInt(rotateAmount) * rotateDirection + Mathf.RoundToInt(puzzleLayer.transform.localEulerAngles.y);

			if (rotateTarget >= 360) 
			{
				rotateTarget -= 360;
			} 
			else if (rotateTarget < 0) 
			{
				rotateTarget += 360;
			}

			rotating = true;
		}
	}

	void Reset()
	{
		reseting = true;
		rotating = true;
	}

	// Update is called once per frame
	void Update () 
	{
		if (rotating) 
		{
			puzzleLayer.transform.RotateAround (puzzleLayer.transform.position, puzzleLayer.transform.up, speed*Time.deltaTime * rotateDirection);
			worldLayer.transform.RotateAround  (worldLayer.transform.position,  worldLayer.transform.up,  speed*Time.deltaTime * rotateDirection);

			//puzzleLayer.transform.localRotation = Quaternion.Euler (0.0f, Mathf.RoundToInt (puzzleLayer.transform.localEulerAngles.y), 0.0f);

			//currentRotate += 0.5f;

			if (reseting == true) 
			{
				if (Mathf.RoundToInt(puzzleLayer.transform.localEulerAngles.y) == startAngle) 
				{
					rotating = false;
					reseting = false;
					puzzleLayer.transform.localRotation = Quaternion.Euler (0.0f, Mathf.RoundToInt (puzzleLayer.transform.localEulerAngles.y), 0.0f);
					worldLayer.transform.localRotation = Quaternion.Euler (0.0f, Mathf.RoundToInt (worldLayer.transform.localEulerAngles.y), 0.0f);
					//currentRotate = 0.0f;
				}
			} 
			else 
			{
				//Debug.Log (Mathf.RoundToInt(puzzleLayer.transform.localEulerAngles.y) + " = " + (int)rotateTarget);
				if (Mathf.RoundToInt(puzzleLayer.transform.localEulerAngles.y) == rotateTarget) 
				{
					rotating = false;
					//currentRotate = 0.0f;
					puzzleLayer.transform.localRotation = Quaternion.Euler (0.0f, Mathf.RoundToInt (puzzleLayer.transform.localEulerAngles.y), 0.0f);
					worldLayer.transform.localRotation = Quaternion.Euler (0.0f, Mathf.RoundToInt (worldLayer.transform.localEulerAngles.y), 0.0f);

					PuzzleController.SendMessage ("Check");
				}
			}
		}
	}
}
