using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Player_Controller : MonoBehaviour 
{
    // Movement Variables
	public float movementSpeed = 2f;
	public float mouseSensitivity = 5f;
	public float jumpSpeed = 5f;
	public float upDownRange = 60f;
    float forwardSpeed = 0f, sideSpeed = 0f;
    float verticalRotation = 0f;
	float verticalVelocity = 0f;
	float rotLeftRight = 0.0f;
	CharacterController characterController;

    // Scrying Mirror
	public GameObject mirror;
	bool holding_mirror;
	bool mirror_toggle;
	public GameObject mirror_default_location;
	public GameObject mirror_used_position;
	float fracJourney = 1.1f;

    // Post proccessor
    public GameObject postP;


	///////////////////////////////

	public float drunkness;
	public Camera this_camera;
	public GameObject player_model;
	Vector3 camera_pos;
	float head_bob;
	float counter = 0.0f;
    public bool drunk;

	Head_Bob headBob = new Head_Bob();

	public LensFlare flare;
	public Light mirror_light;

	
	public bool cursorLocked = false;

	public Text onScreenText;


	private bool redEaten = false;
	private bool blueEaten = false;


	// Use this for initialization
	void Start () 
	{
		mirror.transform.SetPositionAndRotation (mirror_default_location.transform.position, mirror.transform.rotation);
		holding_mirror = false;
		mirror_toggle = false;

		Cursor.visible = false;

		characterController = GetComponent<CharacterController> ();

		headBob.SetUp (this_camera);
	}

	void TogglePause()
	{
		if (cursorLocked == true) 
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			cursorLocked = false;
		} 
		else if (cursorLocked == false) 
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			cursorLocked = true;
		}
	}

	void SetFlareLevel(float brightness)
	{
		//flare.brightness = brightness;
		mirror_light.intensity = brightness;
	}

	void SetDrunkness(float num)
	{
		drunkness = num;
	}

	// Update is called once per frame
	void Update () 
	{
		if (cursorLocked == true) 
		{

			RaycastHit hit;		
			Ray ray = Camera.main.ScreenPointToRay (new Vector3 ((Screen.width / 2), (Screen.height / 2)));

			if (Physics.Raycast (ray.origin, ray.direction, out hit, 5.0f)) 
			{
				if (hit.collider.gameObject.tag == "PickUp") 
				{
					onScreenText.text = "Press LMB to pick up";
					if (Input.GetMouseButtonDown (0))
                    {
						hit.collider.gameObject.SetActive (false);
					}
				} 
				else if (hit.collider.gameObject.tag == "PuzzleLayer") 
				{
					onScreenText.text = "Press LMB to turn";
					if (Input.GetMouseButtonDown (0)) 
					{
						hit.collider.gameObject.SendMessage ("SetDirection", 1);
						hit.collider.gameObject.SendMessage ("Rotate", true);
					} 
					else if (Input.GetMouseButtonDown (1)) 
					{
						hit.collider.gameObject.SendMessage ("SetDirection", -1);
						hit.collider.gameObject.SendMessage ("Rotate", true);

					}
				} 
				else if (hit.collider.gameObject.tag == "PuzzleReset") 
				{
                    onScreenText.text = "Reset the Puzzle";
                    if (Input.GetMouseButtonDown (0)) 
					{
						hit.collider.gameObject.SendMessage ("Reset");
					} 
				}
				else if (hit.collider.gameObject.tag == "Mirror") 
				{
					onScreenText.text = "What is this?";
					if (Input.GetMouseButtonDown (0)) 
					{
						hit.collider.gameObject.SetActive (false);
						mirror.SetActive (true);
						holding_mirror = true;
					}
				} 
				else 
				{
                    onScreenText.text = "";
				}

			}
            else
            {
                onScreenText.text = "";
            } 
			

			//CameraRoatation
			float rotLeftRight = Input.GetAxis ("Mouse X") * mouseSensitivity + Mathf.Sin(Time.time)/drunkness;
			transform.Rotate (0, rotLeftRight, 0);

			verticalRotation -= Input.GetAxis ("Mouse Y") * mouseSensitivity + Mathf.Cos(Time.time)/drunkness;
			verticalRotation = Mathf.Clamp (verticalRotation, -upDownRange, upDownRange);


			Vector3 newCameraPosition = this_camera.transform.localPosition;

			if (sideSpeed != 0.0f || forwardSpeed != 0.0f) 
			{

				newCameraPosition.y = headBob.DoHeadBob (1.0f);
				this_camera.transform.localPosition = newCameraPosition;
				player_model.transform.localPosition = newCameraPosition;
			} 
			else 
			{
				newCameraPosition = this_camera.transform.localPosition;
				player_model.transform.localPosition = newCameraPosition;
	
			}
				
			//Moves the camera and the player light
			Camera.main.transform.localRotation = Quaternion.Euler (verticalRotation, 0, 0);
			//if (holding_mirror) 
			//{
			//	//mirror.transform.localRotation = Quaternion.Euler (verticalRotation, 0, 0);
			//	//Debug.Log ("mirror");
			//}

			//Movement
			forwardSpeed = Input.GetAxis ("Vertical") * movementSpeed;
			sideSpeed = Input.GetAxis ("Horizontal") * movementSpeed;

			if (characterController.isGrounded) 
			{
				verticalVelocity = 0;
			}
			else
			{
				verticalVelocity += Physics.gravity.y * Time.deltaTime * 2;
			}

			Vector3 speed = new Vector3 (sideSpeed, verticalVelocity, forwardSpeed);

			speed = transform.rotation * speed;

			characterController.Move (speed * Time.deltaTime);

		}

		if(Input.GetKeyDown (KeyCode.Escape))
		{
			TogglePause ();
		}

		if(Input.GetKeyDown (KeyCode.E))
		{
			mirror_toggle = true;
			fracJourney = 0.0f;
			postP.SendMessage ("SetSpeed", 1.1f);
		}
		else if(Input.GetKeyUp (KeyCode.E))
		{
			mirror_toggle = false;
			fracJourney = 0.0f;
			postP.SendMessage ("SetSpeed", 0.11f);
		}

		if (mirror_toggle == true & fracJourney <= 1.0f) 
		{
			fracJourney += 4.0f * Time.deltaTime;
			mirror.transform.position = Vector3.Lerp (mirror.transform.position, mirror_used_position.transform.position, fracJourney);
		} 
		else if (mirror_toggle == false & fracJourney <= 1.0f) 
		{
			fracJourney += 4.0f * Time.deltaTime;
			mirror.transform.position = Vector3.Lerp (mirror.transform.position, mirror_default_location.transform.position, fracJourney);

		}
	}
}

