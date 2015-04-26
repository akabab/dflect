using UnityEngine;
using System.Collections;

public class foesMove : MonoBehaviour {
    public Transform target;
    private NavMeshAgent agent;
    private float lastSeen = 3f;
    private bool hasTargetInRange = false;
    private bool isInCollide = false;

    // Use this for initialization
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        // agent.updatePosition = true;
        // agent.updateRotation = true;
    }

    void Reach(Vector3 dest, float stopDist = 0f) {
        agent.Resume();
        if (agent.destination == dest) { return; }

        agent.stoppingDistance = stopDist;
        agent.SetDestination(dest);
    }

    void follow(Vector3 pos) {
        // Vector3.Distance(this.transform.position, pos) > 3f
        if (!hasTargetInRange) {
            // print(Vector3.Distance(this.transform.position, pos) + " " + this.tag + " - " + this.gameObject.name);
            Reach(pos, 3f);
        }
    }
    // void OnTriggerEnter(Collider other) {
    //     if (other.gameObject.tag == this.tag && other.attachedRigidbody) {
    //         // Debug.Log("it's not okay now " + other.gameObject.name);
    //     }
    // }

    // void OnTriggerStay(Collider other) {
    //     if (other.gameObject.tag == this.tag && other.attachedRigidbody) {
    //         // GetComponent<NavMeshAgent>();
    //         // Vector3 direction = other.transform.position - this.transform.position;
    //         // direction.Normalize();
    //         other.attachedRigidbody.AddForce((other.transform.position - this.transform.position) * 2);
    //         isInCollide = true;
    //     }
    // }

    // void OnTriggerExit(Collider other) {
    //     // remove force when outside of the range
    //     if (other.gameObject.tag == this.tag && other.attachedRigidbody) {
    //         other.attachedRigidbody.velocity = Vector3.zero;
    //         other.attachedRigidbody.angularVelocity = Vector3.zero;
    //         isInCollide = false;
    //     }
    // }

    // Update is called once per frame
    void Update () {
        NavMeshHit hit;
        if (agent.Raycast(target.position, out hit)) {
            lastSeen += Time.deltaTime;
            hasTargetInRange = false;
            if (lastSeen < 0.5f) {
                Reach(target.position, 0f);
            }
        } else {
            hasTargetInRange = true;
            GameObject[] enemyArray = GameObject.FindGameObjectsWithTag(this.tag);
            foreach (GameObject enemy in enemyArray) {
                foesMove enemyScript = enemy.GetComponent<foesMove>();
                if (enemyScript) {
                    enemyScript.follow(this.transform.position);
                }
            }
            Reach(target.position, 3f);
            lastSeen = 0f;
        }
        if (hasTargetInRange) {
            // GetComponent<Renderer>().material.color = Color.yellow;
        } else {
            // GetComponent<Renderer>().material.color = Color.black;
        }
        if (agent.remainingDistance < 3f) {
            agent.Stop();
            if (hasTargetInRange) {
                this.transform.LookAt(target);
            }
        } else {
            Debug.DrawLine(this.transform.position, agent.destination);
        }
    }
}
