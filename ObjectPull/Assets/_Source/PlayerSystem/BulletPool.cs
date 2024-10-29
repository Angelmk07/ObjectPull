using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PlayerSystem
{
    public class BulletPool
    {
        private Queue<Bullet> _bullets = new Queue<Bullet>();
        private const int _startPoolsize = 10;
        public BulletPool(Bullet bullet)
        {
            initPool(bullet);
        }

        public Bullet GetItem()
        {
            return _bullets.Dequeue();
        }
        public void initPool(Bullet bulletpref)
        {

            for (int i = 0; i < _startPoolsize; i++)
            {
                Bullet bulletInst = Object.Instantiate(bulletpref);
                _bullets.Enqueue(bulletInst);
                bulletInst.OnDisableBullet += ReturnToPull;
            }
        }

        public bool TryGetItem(out Bullet bullet) =>
            _bullets.TryDequeue(out bullet);

        public void ReturnToPull(Bullet bullet) =>
            _bullets.Enqueue(bullet);

        public void Unsubscribe()
        {

            foreach(Bullet bullet in _bullets)
            {
                bullet.OnDisableBullet -= ReturnToPull;
            }
        }
    }

}
