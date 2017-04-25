using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrannyControl : MonoBehaviour {

	static Animator anim;
	public Transform elvis;
	private Rigidbody rb;

	private Vector3 heading;
	private float distance;
	private Vector3 currentlyKnownElvisPosition;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> (); 
		rb = GetComponent<Rigidbody> ();
		InvokeRepeating("resetDirection", 0.0f, 4f);
	}
	
	// Update is called once per frame
	void Update () {

		if (distance != null) {
			if (distance < 5) {
				// TODO even the rotation to look at the player must be delayed
				anim.SetTrigger ("chaseElvis");
				transform.LookAt (elvis);
				transform.position = Vector3.MoveTowards(transform.position, currentlyKnownElvisPosition, 2 * Time.deltaTime);
			} 
		} else {
			resetDirection ();
		}
//		transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * 2, Space.World);
//		RaycastHit hit;
//		Vector3 dir = new Vector3(0,-1,0);
//		Debug.DrawRay(transform.position,dir*10,Color.green);
//		if(Physics.Raycast(this.transform.position + new Vector3(0, 0.1f, 0),dir, out hit,10)){
//			print ("on ground" + hit.transform.name);
//		}
//		else{
//			print("fell offf!!!!!!!!!!!!!!!!!!!!!!");
//		}
	}

	void resetDirection() {
		currentlyKnownElvisPosition = elvis.position;
		heading = new Vector3 (elvis.transform.position.x - this.transform.position.x, 
			0, elvis.transform.position.z - this.transform.position.z);
		distance = heading.magnitude;
		print (distance);
	}
}
