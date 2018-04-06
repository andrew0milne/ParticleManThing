using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head_Bob : MonoBehaviour 
{

	//Camera mainCamera;
	Vector3 cameraStartPos;
	float headBob;
	float counter;

	// Use this for initialization
	void Start () 
	{
		headBob = 0.0f;
		counter = 0.0f;
	}

	public void SetUp(Camera cam)
	{
		cameraStartPos = cam.transform.localPosition;
	}

	public float DoHeadBob(float speed)
	{
		headBob = Mathf.Sin (counter * 10.0f) / 10.0f + cameraStartPos.y;
		counter += Time.deltaTime;

		return headBob;
	}

	// Update is called once per frame
	void Update () 
	{
		
	}
}
