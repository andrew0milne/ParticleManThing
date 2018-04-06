using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
//using UnityEditor.AI;
using UnityEngine.AI;

public class MazeGenerator : MonoBehaviour 
{
	public GameObject map;

	//NavMeshSurface nms;

	private Transform[] allChildren;

	const int mazeSize = 17;

	private GameObject[,] mazeObjects = new GameObject[mazeSize,mazeSize];
	int[,,] maze = new int[,,]
	{
		{
			{0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
			{0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1}, 
			{0, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1}, 
			{0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1}, 
			{0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 0, 1}, 
			{0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 1, 0, 1}, 
			{0, 1, 1, 0, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 0, 1}, 
			{0, 1, 1, 0, 0, 0, 0, 1, 0, 1, 1, 1, 0, 0, 0, 0, 1}, 
			{0, 1, 1, 0, 1, 1, 1, 1, 0, 1, 0, 0, 0, 1, 1, 1, 1}, 
			{0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 1}, 
			{1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 0, 1, 1, 0, 1}, 
			{1, 2, 0, 1, 1, 0, 1, 0, 0, 0, 0, 1, 0, 1, 1, 0, 1}, 
			{1, 1, 0, 1, 1, 0, 1, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1}, 
			{0, 1, 0, 1, 1, 0, 1, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1}, 
			{0, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0}, 
			{0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0}, 
		},
		{
			{0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
			{0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 1}, 
			{0, 1, 0, 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
			{0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1}, 
			{0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 1}, 
			{0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1}, 
			{0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1}, 
			{0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 0, 1}, 
			{0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 1, 0, 1}, 
			{0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 1, 1, 0, 1, 1, 0, 1}, 
			{1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 1}, 
			{1, 2, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 1, 1, 1}, 
			{1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 1, 1}, 
			{0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1}, 
			{0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0}, 
			{0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0}, 
		},
	};

		
	public GameObject wallPrefab;
	public GameObject mazeStartPos;
	public GameObject toyHorse;
	public GameObject mazeFloor;

	public int mazeNum;
	public int nextMazeNum;
	public int maxMazeNum;

    Color emissionColour = new Color(0.0f, 0.0f, 0.0f);

	float mazeScale;

	public bool active;
	float time;

	// Use this for initialization
	void Start () 
	{
		time = 0.0f;
		mazeNum = 0;
		nextMazeNum = mazeNum++;

		mazeScale = wallPrefab.transform.localScale.x;

		//nms = map.GetComponent<NavMeshSurface> ();
		//nms.

		GameObject floor = Instantiate (mazeFloor, new Vector3 ((mazeSize * mazeScale)/2.0f + mazeStartPos.transform.position.x- 0.5f * mazeScale, mazeStartPos.transform.position.y, (mazeSize * mazeScale)/2.0f + mazeStartPos.transform.position.z- 0.5f * mazeScale), Quaternion.identity, this.gameObject.transform);
		Vector3 tempScale = new Vector3(mazeSize * mazeScale, 1.0f, mazeSize * mazeScale);
		floor.transform.localScale = tempScale;

		for (int z = 0; z < mazeSize; z++) 
		{
			for (int x = 0; x < mazeSize; x++) 
			{
				if (maze [mazeNum, z, x] != 2) 
				{
					mazeObjects [z, x] = Instantiate (wallPrefab, new Vector3 (x * mazeScale + mazeStartPos.transform.position.x, mazeStartPos.transform.position.y + 0.5f * mazeScale, z * mazeScale + mazeStartPos.transform.position.z), Quaternion.identity, this.gameObject.transform);
                   // mazeObjects[z, x].GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                    mazeObjects[z, x].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);

                    if (maze [mazeNum, z, x] == 0) 
					{
						mazeObjects [z, x].transform.localScale -= mazeObjects [z, x].transform.localScale;
						mazeObjects [z, x].GetComponent<BoxCollider> ().enabled = false;
                        
                        mazeObjects [z, x].SetActive (false);
					}
				} 
				else 
				{
					Vector3 tempLocation = new Vector3 (x * mazeScale + mazeStartPos.transform.position.x, mazeStartPos.transform.position.y + 0.79f, z * mazeScale + mazeStartPos.transform.position.z);
					toyHorse.transform.position = tempLocation;
				}
			}
		}

		//nms.BuildNavMesh ();

		// Fill the children list
		allChildren = GetComponentsInChildren<Transform>();
	}

	IEnumerator ChangeMaze ()
	{
		active = true;
		Vector3 start = new Vector3 (0.0f, 0.0f, 0.0f);
		Vector3 end = new Vector3 (mazeScale, mazeScale, mazeScale);

		float time = 0.0f;

        while (time <= 1.5f)
        {
            for (int z = 0; z < mazeSize; z++)
            {
                for (int x = 0; x < mazeSize; x++)
                {
                    if (maze[mazeNum, z, x] != maze[nextMazeNum, z, x])
                    {
                        if (maze[nextMazeNum, z, x] == 0)
                        {
                            mazeObjects[z, x].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(Color.white, Color.black, time));
                        }
                        else if (maze[nextMazeNum, z, x] == 1)
                        {
                            mazeObjects[z, x].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(Color.black, Color.white, time));
                        }
                    } 
                }
            }
            //nms.BuildNavMesh ();
            yield return new WaitForSeconds(Time.deltaTime);
            time += Time.deltaTime * 4.0f;
        }

        time = 0.0f;

        while (time <= 1.1f) 
		{
			for (int z = 0; z < mazeSize; z++) 
			{
				for (int x = 0; x < mazeSize; x++) 
				{
					if (maze [mazeNum, z, x] != maze [nextMazeNum, z, x]) 
					{
						if (maze [nextMazeNum, z, x] == 0) 
						{
							mazeObjects [z, x].SetActive(true);
							mazeObjects [z, x].transform.localScale = Vector3.Slerp (start, end, time);                        
                            mazeObjects [z, x].GetComponent<BoxCollider> ().enabled = true;
                           // mazeObjects[z, x].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(Color.white, Color.black, time));
                        } 
						else if(maze [nextMazeNum, z, x] == 1) 
						{
							//mazeObjects [z, x].SetActive(false);
							mazeObjects [z, x].transform.localScale = Vector3.Slerp (end, start, time);
                           // mazeObjects[z, x].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(Color.black, Color.white, time));
                            //mazeObjects [z, x].GetComponent<BoxCollider> ().enabled = false;
						}
					}
				}
			}
			//nms.BuildNavMesh ();
			yield return new WaitForSeconds (Time.deltaTime);
			time += Time.deltaTime * 2.0f;
		}

		for (int z = 0; z < mazeSize; z++) 
		{
			for (int x = 0; x < mazeSize; x++) 
			{
				if (maze [mazeNum, z, x] == 0) 
				{
					mazeObjects [z, x].SetActive(false);
				}
			}
		}

		//nms.BuildNavMesh ();

		mazeNum++;
		if(mazeNum >= maxMazeNum)
		{
			mazeNum = 0;
		}

		nextMazeNum = mazeNum + 1;
		if (nextMazeNum >= maxMazeNum) 
		{
			nextMazeNum = 0;
		}

		active = false;

		yield return null;
	}

	IEnumerator RemoveMaze()
	{
		active = true;

		Vector3 start = new Vector3 (mazeScale, mazeScale, mazeScale);
		Vector3 end = new Vector3 (0.0f, 0.0f, 0.0f);

		float time = 0.0f;

		while (time <= 1.1f) 
		{
			for (int z = 0; z < mazeSize; z++) 
			{
				for (int x = 0; x < mazeSize; x++) 
				{
						if (maze [nextMazeNum, z, x] == 1) 
						{
							mazeObjects [z, x].SetActive(true);
							mazeObjects [z, x].transform.localScale = Vector3.Lerp (start, end, time);
							mazeObjects [z, x].GetComponent<BoxCollider> ().enabled = false;
						} 
				}
			}
			//nms.BuildNavMesh ();
			yield return new WaitForSeconds (Time.deltaTime);
			time += Time.deltaTime * 2.0f;
		}

		yield return null;
	}

	void Toggle()
	{
		StartCoroutine (ChangeMaze ());
	}

	void EndMaze()
	{
		//Start
	}

	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown (KeyCode.Q) && active == false)
		{
			Toggle ();
		}
	}
}
