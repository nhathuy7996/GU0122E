using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    LineRenderer lineRenderer;
    [SerializeField] float _speedRotate;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion q = this.transform.rotation;
        float z = q.eulerAngles.z + _speedRotate * Time.deltaTime;
        q.eulerAngles = new Vector3(0,0,z) ;
        this.transform.rotation = q ;

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, this.transform.position);
        float angle = (this.transform.rotation.eulerAngles.z + 90) * Mathf.Deg2Rad;

        Vector2 dir = new Vector2(Mathf.Cos(angle),Mathf.Sin(angle));

        Debug.DrawLine(this.transform.position, dir);

        RaycastHit2D hit2D = Physics2D.Raycast(this.transform.position, dir, 1000);

        if (hit2D.collider == null)
        {
            lineRenderer.SetPosition(1, dir * 1000);
            return;
        }


        lineRenderer.SetPosition(1, hit2D.point);
    }
}
