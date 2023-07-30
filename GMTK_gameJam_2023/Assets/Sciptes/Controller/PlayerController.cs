using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private GameObject currentOneWayPlayform;
    [SerializeField] private CapsuleCollider2D playerCollider;
    [SerializeField] private ArmController arm;
    private PlayerController opponent;
    private PlayerController playerController;
    private PlayerManager manager;
    private Rigidbody2D rb2D;
    Animator animator;
    Image dcard;
    Image pcard;
    public GameObject barrierPrefab;

    [SerializeField] private float basicMoveSpeed;
    private float moveSpeed;
    [SerializeField] private float jumpForce;
    
    private Skill currentSkill;

    [SerializeField] private bool character;
    private bool role;

    private bool _isJumping = false;
    private bool _isFalling = false;
    private bool _isRunning = false;
    private bool isFacingRight = true;
    private bool _isStunned = false;
    private bool _isCollision;
    private bool _isInvincible=false;
    private PhysicsCheck physicsCheck;

    public AudioManager audioManager;
    public bool isflashing;
    public float moveHorizontal;
    public float moveVertical;
    public bool isSlowed;
    public GameObject smash;
    public GameObject shield;
    TextMeshProUGUI dcardname;
    TextMeshProUGUI pcardname;
    private bool isFalling
    {
        get
        {
            return _isFalling;
        }
        set
        {
            _isFalling = value;
           

        }
    }

    public bool isJumping
    {
        get
        {
            return _isJumping;
        }
        private set
        {
            _isJumping = value;
            
        }
    }


    public bool isRunning
    {
        get
        {
            return _isRunning;
        }
        private set
        {
            _isRunning = value;
            
        }
    }

    private void Awake()
    {
        isSlowed = false;
        isflashing = false;
        physicsCheck = GetComponent<PhysicsCheck>();
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        role=character;
        audioManager = GameObject.Find("UIManager").GetComponent<AudioManager>();
        dcard = GameObject.Find("dcard1").GetComponent<Image>();
        pcard = GameObject.Find("pcard1").GetComponent<Image>();
        dcardname = GameObject.Find("dcardname").GetComponent<TextMeshProUGUI>();
        pcardname = GameObject.Find("pcardname").GetComponent<TextMeshProUGUI>();
        playerController = GetComponent<PlayerController>();
    }
    void Start()
    {
        if (dcard.sprite != null)
        {
            if (character)
            {
                giveSkill(dcard.sprite.name);
            }
        }
        if(pcard.sprite != null)
        {
            if (!character)
            {
                giveSkill(pcard.sprite.name);
            }
        }
        moveSpeed=basicMoveSpeed;

    }
    void Update()
    {
        if(character){
            moveHorizontal = Input.GetAxisRaw("Horizontal1");
            moveVertical = Input.GetAxisRaw("Vertical1");
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (!_isStunned && currentOneWayPlayform != null && currentOneWayPlayform.tag =="OneWayPlatform")
                {
                    StartCoroutine(DisableCollision());
                }
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!_isStunned && currentSkill != null)
                {
                    audioManager.AudioPlay(2);
                    currentSkill.UseSkill();
                    currentSkill=null;
                    dcard.sprite = null;
                    dcard.color = new Color(255, 255, 255, 0);
                    dcardname.text = "您暂时没有卡牌~";
                }
            }
        }else{
            moveHorizontal = Input.GetAxisRaw("Horizontal2");
            moveVertical = Input.GetAxisRaw("Vertical2");
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (!_isStunned&&currentOneWayPlayform != null && currentOneWayPlayform.tag != "Platform")
                {
                    StartCoroutine(DisableCollision());
                }
            }
            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                if (!_isStunned && currentSkill != null)
                {
                    audioManager.AudioPlay(2);
                    currentSkill.UseSkill();
                    currentSkill=null;
                    pcard.sprite = null;
                    pcard.color = new Color(255, 255, 255, 0);
                    pcardname.text = "您暂时没有卡牌~";
                }
            }
        }
        

    }
    private void FixedUpdate()
    {
        if (!_isStunned) {
            rb2D.velocity = new Vector2(moveHorizontal * moveSpeed * Time.deltaTime, rb2D.velocity.y);
            isRunning = true;

            if (physicsCheck.isGround && rb2D.velocity.y < 0.1f&& moveVertical>0)
            {
                audioManager.AudioPlay(1);
                rb2D.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);

            }

            onFalling();
            FlipFace();
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "OneWayPlatform")
        {
            currentOneWayPlayform = collision.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)

    {
        if (_isCollision == false)
        {
            if ((collision.gameObject.tag == "OneWayPlatform" || collision.gameObject.tag == "Platform")  /*gameObject.transform.position.y - collision.gameObject.transform.position.y > 0 */ && rb2D.velocity.y == 0)
            {
                isJumping = false;
                if(collision.gameObject.tag == "OneWayPlatform")
                {
                    currentOneWayPlayform = collision.gameObject;

                }
                Debug.Log(collision.gameObject.tag);
            }
        }
        
        
    }
   

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "OneWayPlatform" || collision.gameObject.tag == "Platform")
        {
            isJumping = true;
            currentOneWayPlayform = null;

        }
    }
    private void onFalling()
    {
        if (rb2D.velocity.y < 0f)
        {
            isFalling = true;

        }
        else
        {
            isFalling = false;
        }
    }
 

    private IEnumerator DisableCollision()
    { if (currentOneWayPlayform.GetComponent<CompositeCollider2D>() != null)
        {
            CompositeCollider2D platformCollider = currentOneWayPlayform.GetComponent<CompositeCollider2D>();
            Physics2D.IgnoreCollision(playerCollider, platformCollider);
            yield return new WaitForSeconds(0.3f);
            Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
        }
        else
        {
            Debug.Log("can't get currentOneWayPlayForm");
        }
    }
    public Collider2D GetCollider(){
        return playerCollider;
    }
    private void FlipFace()
    {
        if (isFacingRight && moveHorizontal < 0f || !isFacingRight && moveHorizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            this.gameObject.GetComponent<SpriteRenderer>().flipX=!this.gameObject.GetComponent<SpriteRenderer>().flipX;
        }
    }
    
    public void Win(){
        manager.whoWin(true);
        manager.EndGame(true);
        //Debug.Log(character);
    }

    public PlayerController GetOpponent(){
        return opponent;
    }

    public void SetOpponent(PlayerController opponent){
        this.opponent=opponent;
    }

    public void onStunned()
    {
        if(!_isInvincible){
            _isStunned = true;
        }
    }
    public void offStunned()
    {
        _isStunned = false;
    }
    public void onInvincible()
    {
        _isInvincible = true;
    }
    public void offInvincible()
    {
        _isInvincible = false;
    }
    public bool getInvincible(){
        return _isInvincible;
    }
    public void ChangeSpeed(float ratio){
        if(!_isInvincible){
            moveSpeed=basicMoveSpeed*ratio;
        }
    }
    public void ResetSpeed(){
        moveSpeed=basicMoveSpeed;
    }
    public void SetSkill(Skill skill)
    {
        currentSkill = skill;
    }
    public void SetCharacter(bool c){
        character=c;
    }
    public void FlipCharacter(){
        character=!character;
    }
    public void SetRole(bool role){
        this.role=role;
        arm.SetUsable(role);
    }
    public bool GetRole(){
        return role;
    }
    public ArmController GetArm(){
        return arm;
    }

    public PlayerManager GetManager(){
        return manager;
    }

    public void SetManager(PlayerManager manager){
        this.manager=manager;
    }
    public float getMoveHorizontal()
    {
        return moveHorizontal;
    }
    public float getMoveVertical()
    {
        return moveVertical;
    }
    
    public void giveSkill(string cardName)
    {
        switch (cardName)
        {
            case "金钟罩d":
                playerController.SetSkill(new Shield(playerController));
                dcardname.text = "金钟罩";
                break;
            case "减速d":
                playerController.SetSkill(new Impact(manager, playerController));
                dcardname.text = "震荡波";
                break;
            case "闪现d":
                playerController.SetSkill(new Flash(playerController));
                dcardname.text = "闪现";
                break;
            case "障碍d":
                playerController.SetSkill(new Barrier(playerController, barrierPrefab));
                dcardname.text = "超级路障";
                break;
            case "伸手d":
                playerController.SetSkill(new Hand(manager, playerController));
                dcardname.text = "麒麟臂";
                break;
            case "金钟罩p":
                playerController.SetSkill(new Shield(playerController));
                pcardname.text = "金钟罩";
                break;
            case "减速p":
                playerController.SetSkill(new Impact(manager, playerController));
                pcardname.text = "震荡波";
                break;
            case "闪现p":
                playerController.SetSkill(new Flash(playerController));
                pcardname.text = "闪现";
                break;
            case "障碍p":
                playerController.SetSkill(new Barrier(playerController, barrierPrefab));
                pcardname.text = "超级路障";
                break;
            case "伸手p":
                playerController.SetSkill(new Hand(manager, playerController));
                pcardname.text = "麒麟臂";
                break;
            default:
                break;
        }
    }
    public void playSmash()
    {
        smash.SetActive(true);
        Invoke("stopSmash", 0.375f);
    }
    public void playflash()
    {
           isflashing = true;
        Invoke("stopFlash", 1);
            
    }
    public void playShield()
    {
        shield.SetActive(true);
        Invoke("stopShield", 3);
    }
    public void stopSmash()
    {
        smash.SetActive(false);
    }
    public void stopFlash()
    {
        isflashing = false;
    }
    public void stopShield()
    {
        shield.SetActive(false);
    }
}
