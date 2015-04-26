using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

	public GameObject explosion;
	public float longevity = 10f;
	private float borntime;
	private bool deflected = false;

	void Start () {
		borntime = Time.time;
	}
	

	void Update () {
		if (Time.time > borntime + longevity) {
			explode();
		}
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "enemy"){
			if (deflected == true) {
				explode();
			}
		} else if (other.gameObject.tag == "player") {
			//bounce back
			deflected = true;
			Debug.Log("Bullet deflected");
		} else {
			//destroy
			Debug.Log("something hit");
			Invoke("explode", 0.1f);
		}
	}

	void explode() {
		Object.Instantiate(explosion, this.transform.position, Quaternion.identity);
		Destroy(gameObject, 0.1f);
	}
}
