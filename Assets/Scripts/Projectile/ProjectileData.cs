using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "OneManArmy/Projectile", order = 0)]
public class ProjectileData : ScriptableObject
{
    [Serializable]
    public enum TeamType { None, Player, Enemy, Neutral }

    [SerializeField] private TeamType teamType;
    public TeamType TType => teamType;
    [SerializeField] private int damage;
    public int Damage => damage;
    [SerializeField] private float projectileSpeed;
    public float ProjectileSpeed => projectileSpeed;
}
