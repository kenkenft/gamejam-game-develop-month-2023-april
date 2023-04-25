using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float _playerSpeedBase = 10.0f, _playerSpeed, _playerPenaltyModifier = 0.4f;
    
    private Vector2 _moveXY = new Vector2(0f, 0f);
    public Rigidbody2D PlayerRig;    

    [HideInInspector] public delegate bool OnInteractKeyDown();
    [HideInInspector] public static OnInteractKeyDown CheckCanDeposit;

    [HideInInspector] public delegate void OnCanDepositTrue();
    [HideInInspector] public static OnCanDepositTrue CallDepositCoins;

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
    }

    void Update()
    {
        Move();
        if(Input.GetKeyDown(KeyCode.E))
            CheckInteractContext();
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

    
}
