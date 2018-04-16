using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CielaSpike;

public class GroundWave : MonoBehaviour 
{

	Transform player_location;
	Transform actual_player_location;

	Vector3 controller_location;
	Vector3 this_position;

	float player_height;

	float wave_scale;

	float speed;

	float distance;

	float min, max, x_location, y_location;

	Vector3 endPos, startPos, player_pos;

	float time = 0.0f;

	float num;

	public Color endColor;
	public Color startColor;
	public Color player_color;

	Color final_emission;

	Material thisMat;

	AudioEffect audio_effect;

	float band;

	bool start_low;

	bool playing = true;

	Vector3 scale_offset;

	bool going = true;

	Vector3 perlin_location = Vector3.zero;

	// Use this for initialization
	void Start () 
	{
		Physics.IgnoreLayerCollision (10, 10);

		if (audio_effect == null) 
		{
			//audio_effect = GameObject.Find ("Audio").GetComponent<AudioEffect>();
		}
	}

	public void Init(Transform controller_loc, Transform p_location, float p_height, float wScale, float spd, float mn, float mx, float xLoc, float yLoc, bool low, float scale)
	{
		player_location = p_location;
		player_height = p_height;

		scale_offset = new Vector3 (0.0f, transform.localScale.y / 2.0f, 0.0f);

		wave_scale = wScale;

		speed = spd;

		min = mn;
		max = mx;

		start_low = low;

		controller_location = controller_loc.position;

		if (start_low) 
		{
			player_pos = transform.position - scale_offset;
			startPos = new Vector3 (player_pos.x, player_pos.y - wScale / 2.0f, player_pos.z);
			endPos = new Vector3 (player_pos.x, player_pos.y - wScale, player_pos.z);
		} 
		else 
		{
			player_pos = transform.position - scale_offset;
			startPos = new Vector3 (player_pos.x, player_pos.y + wScale / 2.0f, player_pos.z);
			endPos = new Vector3 (player_pos.x, player_pos.y + wScale, player_pos.z);
		}

		time = 0.0f;

		thisMat = GetComponent<Renderer> ().material;

		x_location = xLoc / scale;
		y_location = yLoc / scale;

		this.StartCoroutineAsync (Move ());
	}

	IEnumerator Move()
	{
		while (going) 
		{
			

			num = Mathf.PerlinNoise (x_location + time, y_location + time);
			num += Mathf.PerlinNoise (x_location - (time + 0.2f), y_location - (time + 0.2f));

			num /= 2.0f;




			perlin_location = endPos;

			perlin_location.y += num * wave_scale;
				//Vector3 (endPos.x, endPos.y + num * wave_scale, endPos.z);  //Vector3.Lerp (startPos, endPos, num);
			//this.transform.localPosition = new Vector3 (endPos.x, endPos.y + num * wave_scale, endPos.z);//Vector3.Lerp (startPos, endPos, num);
			this_position = perlin_location;

			this_position.y = controller_location.y;


			yield return Ninja.JumpToUnity;

			Vector3 p_pos = player_location.position;

			yield return Ninja.JumpBack;

			distance = Vector3.Distance (p_pos, this_position);


			yield return Ninja.JumpToUnity;


			final_emission = Color.Lerp (startColor, endColor, num);

			final_emission = Color.Lerp (player_color, final_emission, (distance - min) / max);

			//band = 1.0f - audio_effect.average_band_buffer/ 2.0f;

			//final_emission = Color.Lerp(Color.red, final_emission, band);


			this.transform.localPosition = perlin_location;
			this.transform.localPosition = Vector3.Slerp (player_pos, transform.position, ((distance - min) / max) - 0.1f);

			thisMat.SetColor ("_EmissionColor", Color.Lerp (player_color, thisMat.GetColor ("_EmissionColor"), (distance - min) / max));
			thisMat.SetColor ("_EmissionColor", final_emission);


			time += Time.deltaTime;
			yield return Ninja.JumpBack;
		}

		yield return null;
	}


	// Update is called once per frame
//	void Update () 
//	{
//		time += Time.deltaTime;
//
//		num = Mathf.PerlinNoise (x_location + time, y_location + time);
//		num += Mathf.PerlinNoise (x_location - (time + 0.2f), y_location - (time + 0.2f));
//
//		num /= 2.0f;
//
//		this.transform.localPosition = new Vector3 (endPos.x, endPos.y + num * wave_scale, endPos.z);//Vector3.Lerp (startPos, endPos, num);
//		thisMat.SetColor("_EmissionColor", Color.Lerp(startColor, endColor, num * 2.0f));
//
//		this_position = transform.position;
//
//		this_position.y = controller_location.y;
//
//		//if (start_low) 
//		//{
//
//		distance = Vector3.Distance (player_location.position, this_position);
//		//} 
////		else 
////		{
////			distance = Vector3.Distance (player_location.position + scale_offset, transform.position);
////		}
//
//		this.transform.localPosition = Vector3.Slerp (player_pos, transform.position, ((distance - min) / max) - 0.1f);
//	
//		thisMat.SetColor("_EmissionColor", Color.Lerp(player_color, thisMat.GetColor("_EmissionColor") , (distance - min) / max));
//
//		final_emission = Color.Lerp(startColor, endColor, num);
//
//		final_emission = Color.Lerp (player_color, final_emission, (distance - min) / max);
//
//		//band = 1.0f - audio_effect.average_band_buffer/ 2.0f;
//
//		//final_emission = Color.Lerp(Color.red, final_emission, band);
//
//		thisMat.SetColor ("_EmissionColor", final_emission);
//	}
}
