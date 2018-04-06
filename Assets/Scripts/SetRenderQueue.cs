using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRenderQueue : MonoBehaviour 
{

//	public int queueNum;
//	public Material mat;
//
//	// Use this for initialization
//	void Start () 
//	{
//		mat.renderQueue = queueNum;
//	}
//
//	void Update()
//	{
//		mat.renderQueue = queueNum;
//	}


	[SerializeField]
	protected int[] m_queues = new int[]{3000};

	protected void Awake() {
		Material[] materials = GetComponent<Renderer>().materials;
		for (int i = 0; i < materials.Length && i < m_queues.Length; ++i) {
			materials[i].renderQueue = m_queues[i];
		}
	}
}
