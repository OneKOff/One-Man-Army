using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveController : MonoBehaviour
{
    [SerializeField] private List<EnemyWave> enemyWaves = new List<EnemyWave>();
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>(); 

    private int _waveIndex = -1;
    private float _currentTime = 0f;
    private float _lastCurrentTime = -0.2f;
    
    private void Update()
    {
        _lastCurrentTime = _currentTime;
        _currentTime += Time.deltaTime;

        if (_waveIndex + 1 < enemyWaves.Count && enemyWaves[_waveIndex + 1].WaveStartTime < _currentTime)
        {
            StartNewWave(_waveIndex + 1);
        }
        
        CheckForNewSpawn();
    }

    public void StartNewWave(int i)
    {
        _waveIndex = i;
        Debug.Log($"Wave {_waveIndex} started");
    }

    public void CheckForNewSpawn()
    {
        for (var i = 0; i <= _waveIndex; i++)
        {
            var waveTime = _currentTime - enemyWaves[i].WaveStartTime;
            foreach (var group in enemyWaves[i].EnemyGroups)
            {
                var index = (int)((waveTime - group.RelativeStartTime) / group.InnerDelay);
                if (index < 0 || index >= group.Amount) { continue; }
                
                Debug.Log($"EnemyWaveController: Index {index}, Wave: {_waveIndex}");
                
                if (IsInTimeFrame(group.RelativeStartTime + index * group.InnerDelay, 
                    _lastCurrentTime - enemyWaves[i].WaveStartTime, waveTime))
                {
                    Instantiate(group.EnemyType,
                        spawnPoints[group.spawnIndexes[Random.Range(0, group.spawnIndexes.Length - 1)]].position, 
                        Quaternion.identity, 
                        transform);
                    Debug.Log($"EnemyWaveController: Spawned {group.EnemyType.name} number {index}, wave {i}");
                }
            }
        }
    }

    public bool IsInTimeFrame(float checkedTime, float lastCurTime, float curTime)
    {
        return checkedTime >= lastCurTime && checkedTime <= curTime;
    }
}
