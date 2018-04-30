using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PostProcessingControl : MonoBehaviour 
{

	public PostProcessingProfile ppp;

	public GameObject player;

	public float perlin_scale;

	ChromaticAberrationModel.Settings aberration_model;

	bool player_here = false;

	public AudioEffect audioEffect;

	// Use this for initialization
	void Start () 
	{
		aberration_model = ppp.chromaticAberration.settings;

		aberration_model.intensity = 0.0f; 

		if (audioEffect == null) 
		{
			audioEffect = GameObject.Find ("Audio").GetComponent<AudioEffect>();
		}
	}
		
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") 
		{
			player_here = true;
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.tag == "Player") 
		{
			player_here = false;
			aberration_model.intensity = 0.0f;
			ppp.chromaticAberration.settings = aberration_model;
		}
	}

	float GetDistance()
	{
		float dist = Vector3.Distance (gameObject.transform.position, player.transform.position);

		return 9.5f - dist;
	}

	// Update is called once per frame
	void Update () 
	{
		if (player_here) 
		{
			aberration_model.intensity = GetDistance () + audioEffect.audio_band_buffer [1];//(Mathf.PerlinNoise(Time.time * 7.0f, 0.0f) * perlin_scale);
			ppp.chromaticAberration.settings = aberration_model;
		}
	}
}
