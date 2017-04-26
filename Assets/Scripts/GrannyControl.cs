using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GrannyControl : MonoBehaviour {

	private Animator anim;
	private Transform elvis;

	private Vector3 heading;
	private float distance;
	private bool onGround = true;
	private bool firstTimeMoving = true;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> (); 
		elvis = GameObject.FindWithTag ("Elvis").transform;
		this.transform.LookAt (elvis);
		heading = new Vector3 (elvis.transform.position.x - this.transform.position.x, 
			0, elvis.transform.position.z - this.transform.position.z);
		distance = heading.magnitude;
	}
	
	// Update is called once per frame
	void Update () {
		elvis = GameObject.FindWithTag ("Elvis").transform;
		heading = new Vector3 (elvis.transform.position.x - this.transform.position.x, 
			0, elvis.transform.position.z - this.transform.position.z);
		distance = heading.magnitude;

		if (onGround) {
			if (distance < 5) {
				if (firstTimeMoving) {
					this.transform.LookAt (elvis);
				}
				firstTimeMoving = false;

				// move
				if (anim.GetBool ("chasingElvis") == false) {
					anim.SetBool ("chasingElvis", true);
					anim.SetBool ("isIdle", false);
				}
				anim.SetTrigger ("chaseElvis");
				transform.Translate (Vector3.forward * 1.8f * Time.deltaTime);

				// rotate SLOWLY
				float degreesPerSecond = 10;
				heading = new Vector3 (elvis.transform.position.x - this.transform.position.x, 
					0, elvis.transform.position.z - this.transform.position.z);
				Vector3 newDir = Vector3.RotateTowards (transform.forward, heading.normalized, 0.5f * Time.deltaTime, 0.0F);
				transform.rotation = Quaternion.LookRotation (newDir);

				RaycastHit hit;
				Vector3 dir = new Vector3 (0, -1, 0);
				if (Physics.Raycast (this.transform.position + new Vector3 (0, 0.1f, 0), dir, out hit, 10)) {

				} else {
					onGround = false;
					anim.SetTrigger ("fallOffPlatform");
					transform.Rotate (Vector3.right * 180 * Time.deltaTime);
					transform.Translate (Vector3.down * 2 * Time.deltaTime, Space.World);
				}
			} else {
				if (anim.GetBool ("isIdle") == false) {
					anim.SetBool ("isIdle", true);
					anim.SetBool ("chasingElvis", false);
				}
				firstTimeMoving = true;
			}
		} else {
			transform.Rotate (Vector3.right * 180 * Time.deltaTime);
			transform.Translate(Vector3.down * 2 * Time.deltaTime, Space.World);
			Destroy (this.gameObject, 3);
		}
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.tag == "Elvis") {
			SceneManager.LoadScene("LoseScreen", LoadSceneMode.Single);
		}
	}
}
