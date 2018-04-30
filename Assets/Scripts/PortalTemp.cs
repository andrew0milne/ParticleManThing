using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalTemp : MonoBehaviour 
{

	public Transform receiver;
	public Image img;

	public float speed = 1.0f;
	public float activate_speed = 1.0f;

	Vector3 posisition_offset;

	Vector3 rotationOffset;

	Color transparent;

	public bool activate_on_spawn = false;

	public bool canTeleport = true;

	GameObject player;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");

		transparent = new Color (255.0f, 255.0f, 255.0f, 0.0f);

		posisition_offset = new Vector3 (0.0f, 0.0f, 0.0f);
		rotationOffset = receiver.transform.rotation.eulerAngles;// new Vector3 (-90.0f, -90.0f, -90.0f);

		//rotationOffset.y = 0.0f;
		//rotationOffset.x -= 0.0f;
		//rotationOffset.z = 0.0f;

		if (activate_on_spawn) 
		{
			GetComponent<Renderer> ().material.color = Color.white;
		} 
		else 
		{
			GetComponent<Renderer> ().material.color = transparent;
			GetComponent<Renderer> ().material.SetColor ("_EmissionColor", Color.black);
			gameObject.SetActive (false);
		}

	}

	IEnumerator TurnOn()
	{
		float t = 0.0f;

		Renderer rend = GetComponent<Renderer> ();

		while (t < 1.1f) 
		{
			rend.material.color = Color.Lerp (transparent, Color.white, t);
			GetComponent<Renderer> ().material.SetColor ("_EmissionColor", Color.Lerp (Color.black, Color.white, t));
			t += activate_speed * Time.deltaTime;
			yield return null;
		}

		yield return null;
	}

	void Activate()
	{
		StartCoroutine (TurnOn ());
	}

	IEnumerator Teleport(GameObject temp)
	{
		canTeleport = false;
		receiver.GetComponent<PortalTemp> ().canTeleport = false;


		float t = 0.0f;

		while (t < 1.1f) 
		{
			img.color = Color.Lerp (transparent, Color.white, t);
			t += speed * Time.deltaTime;

			yield return null;
		}

		player.transform.position = receiver.position - posisition_offset;

		//Debug.Log (player.transform.rotation);
		player.transform.rotation = Quaternion.Euler(0.0f, rotationOffset.y - 180.0f, 0.0f);

		//Rotate(new Vector3(200.0f, 180.0f, 0.0f));
		//player.transform.Rotate (rotationOffset.eulerAngles);
		//Debug.Log (player.transform.rotation);
		//Debug.Log (rotationOffset);

		yield return new WaitForSeconds (0.2f);

		t = 0.0f;

		while (t < 1.5f) 
		{
			img.color = Color.Lerp (Color.white, transparent, t);
			t += speed * Time.deltaTime;
			yield return null;
		}

		//gameObject.SetActive (false);

		yield return null;
	}
		

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player" && canTeleport == true) 
		{			
			StartCoroutine (Teleport (col.gameObject));
			//receiver.GetComponent<PortalTemp> ().StartCoroutine (Wait ());
		}
	}

	IEnumerator Wait()
	{
		
	

		yield return new WaitForSeconds (5.0f);


		Debug.Log ("sup");
		//canTeleport = true;

		yield return null;
	}

	void OnTriggerExit(Collider col)
	{
		if (col.tag == "Player") 
		{

			canTeleport = true;
			//StartCoroutine (Wait ());
		}
	}

	// Update is called once per frame
	void Update () 
	{
		
	}
}
