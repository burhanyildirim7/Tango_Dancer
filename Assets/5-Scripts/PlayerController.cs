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

    [SerializeField] private GameObject fearPoint;

    [SerializeField] private float _speedFast;

    private int _elmasSayisi;

    private GameObject _player;

    private UIController _uiController;

    private int _toplananElmasSayisi;

    


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
        else
        {

        }

        if (other.gameObject.tag == "Goodman")
        {

            GameController._oyunAktif = false;
            playerAnimator.SetTrigger("dance");

            StartCoroutine(PlayerWalkCorotine());

        }
        if(other.gameObject.tag == "Badman")
        {
       
            GameController._oyunAktif = false;
            playerAnimator.SetTrigger("fear");
            FearWalk();
            StartCoroutine(PlayerWalkCorotine());
            PlayerWalkFast();
        }


    }

    private void FearWalk()
    {

        transform.DOMove(fearPoint.transform.position, 2);

    }

    private IEnumerator PlayerWalkCorotine()
    {
        yield return new WaitForSeconds(2);
        PlayerWalkAnim();
        GameController._oyunAktif = true;

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
    



    public void PlayerWalkAnim()
    {
        playerAnimator.SetTrigger("walk");
    }

    public void PlayerWalkFast()
    {
        _karakterPaketi.GetComponent<KarakterPaketiMovement>()._speed = _speedFast;
    }
  
 
}
