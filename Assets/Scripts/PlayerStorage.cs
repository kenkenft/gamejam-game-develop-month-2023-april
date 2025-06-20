using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStorage : MonoBehaviour
{
    [SerializeField] private int _coinCapacityMax = 10, _coinCapacityUsed = 0;
    private int[] _bonusMultiplier = {0, 100, 103, 106, 109, 115, 124, 139, 163, 202, 265};
    [SerializeField] private List<CoinBehaviour>  _coinsCollected = new List<CoinBehaviour>(){};
    [SerializeField] private bool _isCoinCollided = false, _canDeposit = false;
    private List<CoinScriptable> _coinTypes = new List<CoinScriptable>();

    // [HideInInspector] public delegate void OnDepositCoin(CoinBehaviour coinBehaviour);
    [HideInInspector] public delegate void OnDepositCoin(int intValue);
    [HideInInspector] public static OnDepositCoin DepositCoin;

    [HideInInspector] public delegate void OnPlayerStorageChange(float penaltyAmount);
    [HideInInspector] public static OnPlayerStorageChange ApplyWeightPenalty;
    [HideInInspector] public delegate void OnPlaySFX(string audioName);
    [HideInInspector] public static OnPlaySFX PlaySFX;
    
    
    void OnEnable()
    {
        UIManager.StartGameSetUp += GameStartSetUp;
        PlayerControl.CheckCanDeposit += GetCanDeposit;
        PlayerControl.CallDepositCoins += DepositCoins;
        PlayerControl.SetPlayerStorage += GameStartSetUp;
    }

    void OnDisable()
    {
        UIManager.StartGameSetUp -= GameStartSetUp;
        PlayerControl.CheckCanDeposit -= GetCanDeposit;
        PlayerControl.CallDepositCoins -= DepositCoins;
        PlayerControl.SetPlayerStorage -= GameStartSetUp;
    }

    void Start()
    {
        _coinTypes.AddRange(GameProperties.GetCoinScriptables());
    }
    
    public void GameStartSetUp()
    {
        _coinCapacityUsed = 0;
        _coinsCollected.Clear();
        _isCoinCollided = _isCoinCollided ? false : _isCoinCollided; 
        _canDeposit = _canDeposit ? false : false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        CheckColliderTag(col.gameObject.tag, true);
        if(_isCoinCollided && (_coinCapacityUsed < _coinCapacityMax) )
            PickUpCoin(col.gameObject.GetComponent<CoinBehaviour>());
        else
            PlaySFX?.Invoke("fail");
    } 

    void OnTriggerExit2D(Collider2D col)
    {
        CheckColliderTag(col.gameObject.tag, false);
    }

    void CheckColliderTag(string tag, bool state)
    {
        switch(tag)
        {
            case "Coin":
            {  
                _isCoinCollided = state;
                Debug.Log("Coin case: " + _isCoinCollided);
                break;
            }
            case "Pool":
            {
                _canDeposit = state;
                Debug.Log("Pool case: " + _canDeposit);
                break;
            }
            default:
                break;
        }
    }

    void PickUpCoin(CoinBehaviour coin)
    {
        _coinCapacityUsed += 1;
        _coinsCollected.Add(coin);
        ApplyWeightPenalty?.Invoke(-coin.Weight);
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
            DepositCoin?.Invoke(CalcCoinScore());
            for(int i = _coinsCollected.Count; i > 0 ; i--)
            {
                index = i-1;
                Debug.Log("Coin no.: " + i + ". Coin value: " + _coinsCollected[index].Value);
                // DepositCoin?.Invoke(_coinsCollected[index]);    
                ApplyWeightPenalty?.Invoke(_coinsCollected[index].Weight); 
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

     int CalcCoinScore()
     {
        int score = 0;

        int[] coinTypeTally = new int[_coinTypes.Count];

        foreach(CoinBehaviour coin in _coinsCollected)
        {
            for(int i = 0; i < _coinTypes.Count; i++)
            {
                if(coin.Value == _coinTypes[i].CoinValue)
                {    
                    coinTypeTally[i]++;
                    break;
                }
            }
        }

        for(int i = 0; i < coinTypeTally.Length; i++)
        {
            Debug.Log("coinTypeTally index " + i + ": " + coinTypeTally[i]);
            Debug.Log("_bonusMultiplier value: " + _bonusMultiplier[coinTypeTally[i]]);
            score += (100 * _coinTypes[i].CoinValue * coinTypeTally[i] * _bonusMultiplier[coinTypeTally[i]] / 100);
        }

        return score;
     }

}
