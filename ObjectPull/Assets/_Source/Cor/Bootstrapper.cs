using InputSystem;
using PlayerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private Bullet bulletpref;
        [SerializeField] private PlayerShoot playerShoot;
        [SerializeField] private InputListener inputListener;
        [SerializeField] private int StartValBullets;
        private BulletPool _bulletPool;
        void Start()
        {
            inputListener.Construct(playerShoot);
            _bulletPool = new(bulletpref,StartValBullets);
            playerShoot.Construct(_bulletPool);
        }
        public void End()
        {
            _bulletPool.Unsubscribe();
        }
    }

}
