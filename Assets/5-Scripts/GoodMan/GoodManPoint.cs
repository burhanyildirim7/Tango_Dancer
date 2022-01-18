using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodManPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.gameObject.SetActive(false);
        }
    }
}
