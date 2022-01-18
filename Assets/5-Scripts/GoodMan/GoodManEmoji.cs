using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodManEmoji : MonoBehaviour
{
    [SerializeField] private GameObject _emoji;


    private void Awake()
    {
        _emoji.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _emoji.SetActive(true);
        }
    }
}
