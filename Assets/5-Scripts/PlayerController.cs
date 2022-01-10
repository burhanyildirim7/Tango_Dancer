using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private int _iyiToplanabilirDeger;

    [SerializeField] private int _kötüToplanabilirDeger;

    [SerializeField] private GameObject _karakterPaketi;

    [SerializeField] private Animator playerAnimator;

    [SerializeField] private GameObject pointLeft,pointRight,emojiPuke,emojiDrool;


    [SerializeField] private float _speedFast,_speedNormal;

    private int _elmasSayisi;

    private GameObject _player;

    private UIController _uiController;

    private int _toplananElmasSayisi;


    private void Awake()
    {
        emojiPuke.SetActive(false);
        emojiDrool.SetActive(false);
    }

    void Start()
    {
        LevelStart();

        _uiController = GameObject.Find("UIController").GetComponent<UIController>();

    }

    private void OnTriggerEnter(Collider other)
    {
  
        if (other.tag == "Elmas")
        {
            _elmasSayisi += 1;
            _toplananElmasSayisi += 1;
            PlayerPrefs.SetInt("ElmasSayisi", _elmasSayisi);
            Destroy(other.gameObject);
        }


        if (other.gameObject.tag == "GoodManLeft")
        {
            GoodManDance(pointLeft,other.gameObject);
            
        }else if(other.gameObject.tag == "GoodManRight")
        {
            GoodManDance(pointRight, other.gameObject);
        }
        if (other.gameObject.tag == "BadManLeft")
        {
            BadManMove(pointLeft);

        }
        else if (other.gameObject.tag == "BadManRight")
        {
            BadManMove(pointRight);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "BadMan")
        {
            emojiPuke.SetActive(true);

        }
    }

    private void GoodManDance(GameObject point, GameObject man)
    {
        //GameController._oyunAktif = false;

        man.transform.parent.parent = transform;
        playerAnimator.ResetTrigger("walk");
        

        playerAnimator.SetTrigger("dance");
       


        StartCoroutine(PlayerWalk(point, man));



    }

    private IEnumerator PlayerWalk(GameObject point, GameObject man)
    {
        
        yield return new WaitForSeconds(1);
        man.transform.parent.GetComponent<GoodManController>().leftCollider.enabled = false;
        man.transform.parent.GetComponent<GoodManController>().rightcollider.enabled = false;
        man.transform.parent.GetComponent<GoodManController>().baseCollider.enabled = false;
        man.transform.parent.parent = null;
        playerAnimator.ResetTrigger("dance");
        transform.DOMove(point.transform.position, 0.2f).OnComplete(PlayerWalkAnim);
        
    }

    public void PlayerWalkAnim()
    {
        playerAnimator.SetTrigger("walk");
        playerAnimator.SetFloat("run", 1);

        //GameController._oyunAktif = true;
    }


    public void BadManMove(GameObject point)
    {
        playerAnimator.ResetTrigger("walk");
        GameController._oyunAktif = false;
        transform.DOMove(point.transform.position, 0.2f).OnComplete(PlayerWalkFast);
      

    }

    public void PlayerWalkFast()
    {
       
        GameController._oyunAktif = true;
        playerAnimator.SetTrigger("walk");
        _karakterPaketi.GetComponent<KarakterPaketiMovement>()._speed = _speedFast;
        playerAnimator.SetFloat("run", 2);
        StartCoroutine(PlayerWalkNormal());
    }

    public IEnumerator PlayerWalkNormal()
    {
        yield return new WaitForSeconds(2);
        _karakterPaketi.GetComponent<KarakterPaketiMovement>()._speed = _speedNormal;
        playerAnimator.SetFloat("run",1);
    }

    private void WinScreenAc()
    {
        _uiController.WinScreenPanelOpen();
    }

    private void LoseScreenAc()
    {
        _uiController.LoseScreenPanelOpen();
    }


    public void LevelStart()
    {
        _toplananElmasSayisi = 1;
        _elmasSayisi = PlayerPrefs.GetInt("ElmasSayisi");
        _karakterPaketi.transform.position = new Vector3(0, 0, 0);
        _karakterPaketi.transform.rotation = Quaternion.Euler(0, 0, 0);
        _player = GameObject.FindWithTag("Player");
        _player.transform.localPosition = new Vector3(0, 1, 0);
    }
     
}
