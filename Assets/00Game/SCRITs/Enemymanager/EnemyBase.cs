using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamageAble
{
    float _HP = 5;
    [SerializeField] float _range = 10;
    [SerializeField] float _speed = 5;

    PlayerController _playerCtrl;
    Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _playerCtrl = FindAnyObjectByType<PlayerController>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(this.transform.position, _playerCtrl.transform.position) <= _range)
        {
            Debug.DrawLine(this.transform.position, _playerCtrl.transform.position,Color.red);
            Vector2 dir = _playerCtrl.transform.position - this.transform.position;
            _rb.velocity = dir.normalized * _speed;
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if bullet GetHit();
        //els
        //truy cap den Player.Gethit()
        // if obj.name = "Player"
        // Player.GetcomComponent<PlayerController>().gethit
    }

    void Atk()
    {
        
    }

    public void GetHit()
    {
          
    }

    public void GetHit(float dmg)
    {
        this._HP -= dmg;
        if (this._HP <= 0)
        {
            this._HP = 0;
            DataController.instant.Score++;
            Destroy(this.gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, _range);

     
    }
}
