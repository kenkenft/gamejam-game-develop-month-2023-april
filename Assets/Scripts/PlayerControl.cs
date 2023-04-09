using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 10.0f;
    [SerializeField] private int _coinCapacity = 10;
    private Vector2 _moveXY = new Vector2(0f, 0f);
    public Rigidbody2D PlayerRig;
    public Collider2D PlayerCollider;
    
    
    void Update()
    {
        Move();
    }

    private void Move()
    {
        // Debug.Log("Player Move called!");

        _moveXY[0] = Input.GetAxis("Horizontal") * _playerSpeed;
        _moveXY[1] = Input.GetAxis("Vertical") * _playerSpeed;

        if( _moveXY[0] !=0 || _moveXY[1] !=0)
        {
            PlayerRig.velocity = _moveXY;
        }
        

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        CheckTag(col.gameObject.tag);
    } 

    void CheckTag(string tag)
    {
        switch(tag)
        {
            case "Coin":
            {
                Debug.Log("Coin picked up!");
                break;
            }
            case "Pool":
            {
                Debug.Log("Can Deposit Money!");
                break;
            }
            default:
            {
                break;
            }
        }
    }
}
