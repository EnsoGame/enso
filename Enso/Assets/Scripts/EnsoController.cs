using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Lane {
	left,
	middle,
	right
}
	

public class EnsoController : MonoBehaviour {

	// Movement Parameters
	public float runSpeed;
	public Lane currentLane = Lane.middle;
	public float laneSize;

	Rigidbody myRB;

	// Use this for initialization
	void Start () {
		myRB = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			switch (currentLane) {
			case Lane.middle:
				currentLane = Lane.left;
				StartCoroutine(SideMove (new Vector3(0, 0, laneSize), 0.2f));
				break;
			case Lane.right:
				currentLane = Lane.middle;
				StartCoroutine(SideMove (new Vector3(0, 0, laneSize), 0.2f));
				break;
			default:
				break;
			}
		}

		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			switch (currentLane) {
			case Lane.left:
				currentLane = Lane.middle;
				StartCoroutine(SideMove (new Vector3(0, 0, -laneSize), 0.2f));
				break;
			case Lane.middle:
				currentLane = Lane.right;
				StartCoroutine(SideMove (new Vector3(0, 0, -laneSize), 0.2f));
				break;
			default:
				break;
			}
		}
	}

	// FixedUpdate is called every specific timedelta 
	void FixedUpdate () {
		// Get l/r arrow or a/d press from keyboard
		float move = Input.GetAxis ("Horizontal");

		myRB.velocity = new Vector3 (move * runSpeed, myRB.velocity.y, myRB.velocity.z);
	}

	IEnumerator SideMove(Vector3 d, float t) {
		float rate = 1.0f/t;
		float index = 0.0f;
		Vector3 startPosition = transform.position;
		Vector3 endPosition = startPosition + d;
		while (index < 1.0) {
			transform.position = Vector3.Lerp (startPosition, endPosition, index);
			index += rate * Time.deltaTime;
			yield return 0;
		}
		transform.position = endPosition;
	}
}
