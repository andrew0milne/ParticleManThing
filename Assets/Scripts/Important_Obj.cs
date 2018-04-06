using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Important_Obj : MonoBehaviour 
{

	public GameObject player;

	float distance;
	public float max_distance;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		distance = Vector3.Distance (player.transform.position, this.transform.position);

		if (distance < max_distance) 
		{
			// calculate vector from object to player
			Vector3 objectVector = new Vector3(
				Camera.main.transform.position.x - transform.position.x,
				Camera.main.transform.position.y - transform.position.y,
				Camera.main.transform.position.z - transform.position.z
			);

			// get player's forward vector
			Vector3 cameraLook = Camera.main.transform.forward;

			// calculate angle between object and player (and normalize to find speed multiplier)
			float objectAngle = Vector3.Angle(objectVector, Camera.main.transform.forward);
			float speedMultiplier = (objectAngle / 180.0f);


			speedMultiplier += 1.0f;

			float intensity = ((speedMultiplier * speedMultiplier) + ((max_distance - distance)/100.0f))-1.0f;// * 100.0f;  //Mathf.Sqrt((1 - (distance / max_distance)) * speedMultiplier * (speedMultiplier + 1.0f));

			//Debug.Log ((max_distance - distance)/100.0f);

			//player.SendMessage ("SetFlareLevel", intensity);
		}

	}
}
