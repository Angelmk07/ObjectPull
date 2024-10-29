using System.Collections.Generic;
using UnityEngine;
namespace PlayerSystem
{
    public class BulletPool
    {
        private Bullet _bullet;
        private Queue<Bullet> _bullets = new Queue<Bullet>();
        private int _startPoolsize ;
        private int _PoolExtraAdd = 5;
        public BulletPool(Bullet bullet,int startValue)
        {
            _startPoolsize = startValue;
            _bullet= bullet;
            initPool();
        }
        public BulletPool(Bullet bullet)
        {
            _bullet = bullet;
            initPool();
        }
        public Bullet GetItem()
        {
            return _bullets.Dequeue();
        }
        public void initPool()
        {

            for (int i = 0; i < _startPoolsize; i++)
            {
                Bullet bulletInst = Object.Instantiate(_bullet);
                _bullets.Enqueue(bulletInst);
                bulletInst.OnDisableBullet += ReturnToPull;
            }
        }
        void CreateExtra()
        {
            for (int i = 0; i <= _PoolExtraAdd; i++)
            {
                Bullet bulletInst = Object.Instantiate(_bullet);
                _bullets.Enqueue(bulletInst);
                bulletInst.OnDisableBullet += ReturnToPull;
            }
        }
        public bool TryGetItem(out Bullet bullet)
        {
            if(_bullets.TryDequeue(out bullet)) 
            { 
                return true;
            }
            
            CreateExtra();
            TryGetItem(out bullet);
            return true;


        }
            

        public void ReturnToPull(Bullet bullet)
        {
            _bullets.Enqueue(bullet);

            Debug.Log(_bullets.Count);
        }


        public void Unsubscribe()
        {

            foreach(Bullet bullet in _bullets)
            {
                bullet.OnDisableBullet -= ReturnToPull;
            }
        }
    }

}
