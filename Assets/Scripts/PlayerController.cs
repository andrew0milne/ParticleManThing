using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2.0f, sensitivity = 5.0f, upDownRange = 6.0f;

    private float forwardSpeed = 0.0f, sideSpeed = 0.0f, verticalRotation = 0.0f, verticalVelocity = 0.0f;

    private CharacterController characterController;

    private bool playerEnabled = true;

	public Text HUD_text;

	public bool console_active = false;

    private float stepCycle = 0.5f, stepTimer = 0.0f;

    //jacobs rotation code for controller
    private float rotY;
    private float rotX;

	void OnEnable()
    {
        // Intro Sequence - Disable then re-enable the player
        //EventManager.OnGameStart += DisablePlayer;
        //EventManager.OnIntroComplete += EnablePlayer;
    }

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Console") 
		{
			console_active = true;

			//col.gameObject.GetComponentInChildren<Console> ().ActivatePointer (true);
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.tag == "Console") 
		{
			console_active = false;

			//col.gameObject.GetComponentInChildren<Console> ().ActivatePointer (false);
		}
	}

    // Use this for initialization
    void Start()
    {

        // Disable the cursor
        Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;

        // Get Components
        characterController = GetComponent<CharacterController>();
        rotY = transform.rotation.eulerAngles.x;
        rotX = transform.rotation.eulerAngles.y;

		//RotatePlayer(new Vector3(0.0f, 180.0f, 0.0f));
    }

	void RotatePlayer(Vector3 angle)
	{
		Quaternion rot = Quaternion.Euler(angle.x, angle.y, angle.y);
		transform.Rotate (angle);//  new Vector3(angle.x, angle.y, angle.z);
	}

    // Update is called once per frame
    void Update()
    {
        // If the player is active
        if(playerEnabled)
        {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(new Vector3((Screen.width / 2), (Screen.height / 2)));

			if (Physics.Raycast(ray.origin, ray.direction, out hit, 7.0f))
			{
				if (hit.collider.gameObject.tag == "PuzzleLayer")
				{
                    Debug.Log("Puzzle ray cast");
					HUD_text.text = "LMB/RMB to rotate this segment";
					if (Input.GetMouseButtonDown(0) || Input.GetButton("A"))
					{
						hit.collider.gameObject.SendMessage("SetDirection", 1);
						hit.collider.gameObject.SendMessage("Rotate", true);
					}

					else if (Input.GetMouseButtonDown(1) || Input.GetButton("B"))
					{
						hit.collider.gameObject.SendMessage("SetDirection", -1);
						hit.collider.gameObject.SendMessage("Rotate", true);

					}

				}
				else if (hit.collider.gameObject.tag == "PuzzleReset")
				{
					HUD_text.text = "LMB reset the puzzle";
					if (Input.GetMouseButtonDown(0))
					{
						hit.collider.gameObject.SendMessage("Reset");
					}

				}
				else
				{
					HUD_text.text = "";
				}

			}
			else
			{
				HUD_text.text = "";

			}

            // Camera Rotation
            //float rotLeftRight = Input.GetAxis("Mouse X") * 100;
            //rotLeftRight *= Time.deltaTime;
            //transform.Rotate(0, rotLeftRight, 0);

            //verticalRotation -= Input.GetAxis("Mouse Y") * 100;
            //verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);

            //Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

            // New rotational code to allow for delta time and controller input
            float mouseY = -Input.GetAxis("Mouse Y");
            float mouseX = Input.GetAxis("Mouse X");
            
            rotY += mouseY * sensitivity * Time.deltaTime;
            rotX = mouseX * sensitivity * Time.deltaTime;

			transform.Rotate (new Vector3(0.0f, rotX, 0.0f));

           // Quaternion xRotation = Quaternion.Euler(0.0f, rotX, 0.0f);
            //transform.localRotation = xRotation;
            rotY = Mathf.Clamp(rotY, -upDownRange, upDownRange);


            Quaternion localRotation = Quaternion.Euler(rotY, 0.0f, 0.0f);
            Camera.main.transform.localRotation = localRotation;
            // Move the camera
            // Movement
            forwardSpeed = Input.GetAxis("Vertical") * moveSpeed;
            sideSpeed = Input.GetAxis("Horizontal") * moveSpeed;

            // Stop the player falling if they're grounded
            verticalVelocity = characterController.isGrounded ? 0 : verticalVelocity + (Physics.gravity.y * Time.deltaTime);

            Vector3 speed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed);
            speed = transform.rotation * speed;
            characterController.Move(speed * Time.deltaTime);

			if (console_active) 
			{
				RaycastHit hitConsole;
				Ray rayConsole = Camera.main.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0.0f));
				if (Physics.Raycast (rayConsole, out hitConsole)) 
				{
					if (hitConsole.collider.gameObject.tag == "Screen") 
					{
						hitConsole.collider.gameObject.SendMessage ("UpdateConsolePointer", hitConsole.point);
					}
				}
			}

            // Increase the step timer
            stepTimer += Time.deltaTime;
            
            // Check if the player is moving
            if(speed.sqrMagnitude > 0.0f)
            {
                // Check if the player is grounded
                if(characterController.isGrounded)
                {
                    // If the step timer exceeds the cycle interval
                    if (stepTimer > stepCycle)
                    {
                        // Reset the timer
                        stepTimer -= 0.5f;

                        // Post a footstep event
                        //AkSoundEngine.PostEvent("footstep", gameObject);
                    }
                }

            }
            else
            {
                stepTimer = 0.0f;
            }
        }
    }

	void DisablePlayer()
    {
        playerEnabled = false;
    }

    void EnablePlayer()
    {
        playerEnabled = true;
    }

}
