using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private GameObject currentOneWayPlayform;
    [SerializeField] private BoxCollider2D playerCollider;
    [SerializeField] private ArmController arm;
    private PlayerController opponent;
    private PlayerManager manager;
    private Rigidbody2D rb2D;
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
    private bool _isTrapStun = false;
    private PhysicsCheck physicsCheck;

    public AudioManager audioManager;
    public bool isflashing;
    public float moveHorizontal;
    public float moveVertical;
    public bool isSlowed;
    public GameObject smash;
    public GameObject shield;
    private bool onTheWall = false;
    private bool leftWall = false;
    private bool rightWall = false;
    public int playerNumber;
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
    public bool trapStun
    {
        get
        {
            return _isTrapStun;
        }
        private set
        {
            _isTrapStun = value;

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
        role=character;
        audioManager = GameObject.Find("UIManager").GetComponent<AudioManager>();
        moveSpeed = basicMoveSpeed;
       
    }


    void Update()
    {
        if (character)
        {
            moveHorizontal = Input.GetAxisRaw("Horizontal1");
            moveVertical = Input.GetAxisRaw("Vertical1");
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (!_isStunned && !_isTrapStun && currentOneWayPlayform != null && currentOneWayPlayform.tag == "OneWayPlatform")
                {
                    StartCoroutine(DisableCollision());
                }
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!_isStunned && !_isTrapStun && currentSkill != null)
                {
                    audioManager.AudioPlay(2);
                    currentSkill.UseSkill();
                    currentSkill = null;
                }
            }
        }
        else
        {
            moveHorizontal = Input.GetAxisRaw("Horizontal2");
            moveVertical = Input.GetAxisRaw("Vertical2");
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (!_isStunned && !_isTrapStun && currentOneWayPlayform != null && currentOneWayPlayform.tag != "Platform")
                {
                    StartCoroutine(DisableCollision());
                }
            }
            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                if (!_isStunned && !_isTrapStun && currentSkill != null)
                {
                    audioManager.AudioPlay(2);
                    currentSkill.UseSkill();
                    currentSkill = null;
                }
            }
        }
        if (rb2D.velocity.y == 0 && !physicsCheck.isGround)
        {
            moveHorizontal = 0f;
        }

    }
    private void FixedUpdate()
    {
        if (!_isStunned && !_isTrapStun) {
            if (rightWall && moveHorizontal < 0f && onTheWall)
            {
                rb2D.velocity = new Vector2(moveHorizontal * moveSpeed * Time.deltaTime, rb2D.velocity.y);
            }
            if (leftWall && moveHorizontal > 0f && onTheWall)
            {
                rb2D.velocity = new Vector2(moveHorizontal * moveSpeed * Time.deltaTime, rb2D.velocity.y);
            }
            if (!onTheWall)
            {
                rb2D.velocity = new Vector2(moveHorizontal * moveSpeed * Time.deltaTime, rb2D.velocity.y);
            }    
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (role)
            {
                collision.gameObject.GetComponent<PlayerController>().SetRole(true);
                role = false;
            }
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
                //Debug.Log(collision.gameObject.tag);
            }
            if (collision.gameObject.tag == "wall")
            {
                if (collision.gameObject.transform.position.x - transform.position.x < 0f && !physicsCheck.isGround)
                {
                    rightWall = true;
                    onTheWall = true;
                }
                if (collision.gameObject.transform.position.x - transform.position.x > 0f && !physicsCheck.isGround)
                {
                    leftWall = true;
                    onTheWall = true;
                }
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
        if (collision.gameObject.tag == "wall")
        {
            onTheWall = false;
            rightWall = false;
            leftWall= false;
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
            yield return new WaitForSeconds(0.18f);
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
    public void onTrapStunned()
    {
        audioManager.AudioPlay(7);
        if (!_isInvincible)
        {
            _isTrapStun = true;
        }
    }
    public void offTrapStunned()
    {
        _isTrapStun = false;
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
    public bool GetCurrentSkill()
    {
        return currentSkill != null;
    }
    public void SetCharacter(bool c){
        character=c;
    }
    public bool GetCharacter()
    {
        return character;
    }
    public void FlipCharacter(){
        character=!character;
    }
    public void SetRole(bool role){
        this.role=role;
        //arm.SetUsable(role);
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
    public void setPlayerNumber(int number)
    {
        playerNumber = number;
    }
    public int getPlayerNumber()
    {
        return playerNumber;
    }
}
