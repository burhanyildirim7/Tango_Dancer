using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadManController : MonoBehaviour
{
    [SerializeField] private GameObject emojiKiss,emojiCry;

    public Collider leftCollider, rightcollider, baseCollider;
    private void Awake()
    {
        emojiKiss.SetActive(true);
        emojiCry.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            emojiKiss.SetActive(false);
            emojiCry.SetActive(true);
        }
        //StartCoroutine(BadManCorotine());
    }

    //private IEnumerator BadManCorotine()
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    leftCollider.enabled = false;
    //    rightcollider.enabled = false;
    //    baseCollider.enabled = false;


    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if(other.gameObject.tag == "Player")
    //    {
    //        emojiKiss.SetActive(false);
    //        emojiCry.SetActive(true);
    //    }
    //}
}
