using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Coin", menuName = "Coin")]
public class CoinScriptable : ScriptableObject
{
    public Sprite CoinSprite;
    public int CoinValue, CoinWeight;
}
