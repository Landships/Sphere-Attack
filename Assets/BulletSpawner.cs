using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BulletSpawner : NetworkBehaviour {

	//[Command]
	public void CmdFireBullet(GameObject bullet, float bulletSpeed, float bulletLife) 
	{
		GameObject bulletClone = Instantiate(bullet, bullet.transform.position, bullet.transform.rotation) as GameObject;
		bulletClone.SetActive(true);
		Rigidbody rb = bulletClone.GetComponent<Rigidbody>();
		rb.AddForce(-bullet.transform.forward * bulletSpeed);
		//NetworkServer.Spawn(bullet);
		Destroy(bulletClone, bulletLife);
	}
}
