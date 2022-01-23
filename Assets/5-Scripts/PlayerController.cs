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

    [SerializeField] private GameObject _karakterPaketi,_healtBar;

    [SerializeField] private FinishLevel _finishLevel;

    [SerializeField] private Animator playerAnimator;

    [SerializeField] private GameObject pointLeft, pointRight, emojiPuke, emojiDrool, confetti;


    [SerializeField] private float _speedFast, _speedNormal;


    private int _elmasSayisi;

    private GameObject _player;

    private UIController _uiController;



    private int _levelSonuElmasSayisi;


    private void Awake()
    {

        emojiPuke.SetActive(false);
        emojiDrool.SetActive(false);

        GetComponentInChildren<Health>().LoseGame += DeathPlayer;

        _elmasSayisi = PlayerPrefs.GetInt("ElmasSayisi");


    }

    private void Update()
    {
        _finishLevel = GameObject.Find("Finish").GetComponent<FinishLevel>();
    }

    void Start()
    {
        //LevelStart();

        _uiController = GameObject.Find("UIController").GetComponent<UIController>();


    }

    private void OnTriggerEnter(Collider other)
    {

      
        if (other.gameObject.tag == "BadManLeft")
        {
            if (GetComponentInChildren<Health>().currentHealth >= 0)
                BadManMove(pointLeft);
            
        }
        else if (other.gameObject.tag == "BadManRight")
        {
            if (GetComponentInChildren<Health>().currentHealth >= 0)
                BadManMove(pointRight);
            
        }

        if (other.gameObject.tag == "GoodManPoint")
        {
            GoodManPoint();

        }

        if (other.gameObject.tag == "BadManPoint")
        {
            BadManPoint();

        }



        if (other.gameObject.tag == "Obstacle")
        {

            
            if (GetComponentInChildren<Health>().currentHealth >= 0)
            {
                GetComponentInChildren<Health>().ModifyHealth(-10);
                playerAnimator.SetTrigger("stumble");

               StartCoroutine( PlayerStumbleWalk());
                
            }
            

        }

        if (other.gameObject.tag == "SetPlayerCenter")
        {

            transform.DOMoveX(0, 0.5f);
        }

        if (other.gameObject.tag == "Finish")
        {
            GameController._oyunAktif = false;
            playerAnimator.ResetTrigger("walk");
            //playerAnimator.SetTrigger("idle");
            playerAnimator.SetTrigger("pose");
            _healtBar.GetComponent<Canvas>().enabled = false;

            int gidilecekFinishX = (GetComponentInChildren<Health>().currentHealth / 10);
            print(gidilecekFinishX);
            if (gidilecekFinishX != 0)
                _levelSonuElmasSayisi = _levelSonuElmasSayisi * gidilecekFinishX;


            _uiController.LevelSonuElmasSayisi(_levelSonuElmasSayisi);

            if (gidilecekFinishX > 9)
                gidilecekFinishX = 9;

            transform.DOMoveZ(_finishLevel._xFinish[gidilecekFinishX].transform.position.z, gidilecekFinishX);
            if (gidilecekFinishX == 0)
                gidilecekFinishX = 1;

            transform.DORotate(new Vector3(0, 360 * gidilecekFinishX, 0), gidilecekFinishX, RotateMode.FastBeyond360).OnComplete(PlayerWinAnim);

            confetti.GetComponent<ParticleSystem>().Play();

            int sayac = (GetComponentInChildren<Health>().currentHealth / 10);


            StartCoroutine(PlayerHealtBarAzaltma(sayac));


        }
    }

    private void BadManPoint()
    {

        emojiPuke.SetActive(true);
        GetComponentInChildren<Health>().ModifyHealth(-10);

    }

    private void GoodManPoint()
    {

        emojiDrool.SetActive(true);
        GetComponentInChildren<Health>().ModifyHealth(10);

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
        playerAnimator.ResetTrigger("pose");
        playerAnimator.SetTrigger("windance");
        confetti.GetComponent<ParticleSystem>().Stop();
        StartCoroutine(WinScreenAc());

    }





    //OBSTACLE//
    private IEnumerator PlayerStumbleWalk()
    {
        yield return new WaitForSeconds(0.5f);
            
            if (GetComponentInChildren<Health>().currentHealth >= 0)
        {
            playerAnimator.ResetTrigger("stumble");
            playerAnimator.SetTrigger("walk");
        }
      
            

    }
    //OBSTACLE//


    /// GOODMAN ////

    public void StartGoodmanActionLeft(GameObject man, string danceInfo)
    {

        GoodManDance(pointLeft, man, danceInfo);

    }

    public void StartGoodmanActionRight(GameObject man, string danceInfo)
    {

        GoodManDance(pointRight, man, danceInfo);

    }

    private void GoodManDance(GameObject point, GameObject man, string danceInfo)
    {

        man.transform.parent.parent = transform;
        playerAnimator.ResetTrigger("walk");


        playerAnimator.SetTrigger(danceInfo);

        StartCoroutine(PlayerWalk(point, man));
    }

    private IEnumerator PlayerWalk(GameObject point, GameObject man)
    {

        yield return new WaitForSeconds(1);
        man.transform.parent.parent = null;
        playerAnimator.ResetTrigger("dance");
        playerAnimator.ResetTrigger("dance2");
        playerAnimator.ResetTrigger("dance3");
        transform.DOMove(point.transform.position, 0.2f).OnComplete(PlayerWalkAnim);

    }

    public void ResetAllanim()
    {
        playerAnimator.ResetTrigger("walk");
        playerAnimator.ResetTrigger("idle");
        playerAnimator.ResetTrigger("defeat");
        playerAnimator.ResetTrigger("stumble");
        playerAnimator.ResetTrigger("victory");
        playerAnimator.ResetTrigger("pose");
        playerAnimator.ResetTrigger("death");
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
            playerAnimator.ResetTrigger("defeat");
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
        playerAnimator.SetFloat("run", 1);
    }
    /// BADMAN///
    private IEnumerator WinScreenAc()
    {
        yield return new WaitForSeconds(5);
        _uiController.WinScreenPanelOpen();
        //playerAnimator.ResetTrigger("windance");
        playerAnimator.SetTrigger("idle");
        transform.rotation = Quaternion.Euler(0, 0, 0);

    }
    //LOSE

    private void DeathPlayer()
    {
        StartCoroutine(StartDeath());
        
    }

    private IEnumerator StartDeath()
    {
        yield return new WaitForSeconds(0.5f);
        ResetAllanim();
        DOTween.KillAll();

        //gameObject.GetComponent<CapsuleCollider>().enabled = false;
        GameController._oyunAktif = false;
        //gameObject.GetComponentInChildren<Animator>().Rebind();


        playerAnimator.SetTrigger("death");
        StartCoroutine(LoseScreenAc());
    }

    private IEnumerator LoseScreenAc()
    {
        yield return new WaitForSeconds(4f);

        _uiController.LevelSonuElmasSayisi(_levelSonuElmasSayisi);

        //playerAnimator.ResetTrigger("walk");
        //playerAnimator.ResetTrigger("defeat");
        //playerAnimator.ResetTrigger("stumble");
        //playerAnimator.ResetTrigger("dance");
        //playerAnimator.SetTrigger("idle");

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
        ResetAllanim();
        playerAnimator.SetTrigger("idle");
        transform.GetComponent<CapsuleCollider>().enabled = true;
        _healtBar.GetComponent<Canvas>().enabled = true;
        _levelSonuElmasSayisi = 0;

        _elmasSayisi = PlayerPrefs.GetInt("ElmasSayisi");
        _karakterPaketi.transform.position = new Vector3(0, 0, 0);
        _karakterPaketi.transform.rotation = Quaternion.Euler(0, 0, 0);
        _player = GameObject.FindWithTag("Player");
        _player.transform.localPosition = new Vector3(0, 0.55f, 0);


    }

    public void TabToStart()
    {

        playerAnimator.ResetTrigger("idle");
        playerAnimator.SetTrigger("walk");

    }


}
