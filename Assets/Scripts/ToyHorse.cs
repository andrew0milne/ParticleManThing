using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToyHorse : Button 
{
	public GameObject fire;
	public Image whiteScreen;
	float alpha;
	Color imageColour;

	// Use this for initialization
	void Start () 
	{
		imageColour = whiteScreen.color;
		imageColour.a = 0.0f;
		fire.SetActive (false);
		whiteScreen.color = imageColour;
		whiteScreen.gameObject.SetActive (true);
	}

	IEnumerator WhiteOut()
	{

		yield return new WaitForSeconds (2.0f);

		while (alpha <= 1.0f) 
		{
			imageColour.a = alpha;
			whiteScreen.color = imageColour;
			alpha += 0.5f * Time.deltaTime;
			yield return new WaitForSeconds (Time.deltaTime);
		}


		yield return null;
	}

//	public override void InteractCheck()
//	{
//		if (Input.GetKeyDown (KeyCode.E)) 
//		{
//			fire.SetActive (true);
//			StartCoroutine (WhiteOut ());
//		}
//	}
}
