using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controlled_movement : MonoBehaviour {

	private Rigidbody2D spaceman_rigid_body;

	void Start () {
		spaceman_rigid_body = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");

		Movement(horizontal, vertical);
	}

	void Movement(float horizontal, float vertical) {
		spaceman_rigid_body.velocity = new Vector2 (horizontal * 5, vertical * 5);
	}
}