using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControllerBase : MonoBehaviour
{
    [SerializeField] Bulletbase _bulletPrefab;
    PlayerController _controller;

    [SerializeField] GunData _gunData;
    public GunDataTest _gunDataDemo;

    protected float _countTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        _controller = this.GetComponentInParent<PlayerController>();
        _countTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _countTime -= Time.deltaTime;
        if (Input.GetKey(KeyCode.C) && _countTime <= 0)
        {
            Fire();
      
        }
    }

    public virtual void Fire()
    {
        Bulletbase b = LazyPooling.Instance.getObj<Bulletbase>(_bulletPrefab.gameObject)
              .Init((int)_controller.transform.localScale.x, this._gunData);

        b.transform.position = this.transform.position;
        b.gameObject.SetActive(true);

        _countTime = _gunData._speedFire;
    }
}
