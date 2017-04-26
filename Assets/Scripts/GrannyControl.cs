using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GrannyControl : MonoBehaviour {

	static Animator anim;
	public Transform elvis;
	private Rigidbody rb;

	private Vector3 heading;
	private float distance;
	private Vector3 currentlyKnownElvisPosition;
	private bool onGround = true;
	private bool firstTimeMoving = true;
//	private AssetBundle myLoadedAssetBundle;
//	private string[] scenePaths;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> (); 
		rb = GetComponent<Rigidbody> ();
		this.transform.LookAt (elvis);
		currentlyKnownElvisPosition = elvis.position;
		heading = new Vector3 (elvis.transform.position.x - this.transform.position.x, 
			0, elvis.transform.position.z - this.transform.position.z);
		distance = heading.magnitude;
//		myLoadedAssetBundle = AssetBundle.LoadFromFile("Assets/AssetBundles/scenes");
//		scenePaths = myLoadedAssetBundle.GetAllScenePaths();
	}
	
	// Update is called once per frame
	void Update () {
		currentlyKnownElvisPosition = elvis.position;
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
				anim.SetTrigger ("chaseElvis");
				transform.Translate (Vector3.forward * Time.deltaTime);

				// rotate SLOWLY
				float degreesPerSecond = 10;
				heading = new Vector3 (elvis.transform.position.x - this.transform.position.x, 
					0, elvis.transform.position.z - this.transform.position.z);
				Vector3 targetDir = elvis.position - transform.position;
				Vector3 newDir = Vector3.RotateTowards (transform.forward, heading.normalized, 0.5f * Time.deltaTime, 0.0F);
				transform.rotation = Quaternion.LookRotation (newDir);
				//	transform.rotation = Quaternion.RotateTowards(transform.rotation, elvis.rotation, degreesPerSecond * Time.deltaTime);
				//	transform.Rotate (elvis.rotation.eulerAngles * degreesPerSecond * Time.deltaTime);

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
				anim.SetTrigger ("isIdle");
				firstTimeMoving = true;
			}
		} else {
			transform.Rotate (Vector3.right * 180 * Time.deltaTime);
			transform.Translate(Vector3.down * 2 * Time.deltaTime, Space.World);
		}
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.tag == "Elvis") {
			SceneManager.LoadScene("LoseScreen", LoadSceneMode.Single);
		}
	}
}
