using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject CoinPrefab;
    public GameObject[] ValidSpawnArea = new GameObject[4]; // Represent the coordinate boundaries that coins can spawn in. Indexes 0 and 3 represent the outer most boundaries; Index 1 and 2 represent internal boundary box i.e. the coin hole 
    private GameObject _tmpObj;

    private List<GameObject> _pooledObjsList = new List<GameObject>();

    private Vector3 _spawnTargetPos = new Vector3(0f, 0f, 0f);
    public CoinScriptable[] CoinTypes;

    private int _tempInt;

    void OnEnable()
    {
        Timer.SpawnNewCoin += SpawnCoinOnField;
    }

    void OnDisable()
    {
        Timer.SpawnNewCoin -= SpawnCoinOnField;
    }

    void Start()
    {
        InstantiateCoinPrefabPool(3);
    //     for(int i = 0; i < 100; i ++)
    //         SpawnCoinOnField();
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
        _tmpObj = Instantiate(CoinPrefab);
        _tmpObj.name = "Coin Prefab " + _pooledObjsList.Count;
        _tmpObj.transform.parent = transform;
        _pooledObjsList.Add(_tmpObj);
    }

    private void EnableCoinComponents(bool isActive)
    {
        _tmpObj.SetActive(isActive);   // Set coin spriterender to inactive to hide coin
        _tmpObj.GetComponent<Collider2D>().enabled = isActive;
    }
    
    public GameObject GetPooledCoin()
    {
        // Method returns a inactive coin gameobject prefab 
        for(int i = 0; i < _pooledObjsList.Count; i ++)
        {
            if(!_pooledObjsList[i].activeInHierarchy)
                return _pooledObjsList[i];
        }
        Debug.Log("No inactive coin  found - All pooled coins used. Instantiating extra coins");
        InstantiateCoinPrefab(true);
        return _pooledObjsList[_pooledObjsList.Count-1];
    }

    public void SpawnCoinOnField()
    {
        _tmpObj = GetPooledCoin();
        _tmpObj.GetComponent<CoinBehaviour>().CoinSO = SelectRandomCoinValue();
        _tmpObj.GetComponent<CoinBehaviour>().SetUpCoin();
        SelectValidSpawnCoord();
        EnableCoinComponents(true);
        _tmpObj.transform.position = _spawnTargetPos;
    }
    private void SelectValidSpawnCoord()
    {
        _spawnTargetPos[0] = Random.Range(ValidSpawnArea[0].transform.position.x, ValidSpawnArea[3].transform.position.x);
        _spawnTargetPos[1] = Random.Range(ValidSpawnArea[0].transform.position.y, ValidSpawnArea[3].transform.position.y);

        IsSpawnTargetInCoinHole(); 
    }

    private void IsSpawnTargetInCoinHole()
    {
        if(ValidSpawnArea[1].transform.position.x < _spawnTargetPos[0] && _spawnTargetPos[0] < ValidSpawnArea[2].transform.position.x)
        {
            while(ValidSpawnArea[2].transform.position.y < _spawnTargetPos[1] && _spawnTargetPos[1] < ValidSpawnArea[1].transform.position.y)
                _spawnTargetPos[1] = Random.Range(ValidSpawnArea[0].transform.position.y, ValidSpawnArea[3].transform.position.y);
        }
    }      

    private CoinScriptable SelectRandomCoinValue()
    {
        _tempInt =  Random.Range(0,100);

        if(_tempInt > 95)
            return CoinTypes[CoinTypes.Length-1];
        else if (_tempInt <= 95 && _tempInt > 85)
            return CoinTypes[2];
        else if (_tempInt <= 85 && _tempInt > 60)
            return CoinTypes[1];
        else
            return CoinTypes[0];
    } 
}
