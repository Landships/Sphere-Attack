using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CollisionManager : NetworkBehaviour {
    public int score = 0;

    void OnTriggerEnter(Collider col) {

        if (col.gameObject.tag == "Sphere") {
            //CmdDestroy(col.gameObject);
            col.gameObject.GetComponent<SphereMovement>().CmdDestroy();
        }
    }

    [Server]
	public void CmdDestroy(GameObject sphere) {
        RpcClientDestroy();

        //gameObject.GetComponent<SphereMovement>().enabled = false;
		Destroy(gameObject);

        Debug.Log("CollisionManager");
	}

    [ClientRpc]
    void RpcClientDestroy() {
        gameObject.GetComponent<SphereMovement>().enabled = false;
    }
}
