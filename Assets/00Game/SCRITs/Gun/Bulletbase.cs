using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulletbase : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField] float _speed; 
    int _way = 1;
    float _lifeTime = 5;
    float _dmg = 0;

    SpriteRenderer _sprite;
    // Start is called before the first frame update
    void Awake()
    {
        _rb = this.GetComponent<Rigidbody2D>(); 
        _sprite = this.GetComponent<SpriteRenderer>();
    }

    public Bulletbase Init(int wway, GunData gunData)
    {
        this._way = wway;

        _dmg = gunData._dmgBullet;
        _lifeTime = gunData._bulletLifeTime;
        _sprite.flipY = wway == -1 ?false : true;
        return this;
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = Vector3.right * this._way * _speed;
        this._lifeTime -= Time.deltaTime;
        if(this._lifeTime <= 0)
            this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.gameObject.SetActive(false);
        IDamageAble damageAble = collision.GetComponent<IDamageAble>();

        if (damageAble == null)
            return;

        damageAble.GetHit(_dmg);
    }


}
