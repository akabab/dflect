using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed = 2f;
	public float maxSpeed = 3f;
	Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody>();
	}

	void Update() {
	}

	void FixedUpdate() {
		float xforce = Input.GetAxis ("Horizontal") * speed;
		float zforce = Input.GetAxis ("Vertical") * speed;
		if (zforce != 0.0f && xforce != 0.0f) {
			zforce /= 1.41421f;
			xforce /= 1.41421f;
		}
		// Debug.Log("force" + xforce);
		rb.velocity = new Vector3(xforce, 0, zforce);
	}
}
