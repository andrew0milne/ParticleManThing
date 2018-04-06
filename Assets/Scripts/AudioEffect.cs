using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class AudioEffect : MonoBehaviour 
{
	AudioSource thisAudioSource;
	public float[] samples = new float[512];
	public float[] freq_bands = new float[8];
	public float[] band_buffer = new float[8];
	float[] buffer_decrease = new float[8];

	public float[] freq_band_heighest = new float[8];
	public float[] audio_band = new float[8];
	public float[] audio_band_buffer = new float[8];

	public float audio_profile;

	public float average_band_buffer;

	// Use this for initialization
	void Start () 
	{
		thisAudioSource = GetComponent<AudioSource> ();
		AudioProfile (audio_profile);
	}
	
	// Update is called once per frame
	void Update () 
	{
		GetSpectrumData ();
		MakeFreqBands ();
		BandBuffer ();
		CreateAudioBands ();
		GetAverageBandBuffer ();
	}

	void GetAverageBandBuffer()
	{
		for (int i = 0; i < 8; i++) 
		{
			average_band_buffer += audio_band_buffer [i];
		}
		average_band_buffer /= 8.0f;
	}

	void AudioProfile(float audio_profile)
	{
		for (int i = 0; i < 8; i++) 
		{
			freq_band_heighest [i] = audio_profile;
		}
	}

	void CreateAudioBands()
	{
		for (int i = 0; i < 8; i++) 
		{
			if (freq_bands [i] > freq_band_heighest [i]) 
			{
				freq_band_heighest [i] = freq_bands [i];
			}
			audio_band [i] = freq_bands [i] / freq_band_heighest [i];
			audio_band_buffer [i] = band_buffer [i] / freq_band_heighest [i];
		}
	}

	void GetSpectrumData()
	{
		thisAudioSource.GetSpectrumData (samples, 0, FFTWindow.Blackman);
	}

	void MakeFreqBands()
	{
		int count = 0;

		for (int i = 0; i < 8; i++) 
		{
			float average = 0;
			int sampleCount = (int)Mathf.Pow (2, i) * 2;
			if (i == 7) 
			{
				sampleCount += 7;
			}

			for (int j = 0; j < sampleCount; j++) {
				if (count < 512) {
					average += samples [count] * (count + 1);
					count++;
				}
			}

			average /= count;

			freq_bands [i] = average * 10;
		}
	}

	void BandBuffer()
	{
		for (int i = 0; i < 8; i++) 
		{
			if (freq_bands [i] > band_buffer [i]) 
			{
				band_buffer [i] = freq_bands [i];
				buffer_decrease [i] = 0.005f;
			}

			if (freq_bands [i] < band_buffer [i]) 
			{
				band_buffer [i] -= buffer_decrease [i];
				buffer_decrease [i] *= 1.2f;
			}
		}
	}
}
