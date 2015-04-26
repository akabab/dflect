using UnityEngine;
using System.Collections;

public class cameraControler : MonoBehaviour {
	Camera cam;
	public float maxDistance = 30.0f;

	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera>();
	}

	void UpdateCameraValue(float val) {
		if (val != 0.0f) {
			cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - (val * 3.0f), 1.0f, maxDistance);
		}
	}

	// Update is called once per frame
	void Update () {
		UpdateCameraValue(Input.GetAxis("Mouse ScrollWheel"));
	}
}
