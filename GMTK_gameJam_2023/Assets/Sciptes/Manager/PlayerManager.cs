using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject player1Prefab, player2Prefab;
    [SerializeField] private Transform player1SpawnPoint, player2SpawnPoint;
    [SerializeField] private Transform trapPoints;
    [SerializeField] private GameObject trapPrefeb;
    [SerializeField] private int trapPointNum;
    //private ScoreManager scoreManager;
    private UIController uiController;
    private PlayerController player1;//医生
    private PlayerController player2;//病人
    private GameObject trap;//当前轮的陷阱
    private List<Checker> checker_list;
    private bool cacher;//记录当前抓人者
    private int doctorScore;
    private int patientScore;
    private bool winner;
    private Destroy lifeTimeChecker;
    private bool touched;

    //AudioManager audioManager;

    private void Awake()
    {
        cacher = true;
        checker_list = new List<Checker>();
        lifeTimeChecker=new Destroy();
        checker_list.Add(lifeTimeChecker);
        touched=false;
        //scoreManager=GameObject.Find("GameManager").GetComponent<ScoreManager>();
        uiController = GetComponent<UIController>();
        //audioManager = GameObject.Find("Canvas").GetComponent<AudioManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        AddChecker(new TimeCountDown(this));
        SpawnPlayer();
        StartCheck();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnPlayer()
    {
        player1 = Instantiate(player1Prefab, player1SpawnPoint.position, player1SpawnPoint.rotation).GetComponent<PlayerController>();
        player2 = Instantiate(player2Prefab, player2SpawnPoint.position, player2SpawnPoint.rotation).GetComponent<PlayerController>();
        trap = Instantiate(trapPrefeb, trapPoints.GetChild(Random.Range(0,trapPointNum)).transform.position, trapPoints.rotation);
        trap.transform.SetParent(trapPoints);
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

    public void AddLifeTime(LifeTime lifetime){
        lifeTimeChecker.AddLifeTime(lifetime);
    }

    public void CheckByTime()
    {
        foreach (Checker checker in checker_list)
        {
            checker.Check();
        }
    }

    public void StartCheck()
    {
        InvokeRepeating("CheckByTime", 0f, 0.02f);
    }

    public IEnumerator ClearCheck()
    {
        CancelInvoke("CheckByTime");
        yield return new WaitForSeconds(0.02f);
        checker_list.Clear();
    }

    public void PauseCheck(){
        foreach (Checker checker in checker_list)
        {
            checker.Pause();
        }
    }

    public void ResumeCheck(){
        foreach (Checker checker in checker_list)
        {
            checker.Resume();
        }
    }

    public void ResetCheck(){
        foreach (Checker checker in checker_list)
        {
            checker.Reset();
        }
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
        return player1;
    }

    public PlayerController GetPlayer2()
    {
        return player2;
    }
    private IEnumerator DestroyPlayers()
    {
        //ClearCheck();
        PauseCheck();
        ResetCheck();
        player1.onStunned();
        player2.onStunned();
        if(trap){
            Destroy(trap);
        }
        yield return new WaitForSeconds(1f);
        Destroy(player1.gameObject);
        Destroy(player2.gameObject);
        //checker_list.Clear();
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
    public void SetTouched(bool state){
        touched=state;
    }

    public bool GetTouched(){
        return touched;
    }
    public void EndGame(bool role)
    {
        //Debug.Log(role);
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
        touched=false;
        SpawnPlayer();
        //AddChecker(new TimeCountDown(this));
        ResumeCheck();
    }
    public void RefreshTime(float time)
    {
        uiController.SetTimeText(time);
    }
}
