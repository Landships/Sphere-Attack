namespace VRTK.Examples
{
    using UnityEngine;


	public class Gun : VRTK_InteractableObject
    {
        public GameObject bullet;
        private float bulletSpeed = 1000f;
        private float bulletLife = 5f;
		public BulletSpawner spawner;

        public override void StartUsing(GameObject usingObject)
        {
            base.StartUsing(usingObject);
			spawner.CmdFireBullet(bullet, bulletSpeed, bulletLife);
        }

        protected override void Start()
        {
            base.Start();
            bullet = transform.Find("Bullet").gameObject;
            bullet.SetActive(false);
        }
   }
}