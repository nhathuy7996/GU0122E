using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DrawController : MonoBehaviour
{
    List<Vector3> Listpos = new List<Vector3>();
    Vector2 oldPos = Vector2.zero;
    LineRenderer lineRenderer;
    Rigidbody2D rigidbody2;

    EdgeCollider2D edgeCollider2;

    

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        rigidbody2 = GetComponent<Rigidbody2D>();
        edgeCollider2 = GetComponent<EdgeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instant._GameState != GameController. GAME_STATE.init)
            return;

        if (Input.GetMouseButtonUp(0))
        { 
            StartGame();
        }

        if (!Input.GetMouseButton(0))
            return;
        GameController.instant.ChangeGameState(GameController.GAME_STATE.init);

         Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Vector2.Distance(oldPos, mousePos) < 0.5f)
            return;

        oldPos = mousePos;

        Listpos.Add(mousePos);

        lineRenderer.positionCount = Listpos.Count;
        lineRenderer.SetPositions(Listpos.ToArray());
    }


    void StartGame()
    {
        Vector2[] poss = new Vector2[Listpos.Count];
        for (int i = 0; i < Listpos.Count; i++)
        {
            poss[i] = Listpos[i];
        }
        edgeCollider2.points = poss;
        rigidbody2.simulated = true;

        GameController.instant.ChangeGameState(GameController.GAME_STATE.play);

      

    }

}
