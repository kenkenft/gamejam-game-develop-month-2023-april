using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    private GameObject tmpObj;

    private List<GameObject> pooledObjsList = new List<GameObject>();

    void Start()
    {
        InstantiateCoinPrefabPool(50);
    }

    public void InstantiateCoinPrefabPool(int expectedUpperLimit)
    {
        // Method that instantiates initial pool of coin prefab objects
        for(int i = 0; i < expectedUpperLimit; i++)
            InstantiateCoinPrefab(false);

    }

    private void InstantiateCoinPrefab(bool isActive)
    {
        // Instantiate coin prefab and add to pooledObjsList 
        tmpObj = Instantiate(coinPrefab);
        tmpObj.name = "Coin Prefab " + pooledObjsList.Count;
        tmpObj.SetActive(isActive);   // Set coin spriterender to inactive to hide coin
        tmpObj.GetComponent<Collider2D>().enabled = isActive;
        tmpObj.transform.parent = transform;
        pooledObjsList.Add(tmpObj);
    }
    
    public GameObject GetPooledCoin()
    {
        // Method returns a inactive coin gameobject prefab 
        for(int i = 0; i < pooledObjsList.Count; i ++)
        {
            if(!pooledObjsList[i].activeInHierarchy)
                return pooledObjsList[i];
        }
        Debug.Log("No inactive coin  found - All pooled coins used. Instantiating extra coins");
        InstantiateCoinPrefab(true);
        return pooledObjsList[pooledObjsList.Count-1];
    }

}
