using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyWave", menuName = "OneManArmy/EnemyWave")]
public class EnemyWave : ScriptableObject
{
    [Serializable]
    public struct EnemyGroup
    {
        public Enemy EnemyType;
        public int Amount;
        public float InnerDelay;
        public float RelativeStartTime;
        public int[] spawnIndexes;
    }

    [SerializeField] private List<EnemyGroup> enemyGroups;
    public List<EnemyGroup> EnemyGroups => enemyGroups;
    [SerializeField] private float waveStartTime;
    public float WaveStartTime => waveStartTime;
}
