//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerAnimation : MonoBehaviour
//{
//    private Animator anim;
//    public event Action PlayerRunnerAnimEvent;
//    private void Awake()
//    {
//        anim = transform.GetComponent<Animator>();
//        GetComponent<PlayerMovement>().PlayerWinAnimEvent += PlayerWinAnim;
//        GetComponent<PlayerMovement>().PlayerRunAnimEvent += PlayerRunAnim;
//    }

//    private void PlayerWinAnim()
//    {
//        anim.ResetTrigger("Run");
//        anim.SetTrigger("Win");
//        PlayerRunnerAnimEvent();
        
//    }

//    private void PlayerRunAnim()
//    {
//        anim.ResetTrigger("Win");
//        anim.SetTrigger("Run");
//    }
//}
