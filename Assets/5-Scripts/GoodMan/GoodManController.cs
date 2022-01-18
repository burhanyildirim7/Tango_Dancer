using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodManController : MonoBehaviour
{

    [SerializeField] private Animator manAnimator;
    [SerializeField] private GameObject emojiKiss, emojiCool;
    [SerializeField] private GameObject baseGameObject;

    public Collider leftCollider, rightcollider;

    

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
        yield return new WaitForSeconds(0.5f);
        manAnimator.SetTrigger("idle");
        emojiCool.SetActive(false);

        leftCollider.enabled = false;
        rightcollider.enabled = false;

        Destroy(baseGameObject, 2);
    }


}
