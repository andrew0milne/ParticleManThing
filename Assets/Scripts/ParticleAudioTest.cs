using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAudioTest : MonoBehaviour 
{

	ParticleSystem.ShapeModule pSystem;
	ParticleSystem.EmissionModule emmision;
	ParticleSystem.MainModule main;
	ParticleSystemRenderer rend;
	public Material mat;

	public Light light;

	AudioEffect audio_effect;

	AudioSource audio;

	public int num;

	public float scale;

	float c = 0.1f;

	bool played = false;

	public Color min_colour = new Color(0.2f, 0.2f, 0.2f);
	public Color max_colour = new Color(0.6f, 0.6f, 0.6f);

	// Use this for initialization
	void Start () 
	{
		pSystem = this.GetComponent<ParticleSystem> ().shape;
		emmision = this.GetComponent<ParticleSystem> ().emission;
		main = this.GetComponent<ParticleSystem> ().main;
		audio_effect = this.GetComponent<AudioEffect> ();
		audio = this.GetComponent<AudioSource> ();
		mat.SetColor ("_EmissionColor", min_colour);
	}

	void OnTriggerEnter(Collider col)
	{
		Debug.Log ("hello");
		if (col.tag =="Player")
		{
			audio.Play ();
			played = true;
		}
	}

	// Update is called once per frame
	void Update () 
	{
		//pSystem.startSize = c;
		if (audio.isPlaying) 
		{
			pSystem.randomPositionAmount = audio_effect.audio_band [num] / scale;
			light.intensity = 1.0f + audio_effect.audio_band [num];
			mat.SetColor("_EmissionColor", Color.Lerp(min_colour, max_colour, audio_effect.audio_band_buffer [num]));
		}

		//Debug.Log (audio_effect.audio_band [num]);

		//pSystem.start *= AudioEffect.band_buffer [num];

		//this.transform.localScale = new Vector3(audio_effect.band_buffer [num], audio_effect.band_buffer [num], audio_effect.band_buffer [num]);
		c+= 0.1f;

		if (audio.isPlaying == false && played == true) 
		{
			emmision.rateOverTime = 0.0f;
			main.gravityModifier = 1.0f;
			main.simulationSpeed = 0.3f;
			mat.SetColor("_EmissionColor", Color.Lerp(mat.GetColor("_EmissionColor"), Color.black, c));
			light.intensity -= Time.deltaTime;
			c += Time.deltaTime;
		}
	}
}
