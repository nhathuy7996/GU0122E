using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageAble
{

    Rigidbody2D _rigi;
    [SerializeField]
    AnimControllerbase _animCtrl;
    Vector2 _movement = Vector2.zero;
    [SerializeField] float _speed, _jumpForce;
    [SerializeField] bool _isOnGround = false;
    [SerializeField]
    PLAYER_STATE _state = PLAYER_STATE.IDLE;

    public PLAYER_STATE state => _state;

    public GunControllerBase _GunController;

    float _HP = 100;

    public enum PLAYER_STATE
    {
        IDLE = 0,
        JUMP = 2,
        RUN = 1,
        FALL = 3
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigi = GetComponent<Rigidbody2D>();
        _animCtrl = this.GetComponentInChildren<AnimControllerbase>();
    }

    // Update is called once per frame
    void Update()
    {

        UpdateState();
        _animCtrl.ChangeAnim(_state);

        _movement.x = _speed * Input.GetAxisRaw("Horizontal");
        _movement.y = this._rigi.velocity.y;

        this._rigi.velocity = _movement;
        if (_movement.x > 0)
            this.transform.localScale = Vector3.one;
        else if (_movement.x < 0)
            this.transform.localScale = new Vector3(-1,1,1);

        if (Input.GetKeyDown(KeyCode.Space) && _isOnGround)
        {
            _isOnGround = false;
            _rigi.AddForce(new Vector2(0,_jumpForce));
        }

        //_animCtrl.ChangeBlend("isShoot", Input.GetKeyDown(KeyCode.C)? 1: 0);
        if (Input.GetKey(KeyCode.C))
        {

            _animCtrl.ChangeBlend(CONSTANT.isShoot,1);
        }else
            _animCtrl.ChangeBlend(CONSTANT.isShoot, 0);
    }

    void UpdateState()
    {
        if(_isOnGround)
            _state = _movement.x != 0 ? PLAYER_STATE.RUN: PLAYER_STATE.IDLE;
        else
        {
            _state = _movement.y >= 0 ? PLAYER_STATE.JUMP : PLAYER_STATE.FALL;
        }
    }

    public void GetHit(float dmg)
    {
        this._HP -= dmg;
        if (this._HP <= 0)
            this._HP = 0;

        Observer.instant.NOtify("HP", _HP);
    }

    void Atk()
    {
        //EnemyBase.GetHit()
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if trap => player.GetHit()

        // if enemy => player.GetHit()

        //if .....
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isOnGround = true;
    }

    public void GetHit()
    {
        throw new System.NotImplementedException();
    }
}
