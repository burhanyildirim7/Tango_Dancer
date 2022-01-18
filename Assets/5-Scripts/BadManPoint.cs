using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadManPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.gameObject.SetActive(false);
        }
    }
}
