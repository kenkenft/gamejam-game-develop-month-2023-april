using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinHoleBehaviour : MonoBehaviour
{
    [SerializeField] private int _totalCoinValue = 0;

    // void OnEnable()
    // {
    //     PlayerStorage.DepositCoin += DepositCoin;
    // }

    // void OnDisable()
    // {
    //     PlayerStorage.DepositCoin -= DepositCoin;
    // }

    void DepositCoin(CoinBehaviour droppedCoin)
    {
        _totalCoinValue += droppedCoin.Value;
    }
}
