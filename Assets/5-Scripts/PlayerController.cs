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

    

    private int _levelSonuElmasSayisi;


    private void Awake()
    {
       
        emojiPuke.SetActive(false);
        emojiDrool.SetActive(false);

        FindObjectOfType<Health>().LoseGame += LoseScreenAc;
        
    }

    void Start()
    {
        //LevelStart();

        _uiController = GameObject.Find("UIController").GetComponent<UIController>();

        
    }

    private void OnTriggerEnter(Collider other)
    {
  
        //if (other.tag == "Elmas")
        //{
        //    _elmasSayisi += 1;
        //    _toplananElmasSayisi += 1;
        //    PlayerPrefs.SetInt("ElmasSayisi", _elmasSayisi);
        //    Destroy(other.gameObject);
        //}


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

        if (other.gameObject.tag == "GoodMan")
        {
            emojiDrool.SetActive(true);
            GetComponentInChildren<Health>().ModifyHealth(10);

            _elmasSayisi += 10;
            _levelSonuElmasSayisi += 10;
            PlayerPrefs.SetInt("ElmasSayisi", _elmasSayisi);

            
        }

        if (other.gameObject.tag == "Obstacle")
        {
        
            GetComponentInChildren<Health>().ModifyHealth(-10);
          
            playerAnimator.SetTrigger("stumble");
            StartCoroutine(PlayerStumbleWalk());
            
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "BadMan")
        {
            emojiPuke.SetActive(true);
            GetComponentInChildren<Health>().ModifyHealth(-10);
        }

    }

    //Obstacle//
    private IEnumerator PlayerStumbleWalk()
    {
        yield return new WaitForSeconds(1);
        playerAnimator.ResetTrigger("stumble");
        playerAnimator.SetTrigger("walk");

    }
    //Obstacle//


    /// GOODMAN ////

    private void GoodManDance(GameObject point, GameObject man)
    {
    
        man.transform.parent.parent = transform;
        playerAnimator.ResetTrigger("walk");


        playerAnimator.SetTrigger("dance");    

        StartCoroutine(PlayerWalk(point, man));
    }

    private IEnumerator PlayerWalk(GameObject point, GameObject man)
    {
        
        yield return new WaitForSeconds(1);
        man.transform.parent.parent = null;
        playerAnimator.ResetTrigger("dance");
        transform.DOMove(point.transform.position, 0.2f).OnComplete(PlayerWalkAnim);
        
    }

    public void PlayerWalkAnim()
    {
        playerAnimator.SetTrigger("walk");
        playerAnimator.SetFloat("run", 1);
        emojiDrool.SetActive(false);

    }
    /// GOODMAN ////


    /// BADMAN///

    public void BadManMove(GameObject point)
    {
        playerAnimator.ResetTrigger("walk");
        GameController._oyunAktif = false;
        playerAnimator.SetTrigger("defeat");
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
        
       
        yield return new WaitForSeconds(1);
         emojiPuke.SetActive(false);
        _karakterPaketi.GetComponent<KarakterPaketiMovement>()._speed = _speedNormal;
        playerAnimator.SetFloat("run",1);
    }
    /// BADMAN///
    private void WinScreenAc()
    {
        _uiController.WinScreenPanelOpen();
    }

    private void LoseScreenAc()
    {
        gameObject.GetComponentInChildren<Animator>().Rebind();

        _uiController.LevelSonuElmasSayisi(_levelSonuElmasSayisi);
    
      
        _uiController.LoseScreenPanelOpen();

        playerAnimator.ResetTrigger("walk");
        playerAnimator.ResetTrigger("defeat");
        playerAnimator.ResetTrigger("stumble");
        playerAnimator.ResetTrigger("dance");
        playerAnimator.SetTrigger("idle");

        StartCoroutine(StopGame());
    }

    private IEnumerator StopGame()
    {
        yield return new WaitForSeconds(0.3f);
        GameController._oyunAktif = false;
    }

    public void LevelStart()
    {
      
        
        _levelSonuElmasSayisi = 0;
    
        _elmasSayisi = PlayerPrefs.GetInt("ElmasSayisi");
        _karakterPaketi.transform.position = new Vector3(0, 0, 0);
        _karakterPaketi.transform.rotation = Quaternion.Euler(0, 0, 0);
        _player = GameObject.FindWithTag("Player");
        _player.transform.localPosition = new Vector3(0, 1, 0);
   
       
    }

    public void TabToStart()
    {
        playerAnimator.ResetTrigger("idle");
        playerAnimator.SetTrigger("walk");
      
    }


}
