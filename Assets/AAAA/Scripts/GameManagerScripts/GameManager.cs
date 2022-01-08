//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class GameManager : MonoBehaviour
//{
//    public static GameManager instance;

//    public Text scoreText, levelText, levelScoreText;

//    public int score, levelScore, totalLevelScore;

//    public bool isLevelFinish, isSwipeToStart;

//    public GameObject gameoverScreen, gameStartScreen, gameNextLevelScreen;

//    public LevelController levelController;

//    public Button gameNextLevelButton;

//    public event Action PlayerStartPositionEvent, PlayerFinishMoveEvent, PlayerRunnerDestroyEvent;



//    private void Awake()
//    {
//        instance = this;
//        StopGame();
//        gameoverScreen.SetActive(false);
//        gameNextLevelScreen.SetActive(false);

//    }

//    void Start()
//    {
//        SaveGameGet();
        
//    }

//    void Update()
//    {
//        SaveGameSet();
//        levelScoreText.text = totalLevelScore.ToString();
//        SwipeToStart();
//    }

//    private void SwipeToStart()
//    {
      
//        if (!isSwipeToStart && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
//        {
            
//            StartGame();
//            isSwipeToStart = true;
//        }
//        else if (!isSwipeToStart && Input.GetMouseButton(0))
//        {

//            StartGame();
//            isSwipeToStart = true;
//        }

      
//    }

//    public void AddPoint(int point)
//    {
//        score += point;
//        levelScore += point;
//    }

//    public void StartGame()
//    {
//        Time.timeScale = 1.0f;
//        gameStartScreen.SetActive(false);
//    }

//    public void LoseGame()
//    {

       

//        gameoverScreen.SetActive(true);
//        StopGame();

     

//    }

//    public void StopGame()
//    {
//        Time.timeScale = 0.0f;
//    }

//    public void RestartGame()
//    {
//        PlayerStartPositionEvent();
//        gameoverScreen.SetActive(false);
    
//        levelController.RestartGame();
      
       


//        StopGame();
//        gameStartScreen.SetActive(true);
//        levelScore = 0;
    
//    }

//    public void NextLevel() //GameNextLevel'deki nextLevel butonu bu methodu çalıştırır.
//    {

//        StartCoroutine(NextLevelMethod());


//    }

//    IEnumerator NextLevelMethod()
//    {
//        gameNextLevelButton.interactable = false;
//        score = score + totalLevelScore;
//        yield return new WaitForSecondsRealtime(2);
//        levelController.levelCount++;
//        gameNextLevelScreen.SetActive(false);
//        levelController.NextLevel();
//        PlayerStartPositionEvent();
//        PlayerRunnerDestroyEvent();
     
//        //playerController.DestroyBoats();
//        //playerController.PlayerStartPosition();
//        gameStartScreen.SetActive(true);
//        StopGame();
//        RandomLevel();
//        levelScore = 0;
//        //isSwipeToStart = false;
//        //playerController.PlayerControlActiveTrue();
//        gameNextLevelButton.interactable = true;
//    }

//    public void FinishLevel() // FinisLevel bu methodu çağırır.
//    {
//        //if (isLevelFinish)
//        //{
//        //    //playerController.PlayerControlActiveFalse();
//        //    //playerController.WinAnimation();

//        //    StartCoroutine(StartFinishMethod());

//        //}
     
//        PlayerFinishMoveEvent();
//        StartCoroutine(StartFinishMethod());

//        //PlayerRotateEvent();




//    }

//    IEnumerator StartFinishMethod()
//    {
//        yield return new WaitForSecondsRealtime(4);
//        gameNextLevelScreen.SetActive(true);
//        StopGame();
//    }

//    private void SaveGameGet()
//    {
//        //PlayerPrefs.DeleteAll();

//        score = PlayerPrefs.GetInt("score");
//        levelController.levelCount = PlayerPrefs.GetInt("level");
//        levelController.randomLevel = PlayerPrefs.GetInt("randomlevel");
//    }

//    private void SaveGameSet()
//    {
//        PlayerPrefs.SetInt("randomlevel", levelController.randomLevel);

//        PlayerPrefs.SetInt("level", levelController.levelCount);
//        levelText.text = "Level " + (levelController.levelCount + 1).ToString(); //Level yazısı

//        PlayerPrefs.SetInt("score", score);
//        scoreText.text = score.ToString();
//    }

//    private void RandomLevel()
//    {
//        levelController.randomLevel = UnityEngine.Random.Range(0, levelController.prefabLevels.Count);
//    }

//    public void LevelTotalPoint(int objetcsCount)
//    {
    
//        totalLevelScore = levelScore * objetcsCount ;

//    }
//}

