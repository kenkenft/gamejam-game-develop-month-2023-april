using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStorage : MonoBehaviour
{
    [SerializeField] private int _coinCapacityMax = 10, _coinCapacityUsed = 0;
    [SerializeField] private List<CoinBehaviour>  _coinsCollected = new List<CoinBehaviour>(){};
    private bool _isCoinCollided = false, _canDeposit = false;

    [HideInInspector] public delegate void OnInteractKeyDown(bool state);
    [HideInInspector] public static OnInteractKeyDown SomethingElse;
    
    void OnEnable()
    {
        PlayerControl.CheckCanDeposit += GetCanDeposit;
        PlayerControl.CallDepositCoins += DepositCoins;
    }

    void OnDisable()
    {
        PlayerControl.CheckCanDeposit -= GetCanDeposit;
        PlayerControl.CallDepositCoins -= DepositCoins;
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        CheckColliderTag(col.gameObject.tag);
        if(_isCoinCollided && (_coinCapacityUsed < _coinCapacityMax) )
            PickUpCoin(col.gameObject.GetComponent<CoinBehaviour>());
    } 

    void OnTriggerExit2D(Collider2D col)
    {
        CheckColliderTag(col.gameObject.tag);
    }

    void CheckColliderTag(string tag)
    {
        switch(tag)
        {
            case "Coin":
            {  
                _isCoinCollided = !_isCoinCollided;
                Debug.Log("Coin case: " + _isCoinCollided);
                break;
            }
            case "Pool":
            {
                _canDeposit = !_canDeposit;
                Debug.Log("Pool case: " + _canDeposit);
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
        _coinCapacityUsed += 1;
        _coinsCollected.Add(coin);
        coin.gameObject.SetActive(false);
    }

     bool GetCanDeposit()
     {
        return _canDeposit;
     }

     void DepositCoins()
     {
        for(int i = 0; i < _coinsCollected.Count; i++)
        {
            Debug.Log("Coin no.: " + i + ". Coin value: " + _coinsCollected[i].Value);
        }
     }
}
