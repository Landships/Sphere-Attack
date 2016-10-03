using UnityEngine;
using System.Collections;

public class CollisionManager : MonoBehaviour {
    void OnTriggerEnter(Collider col) {

        if (col.gameObject.tag == "Sphere") {
            Destroy(col.gameObject);

        }

    }
}
