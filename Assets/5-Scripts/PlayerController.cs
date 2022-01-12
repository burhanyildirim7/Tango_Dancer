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

    [SerializeField] private FinishLevel _finishLevel;

    [SerializeField] private Animator playerAnimator;

    [SerializeField] private GameObject pointLeft,pointRight,emojiPuke,emojiDrool;

    [SerializeField] private float _speedFast,_speedNormal;

    private int _elmasSayisi;

    private GameObject _player;

    private UIController _uiController;

    private bool isBadPoint, isGoodPoint;

    private int _levelSonuElmasSayisi;


    private void Awake()
    {
       
        emojiPuke.SetActive(false);
        emojiDrool.SetActive(false);

        FindObjectOfType<Health>().LoseGame += LoseScreenAc;

        _elmasSayisi = PlayerPrefs.GetInt("ElmasSayisi");

        
    }

    private void Update()
    {
        _finishLevel = GameObject.FindGameObjectWithTag("Finish").GetComponent<FinishLevel>();
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


        if (other.gameObject.tag == "GoodManLeft"&& isGoodPoint==false)
        {
            isGoodPoint = true;
            GoodManDance(pointLeft,other.gameObject);
            GoodManPoint();
            
        }
        else if(other.gameObject.tag == "GoodManRight" && isGoodPoint==false)
        {
            isGoodPoint = true;
            GoodManDance(pointRight, other.gameObject);
            GoodManPoint();
            
        }
        if (other.gameObject.tag == "BadManLeft" && isBadPoint==false)
        {
            isBadPoint = true;
            BadManMove(pointLeft);
            BadManPoint();
           
        }
        else if (other.gameObject.tag == "BadManRight" && isBadPoint==false)
        {
            isBadPoint = true;
            BadManMove(pointRight);
            BadManPoint();
          
        }

        //if (other.gameObject.tag == "GoodMan")
        //{
        //    emojiDrool.SetActive(true);
        //    GetComponentInChildren<Health>().ModifyHealth(10);

        //    _elmasSayisi += 10;
        //    _levelSonuElmasSayisi += 10;
        //    PlayerPrefs.SetInt("ElmasSayisi", _elmasSayisi);

            
        //}

        if (other.gameObject.tag == "Obstacle")
        {
        
            GetComponentInChildren<Health>().ModifyHealth(-10);
          
            playerAnimator.SetTrigger("stumble");
            StartCoroutine(PlayerStumbleWalk());
            
        }

        //if (other.gameObject.tag == "BadMan")
        //{
        //    emojiPuke.SetActive(true);
        //    GetComponentInChildren<Health>().ModifyHealth(-10);
        //}

        if (other.gameObject.tag == "Finish")
        {
            playerAnimator.ResetTrigger("walk");
            playerAnimator.SetTrigger("idle");
            GameController._oyunAktif = false;
            int gidilecekFinishX = (GetComponentInChildren<Health>().currentHealth/10);
            print(gidilecekFinishX);
            if(gidilecekFinishX!=0)
                _levelSonuElmasSayisi = _levelSonuElmasSayisi * gidilecekFinishX;
            

            _uiController.LevelSonuElmasSayisi(_levelSonuElmasSayisi);

            if (gidilecekFinishX > 10)
                gidilecekFinishX = 10;
            transform.DOMoveZ(_finishLevel._xFinish[gidilecekFinishX].transform.position.z, gidilecekFinishX);
            transform.DORotate(new Vector3(0,360*gidilecekFinishX,0), gidilecekFinishX,RotateMode.FastBeyond360).OnComplete(PlayerWinAnim);


            int sayac = (GetComponentInChildren<Health>().currentHealth / 10);
            
        
                StartCoroutine(PlayerHealtBarAzaltma(sayac));


        }
    }

    private void BadManPoint()
    {
        
        emojiPuke.SetActive(true);
        GetComponentInChildren<Health>().ModifyHealth(-10);
        isBadPoint = false;
    }

    private void GoodManPoint()
    {
        
        emojiDrool.SetActive(true);
        GetComponentInChildren<Health>().ModifyHealth(10);
        isGoodPoint = false;
        _elmasSayisi += 10;
        _levelSonuElmasSayisi += 10;
        PlayerPrefs.SetInt("ElmasSayisi", _elmasSayisi);
        
    }

    private IEnumerator PlayerHealtBarAzaltma(int sayac)
    {
        while (sayac > 0)
        {
            yield return new WaitForSeconds(0.5f);
            GetComponentInChildren<Health>().ModifyHealth(-10);
            sayac--;
        }
        PlayerHealtBarAzaltma(sayac);
    }



    private void PlayerWinAnim()
    {
        transform.DORotate(new Vector3(0, 180, 0), 0.5f);
        playerAnimator.ResetTrigger("idle");
        playerAnimator.SetTrigger("windance");
        StartCoroutine(WinScreenAc());
    }


    //OBSTACLE//
    private IEnumerator PlayerStumbleWalk()
    {
        yield return new WaitForSeconds(1);
        playerAnimator.ResetTrigger("stumble");
        playerAnimator.SetTrigger("walk");

    }
    //OBSTACLE//


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
        //GameController._oyunAktif = false;
        playerAnimator.SetTrigger("defeat");
        transform.DOMove(point.transform.position, 0.2f).OnComplete(PlayerWalkFast);
        

    }

    public void PlayerWalkFast()
    {
        
        //GameController._oyunAktif = true;
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
    private IEnumerator WinScreenAc()
    {
        yield return new WaitForSeconds(5);
        _uiController.WinScreenPanelOpen();
        playerAnimator.ResetTrigger("windance");
        playerAnimator.SetTrigger("idle");
        transform.rotation = Quaternion.Euler(0, 0, 0);

    }
    //LOSE
    private void LoseScreenAc()
    {
        gameObject.GetComponentInChildren<Animator>().Rebind();

        _uiController.LevelSonuElmasSayisi(_levelSonuElmasSayisi);

        //playerAnimator.ResetTrigger("walk");
        //playerAnimator.ResetTrigger("defeat");
        playerAnimator.ResetTrigger("stumble");
        //playerAnimator.ResetTrigger("dance");
        playerAnimator.SetTrigger("idle");

        _uiController.LoseScreenPanelOpen();


        StartCoroutine(StopGame());

        _levelSonuElmasSayisi = 0;



    }

    private IEnumerator StopGame()
    {
        yield return new WaitForSeconds(0.3f);
        GameController._oyunAktif = false;
    }
    //LOSE
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
