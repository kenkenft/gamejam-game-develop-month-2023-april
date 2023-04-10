using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStorage : MonoBehaviour
{
    [SerializeField] private int _coinCapacityMax = 10, _coinCapacityUsed = 0;
    [SerializeField] private List<CoinBehaviour>  _coinsCollected = new List<CoinBehaviour>(){};
    private bool _isCoinCollided = false, _isPoolCollided = false;

    [HideInInspector] public delegate void OnInteractKeyDown(bool state);
    [HideInInspector] public static OnInteractKeyDown CheckCanDeposit;
    void OnTriggerEnter2D(Collider2D col)
    {
        CheckColliderTag(col.gameObject.tag);
        if(_isCoinCollided && (_coinCapacityUsed < _coinCapacityMax) )
            PickUpCoin(col.gameObject.GetComponent<CoinBehaviour>());
    } 

    void CheckColliderTag(string tag)
    {
        switch(tag)
        {
            case "Coin":
            {
                Debug.Log("Coin picked up!");
                _isCoinCollided = true;
                break;
            }
            case "Pool":
            {
                Debug.Log("Can Deposit Money!");
                _isPoolCollided = true;
                break;
            }
            default:
            {
                break;
            }
        }
    }

    void PickUpCoin(CoinBehaviour coin)
    {
        _isCoinCollided = false;
        _coinCapacityUsed += 1;
        _coinsCollected.Add(coin);
    }

     
}
