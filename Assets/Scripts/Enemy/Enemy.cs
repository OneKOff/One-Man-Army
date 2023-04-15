using System;
using Player;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform healthBarPosition;

    private HealthBar _assignedHealthBar;
    private int _health;
    private Transform _playerTarget;

    private void Start()
    {
        _health = maxHealth;
        _playerTarget = FindObjectOfType<PlayerControl>().transform;
        AssignHealthBar(EnemyManager.Instance.GetNewHealthBar());
    }

    private void Update()
    {
        rb.velocity = Vector3.Lerp(rb.velocity, (_playerTarget.position - transform.position).normalized * speed, Time.deltaTime);

        if (_assignedHealthBar) _assignedHealthBar.transform.position = healthBarPosition.position;
    }
    
    public void AssignHealthBar(HealthBar healthBar)
    {
        _assignedHealthBar = healthBar;
        healthBar.SetMaxValue(maxHealth);
        healthBar.UpdateBar(maxHealth - _health);
        healthBar.transform.position = healthBarPosition.position;
    }

    private void OnDestroy()
    {
        if (!_assignedHealthBar) { return; }
        Destroy(_assignedHealthBar.gameObject);
    }

    public void TakeDamage(int amount)
    {
        _health -= amount;
        if (_assignedHealthBar) _assignedHealthBar.UpdateBar(amount);

        if (_health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
