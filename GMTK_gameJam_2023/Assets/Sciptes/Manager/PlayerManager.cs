using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject player1Prefab, player2Prefab;
    [SerializeField] private Transform player1SpawnPoint, player2SpawnPoint;
    //private ScoreManager scoreManager;
    private UIController uiController;
    private PlayerController player1;//医生
    private PlayerController player2;//病人
    private List<Checker> checker_list;
    private bool cacher;//记录当前抓人者
    private int doctorScore;
    private int patientScore;
    private bool winner;

    //AudioManager audioManager;

    private void Awake()
    {
        cacher = false;
        checker_list = new List<Checker>();
        //scoreManager=GameObject.Find("GameManager").GetComponent<ScoreManager>();
        uiController = GetComponent<UIController>();
        StartGame();
        //audioManager = GameObject.Find("Canvas").GetComponent<AudioManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnPlayer()
    {
        player1 = Instantiate(player1Prefab, player1SpawnPoint.position, player1SpawnPoint.rotation).GetComponent<PlayerController>();
        player2 = Instantiate(player2Prefab, player2SpawnPoint.position, player2SpawnPoint.rotation).GetComponent<PlayerController>();
        player1.transform.SetParent(GameObject.Find("PlayGround").transform);
        player2.transform.SetParent(GameObject.Find("PlayGround").transform);
        player1.SetManager(this);
        player2.SetManager(this);
        player1.SetOpponent(player2);
        player2.SetOpponent(player1);
        player1.GetArm().SetPlayer(player2.transform);
        player2.GetArm().SetPlayer(player1.transform);
        player1.SetRole(cacher);
        player2.SetRole(!cacher);
    }

    public void AddChecker(Checker checker)
    {
        checker_list.Add(checker);
    }

    public void CheckByTime()
    {
        foreach (Checker checher in checker_list)
        {
            checher.Check();
        }
    }

    public void StartCheck()
    {
        InvokeRepeating("CheckByTime", 0f, 0.02f);
    }

    public void ClearCheck()
    {
        CancelInvoke("CheckByTime");
    }

    public float DetectDistance()
    {
        return (player1.transform.position - player2.transform.position).magnitude;
    }

    public void GiveSkill(PlayerController player, Skill skill)
    {
        player.SetSkill(skill);
    }

    public PlayerController GetPlayer1()
    {
        return player1.GetComponent<PlayerController>();
    }

    public PlayerController GetPlayer2()
    {
        return player2.GetComponent<PlayerController>();
    }
    private IEnumerator DestroyPlayers()
    {
        ClearCheck();
        player1.onStunned();
        player2.onStunned();
        yield return new WaitForSeconds(1f);
        Destroy(player1.gameObject);
        Destroy(player2.gameObject);
        checker_list.Clear();
    }
    public void whoWin(bool role)
    {
        winner= role == cacher;
    }
    public bool returnWinner()
    {
        return winner;
    }
    public bool whoIsCacher()
    {
        return cacher;
    }
    public void EndGame(bool role)
    {
        Debug.Log(role);
        StartCoroutine(DestroyPlayers());
        //audioManager.MusicChange(2);
        if (role == cacher)
        {
            ++doctorScore;
        }
        else
        {
            ++patientScore;
        }
        if (doctorScore == 3)
        {
            uiController.doctorWin();
        }
        else if (patientScore == 3)
        {
            uiController.patientWin();
        }
        else
        {
            uiController.SetDoctorScore(doctorScore);
            uiController.SetPatientScore(patientScore);
            uiController.score();
        }

    }
    public void StartGame()
    {
        cacher = !cacher;
        SpawnPlayer();
        AddChecker(new TimeCountDown(this));
        StartCheck();
    }
    public void RefreshTime(float time)
    {
        uiController.SetTimeText(time);
    }
}
