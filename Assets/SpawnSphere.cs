﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SpawnSphere : NetworkBehaviour {

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
            //CmdSpawnSphere ();
            SpawnS();
            timeLeft = spawnTime;

        } else {

            timeLeft -= Time.deltaTime;

        }
    }
    [Server]
    void SpawnS() {
        Vector3 spawn_loc = (Random.Range(minSpawnDistance, maxSpawnDistance) * Random.insideUnitSphere) + center.transform.position;
        while (spawn_loc.y < center.transform.position.y) {
            spawn_loc = Random.Range(minSpawnDistance, maxSpawnDistance) * Random.insideUnitSphere;
        }

        GameObject sphereClone = Instantiate(sphere, spawn_loc, sphere.transform.rotation) as GameObject;
        NetworkServer.Spawn(sphereClone);
        sphereClone.SetActive(true);
        //sphereClone.GetComponent<SpawnSphere> ().SetAc
        //Destroy(sphereClone, 1.0f);
    }

    [Command]
	void CmdSpawnSphere() {
		Vector3 spawn_loc = (Random.Range(minSpawnDistance, maxSpawnDistance) * Random.insideUnitSphere) + center.transform.position;
		while (spawn_loc.y < center.transform.position.y) {
			spawn_loc = Random.Range(minSpawnDistance, maxSpawnDistance) * Random.insideUnitSphere;
		}

		GameObject sphereClone = Instantiate(sphere, spawn_loc, sphere.transform.rotation) as GameObject;
		NetworkServer.Spawn(sphereClone);
		sphereClone.SetActive(true);
        //sphereClone.GetComponent<SpawnSphere> ().SetAc
        Destroy(sphereClone, 1.0f);
        Debug.Log("testing");
	}

    

}
