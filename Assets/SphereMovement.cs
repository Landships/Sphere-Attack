using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SphereMovement : NetworkBehaviour {

    public float minSpeed;
    public float maxSpeed;
    public GameObject center;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        Vector3 direction = center.transform.position - transform.position;
        float speed = Random.Range(minSpeed, maxSpeed);

        transform.position = transform.position + (direction * Time.deltaTime * speed);


    }


    void OnTriggerEnter(Collider col) {

        if (col.gameObject.tag == "Bullet") {
			CmdDestroy ();
            // Score += 1;
        }

    }

	[Command]
	void CmdDestroy() {
		Destroy(gameObject);
	}
}