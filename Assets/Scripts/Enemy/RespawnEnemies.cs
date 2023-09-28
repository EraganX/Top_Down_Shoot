using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnEnemies : MonoBehaviour
{
    [Header("Respawn Locations")]
    [SerializeField] private Transform[] _RespawnLocation;

    [Header("Enemy")]
    [SerializeField] private float _spawnTime=0.5f, _difficulty = 3f;
    [SerializeField] private GameObject _EnemyPrefab;
    float _LastSpawnTime;
    PlayerScript _PlayerScript;
    bool isEnd = false;

    private void Awake()
    {
        _PlayerScript = FindAnyObjectByType<PlayerScript>();
    }


    private void Update()
    {
        if (_PlayerScript != null)
        {
            isEnd = _PlayerScript.isDead;

            if ((Time.time > _LastSpawnTime + _spawnTime) && !isEnd)
            {
                StartCoroutine(SpawnEnemies());
                _spawnTime = Random.Range(0.3f, _difficulty);
                _LastSpawnTime = Time.time;
            }
        }        
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(_spawnTime);
        int randomLoaction = Random.Range(0, _RespawnLocation.Length);
        Instantiate(_EnemyPrefab, _RespawnLocation[randomLoaction].position,Quaternion.identity);
    }
}
