using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    

    public float TouchXDelta { get; private set; }

    public event Action<float> TouchXDeltaMove;



    void Update()
    {



        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {

            TouchXDelta = Input.GetTouch(0).deltaPosition.x;
            TouchXDeltaMove(TouchXDelta);
        }
        else if (Input.GetMouseButton(0))
        {
            TouchXDelta = Input.GetAxis("Mouse X");
            TouchXDeltaMove(TouchXDelta);
        }



       
    }
}
