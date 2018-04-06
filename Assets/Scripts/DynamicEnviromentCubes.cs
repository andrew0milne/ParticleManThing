using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicEnviromentCubes : MonoBehaviour 
{

	public float min_size;
	public float max_size;

	Vector3 min_scale, max_scale;

	public float speed;

	float time = 0.0f;

	bool scale_direction;

	Color min_color;

	public AudioEffect audio_effect;
	public bool channel_colour;

	float amount = 0.0f;
	float cc = 0.0f;
	float scale;

	public bool onPerson = false;
	public bool randomChannel = true;
	public int channel = 0;


	// Use this for initialization
	void Start () 
	{
		if (audio_effect == null) 
		{
			audio_effect = GameObject.Find ("Audio").GetComponent<AudioEffect>();
		}

		speed += Random.Range (0.0f, 5.0f);

		float rand = Random.Range (0.2f, 1.0f);

		min_color = new Color (rand, 0.0f, 0.0f);


		min_color = new Color (2.0f, 0.5f, 0.5f);


		min_size *= this.transform.localScale.x;
		max_size *= this.transform.localScale.x;

		min_scale = new Vector3 (min_size, min_size, min_size);
		max_scale = new Vector3 (max_size, max_size, max_size);

		scale = this.transform.localScale.x;

		if (randomChannel) 
		{
			channel = (int)Random.Range (0.0f, 7.0f);
		}

		Color col = Color.red;

		if (channel_colour) 
		{
			min_color = new Color (rand, rand, rand);

			switch (channel) 
			{
			case 0:
				col = new Color (1.0f, 0.0f, 0.0f);
				break;
			case 1:
				col = new Color (0.0f, 1.0f, 0.0f);
				break;
			case 2:
				col = new Color (0.0f, 0.0f, 1.0f);
				break;
			case 3:
				col = new Color (1.0f, 1.0f, 0.0f);
				break;
			case 4:
				col = new Color (1.0f, 0.0f, 1.0f);
				break;
			case 5:
				col = new Color (0.0f, 1.0f, 1.0f);
				break;
			case 6:
				col = new Color (1.0f, 0.5f, 0.5f);
				break;
			case 7:
				col = new Color (0.5f, 0.5f, 1.0f);
				break;
		
			}
		}
		this.GetComponent<Renderer> ().material.color = col;
	}
	
	// Update is called once per frame
	void Update () 
	{
		amount = scale + audio_effect.audio_band_buffer [channel]/5.0f;
		cc = 1.0f - audio_effect.audio_band_buffer [channel]/2.0f;
		//Debug.Log (cc);

		if (!onPerson) 
		{
			this.transform.localScale = new Vector3 (amount, amount, amount);//Vector3.Slerp (min_scale, max_scale, time);
		}
		this.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(min_color, Color.black, cc));

	}
}
