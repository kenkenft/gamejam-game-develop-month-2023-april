using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStorage : MonoBehaviour
{
    [SerializeField] private int _coinCapacity = 10;
    [SerializeField] private List<GameObject>  _coinsCollected = new List<GameObject>(){};

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
