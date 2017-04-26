using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
	}
}
