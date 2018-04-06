using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PostProcessingControl : MonoBehaviour 
{

	//public PostProcessingProfile currentProfile;
	public PostProcessingProfile ppp;
	public PostProcessingProfile defaultProfile;

	public Light sun;

	public GameObject player;
	public Camera main_camera;
	float crazyness;
	public float speed;

    public bool pppOn;

	ChromaticAberrationModel.Settings aberration_model;
	ColorGradingModel.Settings colour_model;
	MotionBlurModel.Settings blur_model;

	public float max_chromatic_aberration;
	public float max_saturation;
	public float max_motion_blur;
	float default_fov;
	float current_fov;
	public float max_fov;
	float default_camera_sway;
	float current_camera_sway;
	public float max_camera_sway;

	// Use this for initialization
	void Start () 
	{
		//ppp.chromaticAberration.enabled = true;
		colour_model = ppp.colorGrading.settings;
		aberration_model = ppp.chromaticAberration.settings;
		blur_model = ppp.motionBlur.settings;
		crazyness = 0.0f;
		default_fov = 60.0f;
		current_fov = default_fov;
		main_camera.fieldOfView = current_fov;
		default_camera_sway = 100.0f;
		current_camera_sway = default_camera_sway;
		//player.SendMessage ("SetDrunkness", current_camera_sway);
	}
		
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") 
		{
			sun.transform.Rotate(new Vector3(-10.0f, 100.0f, 30.0f));
		}
	}

	void SetSpeed(float num)
	{
		speed = num;
	}

	// Update is called once per frame
	void Update () 
	{
		aberration_model.intensity = crazyness; //player_distance;
		blur_model.frameBlending = crazyness/5.0f;
		colour_model.basic.saturation = 1.0f + crazyness/2.0f;
		current_camera_sway -= crazyness * 2.0f;
		current_fov = default_fov + crazyness * 10.0f;

		if (aberration_model.intensity > max_chromatic_aberration) 
		{
			aberration_model.intensity = max_chromatic_aberration;
		}

		if (blur_model.frameBlending > max_motion_blur) 
		{
			blur_model.frameBlending = max_motion_blur;
		}

		if (colour_model.basic.saturation > max_saturation) 
		{
			colour_model.basic.saturation = max_saturation;
		}

		if (current_fov > max_fov) 
		{
			current_fov = max_fov;
		}

		if (current_camera_sway < max_camera_sway) 
		{
			current_camera_sway = max_camera_sway;
		}

        if (pppOn)
        {
			ppp.chromaticAberration.settings = aberration_model;
			ppp.colorGrading.settings = colour_model;
			ppp.motionBlur.settings = blur_model;
			main_camera.fieldOfView = current_fov;
			//player.SendMessage ("SetDrunkness", current_camera_sway);
        }


		crazyness += 0.5f * Time.deltaTime * speed;
	}
}
