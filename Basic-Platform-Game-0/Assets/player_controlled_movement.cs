using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controlled_movement : MonoBehaviour {

	private Rigidbody2D spaceman_rigid_body;

	[SerializeField]
	private float movement_speed;

	[SerializeField]
	private Transform[] ground_points;

	[SerializeField]
	private float ground_radius;

	[SerializeField]
	private LayerMask what_is_ground;

	private bool is_grounded;

	private bool facing_right;

	private bool jump;

	[SerializeField]
	private float jump_force;

	private void Start () {
		spaceman_rigid_body = GetComponent<Rigidbody2D> ();
		facing_right = true;
	}

	private void Update (){
		handle_input ();
	}
		
	private void FixedUpdate () {
		float horizontal = Input.GetAxis ("Horizontal");

		is_grounded = is_grounded_check();

		movement(horizontal);

		flip (horizontal);

		reset_values ();
	}

	private void movement(float horizontal) {
		spaceman_rigid_body.velocity = new Vector2 (horizontal * movement_speed, spaceman_rigid_body.velocity.y);

		if (is_grounded && jump) {
			is_grounded = false;

			spaceman_rigid_body.AddForce(new Vector2(0, jump_force)); 	
		}
	}

	private void flip(float horizontal) {
		if ((horizontal > 0 && !facing_right) || (horizontal < 0 && facing_right)) {

			facing_right = !facing_right;

			Vector3 the_scale = transform.localScale;

			the_scale.x *= -1;

			transform.localScale = the_scale;
		}
	}

	private void handle_input(){
		if (Input.GetKey (KeyCode.Space)) {
			jump = true;
		}
	}

	private bool is_grounded_check() {
		if (spaceman_rigid_body.velocity.y <= 0.05) {
			foreach (Transform point in ground_points) {
				Collider2D[] colliders = Physics2D.OverlapCircleAll (point.position, ground_radius, what_is_ground);

				for (int i = 0; i < colliders.Length; i++) {
					if (colliders [i].gameObject != gameObject) {
						return true;
					}
				}
			}
		}
		return false;
	}

	private void reset_values(){
		jump = false;
	}
}