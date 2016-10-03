using UnityEngine;
using System.Collections;

public class SpawnSphere : MonoBehaviour {

    public GameObject center;

    public GameObject sphere;

    public int minSpawnDistance = 30;
    public int maxSpawnDistance = 50;
    public float spawnTime = .5f;

    private float timeLeft;



	// Use this for initialization
	void Start () {

        timeLeft = spawnTime;
	
	}
	
	// Update is called once per frame
	void Update () {

        if (timeLeft <= 0) {

            Vector3 spawn_loc = (Random.Range(minSpawnDistance, maxSpawnDistance) * Random.insideUnitSphere) + center.transform.position;
            while (spawn_loc.y < center.transform.position.y) {
                spawn_loc = Random.Range(minSpawnDistance, maxSpawnDistance) * Random.insideUnitSphere;
            }

            GameObject sphereClone = Instantiate(sphere, spawn_loc, sphere.transform.rotation) as GameObject;
            sphereClone.SetActive(true);
            //sphereClone.GetComponent<SpawnSphere> ().SetAc

            timeLeft = spawnTime;

        } else {

            timeLeft -= Time.deltaTime;

        }

        

        

    }

    

}
