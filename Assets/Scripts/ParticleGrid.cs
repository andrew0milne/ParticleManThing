using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleGrid : MonoBehaviour 
{
	public int width;
	public int length;

	ParticleSystem p_system;
	ParticleSystem.Particle[] particles;

	// Use this for initialization
	void Start () 
	{		
		p_system = GetComponent<ParticleSystem> ();

		particles = new ParticleSystem.Particle [p_system.particleCount];

		p_system.GetParticles (particles);

	
		length = (int)Mathf.Sqrt (particles.Length);

		for (int i = 0; i < length; i++) 
		{
			for (int j = 0; j < length; j++) 
			{
				Vector3 pos = new Vector3 (i, 20.0f, j);
				particles [(width * j) + i].position = pos;
			}
		}

		p_system.SetParticles (particles, particles.Length);
	}

	ParticleSystem.Particle MakeParticle(Vector3 pos)
	{
		ParticleSystem.Particle p = new ParticleSystem.Particle ();
		p.startLifetime = Mathf.Infinity;
		p.position = pos;

		return p;
	}

	// Update is called once per frame
	void Update () 
	{
		
	}
}
