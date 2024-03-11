using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GunData_", menuName ="Data/Gun")]
public class GunData : ScriptableObject
{
    public float _maxBullet,
        _speedBullet, 
        _speedFire,
        _dmgBullet, 
        _bulletLifeTime;
}

[System.Serializable]
public class GunDataTest
{
    public float _maxBullet,
       _speedBullet,
       _speedFire,
       _dmgBullet,
       _bulletLifeTime;
     public string   image;

    public string _gunController;

}
