using System;
using Player;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float speed = 5f;
    
    private int _health;
    private Transform _playerTarget;

    private void Awake()
    {
        _health = maxHealth;
        _playerTarget = FindObjectOfType<PlayerControl>().transform;
    }

    private void Update()
    {
        transform.position += (_playerTarget.position - transform.position).normalized * speed * Time.deltaTime;
    }

    public void TakeDamage(int amount)
    {
        _health -= amount;

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
