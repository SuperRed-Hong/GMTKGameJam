using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public Camera mainCamera;
    public bool isStop;
    [Header("Prefabs")]
    public PopUpUI popUpPrefab; //结束弹窗
    public GameObject stopPrefab;   // 暂停弹窗
    public GameObject scorePrefab; //分数弹窗
    public GameObject chooseCardPrefab;   // 抽卡弹窗

    public GameObject comic2Prefab;   // 动画2弹窗
    //public Button ReplayButton;
    [Header("GameObjects")]
    public Transform canvasRoot;
    public TextMeshProUGUI doctorScore;
    public TextMeshProUGUI patientScore;
    public TextMeshProUGUI turnNumber;
    public GameObject ContinueButton;

    private bool firstplay = true;
    //public float timeleft;
    public TextMeshProUGUI CountDown_tet;
    //private bool isCounting;

    public AudioManager audioManager;
    //public AudioManager audiomanager;

    public PlayerManager playerManager;
    public void Awake()
    {
        //audiomanager = GameObject.Find("Canvas").GetComponent<AudioManager>();
        audioManager = GetComponent<AudioManager>();
        playerManager = GetComponent<PlayerManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //isCounting = true;
        //testScene(4);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (isCounting)
        {
            timeleft -= Time.deltaTime;
        }
        CountDown_tet.text = timeleft.ToString(format: "0");
        if(timeleft <= 0 && isCounting)
        {
            isCounting = false;
        }*/
    }


    public void stop()
    {
        if (!isStop)
        {
            isStop = true;
            audioManager.AudioPlay(5);
            //Time.timeScale = 0;
            Instantiate(stopPrefab, canvasRoot);
            playerManager.ClearCheck();
        }
    }

    public void back()
    {
        audioManager.AudioPlay(5);
        SceneManager.LoadScene("Start game");
    }

    public void score()
    {
        //audiomanager.MusicChange(2);
        ScoreUIController  textscore= Instantiate(scorePrefab, canvasRoot).GetComponent<ScoreUIController>();
        textscore.SetDoctor(doctorScore.text);
        textscore.SetPatient(patientScore.text);
        if (firstplay)
        {
            firstplay = false;
            Invoke("comic2", 6f);
            Invoke("nextTurn", 11.1f);
        }
        else
        {
            Invoke("nextTurn", 6.1f);
        }

    }

    public void comic2()
    {
        Instantiate(comic2Prefab, canvasRoot);
    }

    public void chooseCard()
    {

        //audioManager.AudioPlay(5);
        //Instantiate(chooseCardPrefab, canvasRoot);
    }

    public void nextTurn()
    {
        playerManager.StartGame();
    }

    public void doctorWin()
    {
        SceneManager.LoadScene("DoctorWin");
    }
    public void patientWin()
    {
        SceneManager.LoadScene("PatientWin");
    }
    public void SetTimeText(float time){
        CountDown_tet.text = time.ToString(format: "0");
    }

    public void SetDoctorScore(int Score)
    {
        doctorScore.text = Score.ToString();
    }
    public void SetPatientScore(int Score)
    {
        patientScore.text = Score.ToString();
    }
}

