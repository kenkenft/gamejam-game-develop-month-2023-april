using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float _playerSpeedBase = 10.0f, _playerSpeed, _playerPenaltyModifier = 0.4f;
    private bool _isMovingVertical = false, _isMovingSideways = false,_isFacingRight = true;
    // private Vector3 _playerVelocity = new Vector3(0f, 0f, 0f);
    
    private Vector2 _moveXY = new Vector2(0f, 0f);
    public Rigidbody2D PlayerRig;

    public Animator animator;

    [HideInInspector] public delegate bool OnInteractKeyDown();
    [HideInInspector] public static OnInteractKeyDown CheckCanDeposit;
    [HideInInspector] public static OnInteractKeyDown CheckIsPlaying;

    [HideInInspector] public delegate void OnSomeEvent();
    [HideInInspector] public static OnSomeEvent CallDepositCoins;
    [HideInInspector] public static OnSomeEvent TogglePauseUI;
    [HideInInspector] public static OnSomeEvent SetPlayerStorage;

    [HideInInspector] public delegate void OnPlaySFX(string audioName);
    [HideInInspector] public static OnPlaySFX PlaySFX;
    
    void OnEnable()
    {
        PlayerStorage.ApplyWeightPenalty += AddPlayerSpeed;
        _playerSpeed = _playerSpeedBase;
    }

    void OnDisable()
    {
        PlayerStorage.ApplyWeightPenalty -= AddPlayerSpeed;
    }
    
    public void GameStartSetUp()
    {
        _playerSpeed = _playerSpeedBase;
        this.gameObject.transform.position = new Vector3(0f,5f,0f);
        SetPlayerStorage?.Invoke();
    }

    void Update()
    {
        if(CheckIsPlaying.Invoke())
        {
            Move();
            SetPlayerAnimation();
            if(Input.GetKeyDown(KeyCode.E))
                CheckInteractContext();

            if(Input.GetKeyDown(KeyCode.P))
                TogglePauseUI?.Invoke();
        }
    }

    private void Move()
    {
        // Debug.Log("Player Move called!");

        _moveXY[0] = Input.GetAxis("Horizontal") * _playerSpeed;
        _moveXY[1] = Input.GetAxis("Vertical") * _playerSpeed;

        if( _moveXY[0] !=0 || _moveXY[1] !=0)
            PlayerRig.velocity = _moveXY;
    }

    void CheckInteractContext()
    {
        Debug.Log("E-key pressed!");
        if(CheckCanDeposit.Invoke())
        {
            Debug.Log("Depositing money!");
            CallDepositCoins?.Invoke();
        }
        else
        {
            Debug.Log("Can't deposit! Doing something else");
            PlaySFX?.Invoke("fail");
        }
        
    }

    public void AddPlayerSpeed(float speedModifier)
    {
        _playerSpeed += _playerPenaltyModifier * speedModifier;
        _playerSpeed = Mathf.Clamp(_playerSpeed, 1f, _playerSpeedBase);
        Debug.Log("PlayerSpeed: " + _playerSpeed);
    }

    void SetPlayerAnimation()
    {
        // _playerVelocity = PlayerRig.velocity; 
        
        animator.SetFloat("_velocityY", _moveXY[1]);
        animator.SetFloat("_velocityX", Mathf.Abs(_moveXY[0]));

        if(_moveXY[1] !=0)
        {    
            _isMovingVertical = true;
            if(_moveXY[1] > 0)
            {
                animator.SetBool("_isMovingUp", true);
                animator.SetBool("_isMovingDown", false);
            }
            else
            {
                animator.SetBool("_isMovingUp", false);
                animator.SetBool("_isMovingDown", true);
            }
        }
        else
        {    
            _isMovingVertical = false;
            animator.SetBool("_isMovingUp", false);
            animator.SetBool("_isMovingDown", false);
        }
        
        if(_moveXY[0] !=0)
        {    
            _isMovingSideways = true;
            animator.SetBool("_isMovingSideways", true);
        }
        else
        {    
            _isMovingSideways = false;
            animator.SetBool("_isMovingSideways", false);
        }
        
        if(_isMovingVertical || _isMovingSideways)
        {    
            // animator.SetBool("_isMoving", true);
            if(_moveXY[0] > 0f && !_isFacingRight && _isMovingSideways)
                    FlipSprite(); 
            else if(_moveXY[0] < 0f && _isFacingRight && _isMovingSideways) 
                    FlipSprite();
        }
        // else
        //     animator.SetBool("_isMoving", false);

        // if(!_isMovingSideways && !_isMovingVertical)
        //     animator.SetBool("_isIdle", true);
        // else
        //     animator.SetBool("_isIdle", false);


        
    }

    void FlipSprite()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
