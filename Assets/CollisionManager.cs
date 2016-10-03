using UnityEngine;
using System.Collections;


public class CollisionManager : MonoBehaviour {
    public int score = 0;
    
    void OnTriggerEnter(Collider col) {

        if (col.gameObject.tag == "Sphere") {
            Destroy(col.gameObject);
            
            

        }
    }

}
