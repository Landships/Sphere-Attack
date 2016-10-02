using UnityEngine;
using System.Collections;

public class CollisionManager : MonoBehaviour {

    void OnTriggerEnter(Collider col) {

        if (col.gameObject.tag == "Sphere") {
            Debug.Log("hit");
            Destroy(col.gameObject);

        }

    }
}
