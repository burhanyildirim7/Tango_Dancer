using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodManController : MonoBehaviour
{

    [SerializeField] private Animator manAnimator;

    public Collider leftCollider, rightcollider,baseCollider;




    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            manAnimator.SetTrigger("dance");

            StartCoroutine(ManIdleCorotine());
        }
    }

    private IEnumerator ManIdleCorotine()
    {
        yield return new WaitForSeconds(1);
        manAnimator.SetTrigger("idle");
    }
}
