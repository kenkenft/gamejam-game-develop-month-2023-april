using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStorage : MonoBehaviour
{
    [SerializeField] private int _coinCapacityMax = 10, _coinCapacityUsed = 0;
    [SerializeField] private List<CoinBehaviour>  _coinsCollected = new List<CoinBehaviour>(){};
    private bool _isCoinCollided = false, _canDeposit = false;

    [HideInInspector] public delegate void OnDepositCoin(CoinBehaviour coinBehaviour);
    [HideInInspector] public static OnDepositCoin DepositCoin;
    [HideInInspector] public delegate void OnPlaySFX(string audioName);
    [HideInInspector] public static OnPlaySFX PlaySFX;
    
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
        else
            PlaySFX?.Invoke("fail");
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
        PlaySFX?.Invoke("coinPickup");
    }

     bool GetCanDeposit()
     {
        return _canDeposit;
     }

     void DepositCoins()
     {
        int index = 0;
        if(_coinsCollected.Count > 0)
        {    
            for(int i = _coinsCollected.Count; i > 0 ; i--)
            {
                index = i-1;
                Debug.Log("Coin no.: " + i + ". Coin value: " + _coinsCollected[index].Value);
                DepositCoin?.Invoke(_coinsCollected[index]);
                _coinsCollected.RemoveAt(index);
            }
            _coinCapacityUsed = 0;
            PlaySFX?.Invoke("deposit");
        }
        else
        {
            PlaySFX?.Invoke("fail");
        }
     }
}
