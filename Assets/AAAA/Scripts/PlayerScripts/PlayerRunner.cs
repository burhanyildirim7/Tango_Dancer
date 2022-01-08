//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//public class PlayerRunner : MonoBehaviour
//{
    
//    private void Awake()
//    {
       
//        FindObjectOfType<PlayerAnimation>().PlayerRunnerAnimEvent += PlayerRunnerWinAnim;
//        GameManager.instance.PlayerRunnerDestroyEvent += DestroyRunner;
//    }

//    private void PlayerRunnerWinAnim()
//    {


//        GameObject[] runnerGameObject = GameObject.FindGameObjectsWithTag("PlayerRunner");


//        foreach (var item in runnerGameObject)
//        {
//            item.GetComponent<Animator>().SetTrigger("Win");
//        }
        
//    }

//    public void DestroyRunner()
//    {
//        GameObject[] runnerGameObject = GameObject.FindGameObjectsWithTag("PlayerRunner");
//        foreach (var item in runnerGameObject)
//        {
//            Destroy(item);
//        }
//    }
//}
