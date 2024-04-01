using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DemoMobileInput : MonoBehaviour
{
    Vector2[] originPos = new Vector2[2];
    float distance = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        Touch[] touches = Input.touches;

        if (touches.Count() != 2)
            return;
         

        for (int i =0; i< touches.Count(); i++)
        { 
            if (touches[i].phase == TouchPhase.Began)
            {
                originPos[i] = touches[i].position;
            }
        }

        //Distant
        distance = Vector2.Distance(originPos[0], originPos[1]);

        foreach (Touch touch in touches)
        {
            if (touch.phase != TouchPhase.Moved)
                return;
        }


        //Recalculate Distance

        float newDistance = Vector2.Distance(touches[0].position, touches[1].position);

        Camera.main.orthographicSize = 5 / distance * newDistance;

        //Apply Cam size
    }
}
