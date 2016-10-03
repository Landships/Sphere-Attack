namespace VRTK.Examples
{
    using UnityEngine;
	using UnityEngine.Networking;

	public class Gun : VRTK_InteractableObject, NetworkBehaviour
    {
        private GameObject bullet;
        private float bulletSpeed = 1000f;
        private float bulletLife = 5f;

        public override void StartUsing(GameObject usingObject)
        {
            base.StartUsing(usingObject);
            CmdFireBullet();
        }

        protected override void Start()
        {
            base.Start();
            bullet = transform.Find("Bullet").gameObject;
            bullet.SetActive(false);
        }
			
		[Command]
		private void CmdFireBullet() 
		{
			GameObject bulletClone = Instantiate(bullet, bullet.transform.position, bullet.transform.rotation) as GameObject;
			bulletClone.SetActive(true);
			Rigidbody rb = bulletClone.GetComponent<Rigidbody>();
			rb.AddForce(-bullet.transform.forward * bulletSpeed);
			NetworkServer.Spawn(bullet);
			Destroy(bulletClone, bulletLife);
		}
    }
}