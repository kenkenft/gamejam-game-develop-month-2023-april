using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 10.0f;
    
    private Vector2 _moveXY = new Vector2(0f, 0f);
    public Rigidbody2D PlayerRig;    

    private bool _canDeposit = false;

    [HideInInspector] public delegate bool OnInteractKeyDown();
    [HideInInspector] public static OnInteractKeyDown CheckCanDeposit;
    
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
        }
        else
        {
            Debug.Log("Can't deposit! Doing something else");
        }
    }

    
}
