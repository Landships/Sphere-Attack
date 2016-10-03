using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SphereMovement : NetworkBehaviour {

    public float minSpeed;
    public float maxSpeed;
    public GameObject center;
    public Text scoreText;
    private static int score;

    // Use this for initialization
    void Start() {
        center = GameObject.FindGameObjectWithTag("Center");
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
            score += 1;
            scoreText.text = "Score: " + score;
        }

    }

	[Server]
	public void CmdDestroy() {
        RpcClientDestroy();
		Destroy(gameObject);
        Debug.Log("spheremovement");
	}

    [ClientRpc]
    void RpcClientDestroy() {
        Destroy(gameObject);
    }
}