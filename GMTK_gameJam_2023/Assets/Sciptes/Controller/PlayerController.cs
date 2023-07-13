using System.Collections;
using System.Collections.Generic;
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

    public AudioManager audioManager;
    private bool isFalling
    {
        get
        {
            return _isFalling;
        }
        set
        {
            _isFalling = value;
            animator.SetBool("isFalling", value);

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
            animator.SetBool("isJumping", value);
        }
    }
    private float moveHorizontal;
    private float moveVertical;
    public bool isRunning
    {
        get
        {
            return _isRunning;
        }
        private set
        {
            _isRunning = value;
            animator.SetBool("isRunning", value);
        }
    }
    private void Awake()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        role=character;
        audioManager = GameObject.Find("UIManager").GetComponent<AudioManager>();
        dcard = GameObject.Find("dcard1").GetComponent<Image>();
        pcard = GameObject.Find("pcard1").GetComponent<Image>();
        playerController = GetComponent<PlayerController>();
    }
    void Start()
    {
        if (dcard.sprite != null)
        {
            giveSkill(dcard.sprite.name);
        }
        if(pcard.sprite != null)
        {
            giveSkill(pcard.sprite.name);
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
                if (!_isStunned && currentOneWayPlayform != null && currentOneWayPlayform.tag != "Platform")
                {
                    StartCoroutine(DisableCollision());
                }
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!_isStunned && currentSkill != null)
                {
                    audioManager.AudioPlay(3);
                    currentSkill.UseSkill();
                    currentSkill=null;
                    dcard.sprite = null;
                    dcard.color = new Color(255, 255, 255, 0);
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
                    audioManager.AudioPlay(3);
                    currentSkill.UseSkill();
                    currentSkill=null;
                    pcard.sprite = null;
                    pcard.color = new Color(255, 255, 255, 0);
                }
            }
        }
        

    }
    private void FixedUpdate()
    {
        if (!_isStunned) {
            if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
            {

                rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
                isRunning = true;
            }
            else
            {
                isRunning = false;
            }

            if (!isJumping && moveVertical > 0.1f)
            {
                audioManager.AudioPlay(1);
                rb2D.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);

            }

            onFalling();
            FlipFace();
        }
        

    }

    private void OnTriggerStay2D(Collider2D collision)

    {
        if (_isCollision == false)
        {
            if ((collision.gameObject.tag == "OneWayPlatform" || collision.gameObject.tag == "Platform")  /*gameObject.transform.position.y - collision.gameObject.transform.position.y > 0 */ && rb2D.velocity.y == 0)
            {
                isJumping = false;
                currentOneWayPlayform = collision.gameObject;
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
            yield return new WaitForSeconds(0.35f);
            Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
        }
        else
        {
            Debug.Log("can't get currentOneWayPlayForm");
        }
        
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
                break;
            case "减速d":
                playerController.SetSkill(new Impact(manager, playerController));
                break;
            case "闪现d":
                playerController.SetSkill(new Flash(playerController));
                break;
            case "障碍d":
                
                break;
            case "伸手d":
                
                break;
            case "金钟罩p":
                playerController.SetSkill(new Shield(playerController));
                break;
            case "减速p":
                playerController.SetSkill(new Impact(manager, playerController));
                break;
            case "闪现p":
                playerController.SetSkill(new Flash(playerController));
                break;
            case "障碍p":
                
                break;
            case "伸手p":
                
                break;
            default:
                break;
        }
    }
}
