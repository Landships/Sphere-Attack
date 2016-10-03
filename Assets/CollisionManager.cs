using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CollisionManager : MonoBehaviour {
    public int score = 0;

    void OnTriggerEnter(Collider col) {

        if (col.gameObject.tag == "Sphere") {
			CmdDestroy ();
        }
    }
	[Command]
	void CmdDestroy() {
		Destroy(gameObject);
	}
}
