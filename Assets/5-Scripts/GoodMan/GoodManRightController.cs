using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodManRightController : MonoBehaviour
{
    [SerializeField] private Animator manAnimator;

    [SerializeField] private GameObject emojiKiss, emojiCool;
    [SerializeField] private GameObject baseGameObject;

    [SerializeField] private Collider leftCollider,rightCollider;


    public bool _danceAnim1, _danceAnim2, _danceAnim3;

    public string infoDance;

    private PlayerController playerController;

    private void Awake()
    {
        emojiKiss.SetActive(true);
        emojiCool.SetActive(false);
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        DanceInfo();
    }

    public string DanceInfo()
    {

        if (_danceAnim1)
            infoDance = "dance";
        else if (_danceAnim2)
            infoDance = "dance2";
        else if (_danceAnim3)
            infoDance = "dance3";
        else
            infoDance = "dance";

        return infoDance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print(DanceInfo());
            manAnimator.SetTrigger(DanceInfo());
            playerController.StartGoodmanActionRight(gameObject, DanceInfo());
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
        rightCollider.enabled = false;

        Destroy(baseGameObject, 2);
    }
}
