using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class ElvisControl : MonoBehaviour {

	private NavMeshAgent agent;
	private Animator anim;
	private int counter = 3;
	public Transform grannyPrefab;

	private bool destinationSet = false;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();
		InvokeRepeating ("spawnGranny", 3.0f, 5.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindWithTag ("Granny") == null && counter <= 0) {
			SceneManager.LoadScene("WinScreen", LoadSceneMode.Single);
		}

		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;

			if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100)) {
				NavMeshPath path = new NavMeshPath ();
				if (agent.CalculatePath(this.transform.position, path)) {
					if (path.status == NavMeshPathStatus.PathComplete) {
						agent.destination = hit.point;
						if (anim.GetBool ("isWalking") == false) {
							anim.SetBool ("isWalking", true);
							anim.SetBool ("stopWalking", false);
						}
						destinationSet = true;
					}
				}
			}
		}

		// restarts the game
		if (Input.GetKeyDown (KeyCode.R)) 
		{
//			Application.LoadLevel(0);
			SceneManager.LoadScene("Scene1", LoadSceneMode.Single);
			Time.timeScale = 1;
		}

		// quits the game
		if (Input.GetKey (KeyCode.Escape) || Input.GetKey (KeyCode.Q))
		{
			Application.Quit();
		}

		if (Input.GetKeyDown(KeyCode.P))
		{
			if (Time.timeScale == 1)
			{
				Time.timeScale = 0;
			}
			else
			{
				Time.timeScale = 1;
			}
		}

		if (Vector3.Distance(agent.destination, transform.position) <= 0.1f && destinationSet) {
			destinationSet = false;
			if (anim.GetBool ("stopWalking") == false) {
				anim.SetBool ("stopWalking", true);
				anim.SetBool ("isWalking", false);
			}
		}
	}

	void spawnGranny() {
		counter -= 1;
		if (counter >= 0) {
			Instantiate (grannyPrefab, new Vector3(Random.Range (-4, 5), 0, Random.Range (-4, 4)), Quaternion.identity);
		}
	}
}
