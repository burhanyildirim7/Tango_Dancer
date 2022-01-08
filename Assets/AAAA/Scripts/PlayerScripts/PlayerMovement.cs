//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[RequireComponent(typeof(PlayerInput))]
//public class PlayerMovement : MonoBehaviour
//{
   
//    [SerializeField] private float  xSpeed, limitX, runningSpeed;

//    private float newX;

//    public bool isPlayerMovement=true;

//    public event Action PlayerWinAnimEvent, PlayerRunAnimEvent;

//    void Awake()
//    {
//        GetComponent<PlayerInput>().TouchXDeltaMove += HandleTouchMove;
//        GameManager.instance.PlayerStartPositionEvent += PlayerstartPosition;
//        GameManager.instance.PlayerFinishMoveEvent += WinMovement;
   

//    }

 

//    private void HandleTouchMove(float touchXDelta)
//    {
//        newX = transform.position.x + xSpeed * touchXDelta * Time.deltaTime;
       
//    }

//    private void FixedUpdate()
//    {
//        if (isPlayerMovement)
//        {
//            Movement();
//        }
     
     
//    }

//    private void Movement()
//    {
//        newX = Mathf.Clamp(newX, -limitX, limitX);
//        Vector3 newPosition = new Vector3(newX, transform.position.y, transform.position.z + runningSpeed * Time.deltaTime);

//        transform.position = newPosition;
//    }

//   public  void PlayerstartPosition()
//    {
//        transform.position = new Vector3(0, 0.4f, 0);
//        newX = 0f;
//        isPlayerMovement = true;
//        transform.rotation = Quaternion.Euler(0, 0, 0);
//        PlayerRunAnimEvent();

//    }

//    public void WinMovement()
//    {
//        isPlayerMovement = false;
//        transform.rotation = Quaternion.Euler(0, 180f, 0);

//        PlayerWinAnimEvent();
     
//    }


  

//}
