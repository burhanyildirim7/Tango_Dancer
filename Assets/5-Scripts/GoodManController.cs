using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodManController : MonoBehaviour
{

    [SerializeField] private Animator manAnimator;
    [SerializeField] private GameObject emojiKiss, emojiCool;

    public Collider leftCollider, rightcollider,baseCollider;


    private void Awake()
    {
        emojiKiss.SetActive(true);
        emojiCool.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            manAnimator.SetTrigger("dance");
            emojiKiss.SetActive(false);
            emojiCool.SetActive(true);

            StartCoroutine(ManIdleCorotine());
        }
    }

    private IEnumerator ManIdleCorotine()
    {
        yield return new WaitForSeconds(1);
        manAnimator.SetTrigger("idle");
        emojiCool.SetActive(false);

        leftCollider.enabled = false;
        rightcollider.enabled = false;
        baseCollider.enabled = false;
        Destroy(transform.gameObject, 2);
    }


}
