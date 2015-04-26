using UnityEngine;
using System.Collections;

public class lookAtMouse : MonoBehaviour {

	public GameObject canon;
	public GameObject bullet;
	public float bulletSpeed = 10f;
	public float reloadTime = 0.2f;
	public float offset = 0f;
	private float nextFire;

	// Use this for initialization
	void Start () {
		nextFire = Time.time + reloadTime + offset;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButton("Fire1")) {
			if (Time.time > nextFire){
				nextFire = Time.time + reloadTime;
				// anim.SetTrigger("Shoot");
				// audio.Play();
				GameObject clone = Object.Instantiate(bullet, canon.transform.position, canon.transform.rotation) as GameObject;
				clone.GetComponent<Rigidbody>().velocity = canon.transform.forward * bulletSpeed;
			}

		}
		lookat();


	}

	void lookat(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, 100)) {
			Debug.DrawRay (ray.origin, hit.point, Color.red);

			transform.LookAt (new Vector3(hit.point.x, this.transform.position.y, hit.point.z));
		}
	}
}
