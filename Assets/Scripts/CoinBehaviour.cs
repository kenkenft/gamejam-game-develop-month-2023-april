using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    [SerializeField] public CoinScriptable CoinSO;
    [SerializeField] private SpriteRenderer _coinSpriteRenderer;
    public int Value, Weight;

    public void SetUpCoin()
    {
        _coinSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _coinSpriteRenderer.sprite = CoinSO.CoinSprite;
        Value = CoinSO.CoinValue;
        Weight = CoinSO.CoinWeight; 
    }

    public void ToggleCoinSprite(bool state)
    {
        _coinSpriteRenderer.enabled = state;
    }
}
