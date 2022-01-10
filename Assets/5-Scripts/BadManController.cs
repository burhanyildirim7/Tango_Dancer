using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadManController : MonoBehaviour
{
    [SerializeField] private GameObject emojiKiss,emojiCry;


    private void Awake()
    {
        emojiKiss.SetActive(true);
        emojiCry.SetActive(false);
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            emojiKiss.SetActive(false);
            emojiCry.SetActive(true);
        }
    }
}
