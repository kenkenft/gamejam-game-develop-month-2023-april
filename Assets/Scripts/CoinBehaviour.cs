using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    [SerializeField] public CoinScriptable CoinSO;
    [SerializeField] private SpriteRenderer _coinSpriteRenderer;
    public int Value, Weight, DespawnTimerCurrent = 0, DespawnTimerMax = 0;

    // void Start()
    // {
    //     SetUpCoin();
    // }
    
    public void SetUpCoin()
    {
        _coinSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _coinSpriteRenderer.sprite = CoinSO.CoinSprite;
        _coinSpriteRenderer.color = CoinSO.CoinColor;
        Value = CoinSO.CoinValue;
        Weight = CoinSO.CoinWeight;

        DespawnTimerCurrent = 0;
        DespawnTimerMax = CoinSO.CoinDespawnTime;
    }

    public void ToggleCoinSprite(bool state)
    {
        _coinSpriteRenderer.enabled = state;
    }
}
