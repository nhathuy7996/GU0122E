using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour
{
    Transform target;
    Rigidbody2D rb;
    [SerializeField] float _force;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void OnEnabled()
    {
        target = GameController.instant.player;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            target = GameController.instant.player;

        Vector2 dir = target.position - this.transform.position;
        dir = dir.normalized;

        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg - 90;

        Quaternion q = new Quaternion();
        q.eulerAngles = new Vector3(0,0,angle);
        this.transform.rotation = q;

        if (rb.velocity.sqrMagnitude > 100)
            return;
        rb.AddForce(dir* _force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Dog"))
        {
            GameController.instant.ChangeGameState(GameController.GAME_STATE.over);
        }
    }
}
