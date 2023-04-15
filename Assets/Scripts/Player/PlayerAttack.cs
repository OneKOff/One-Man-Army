using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float duration = 0.6f;
    [SerializeField] private int damage = 50;
    [SerializeField] private float pushPower = 5f;
    [SerializeField] private ParticleSystem ps;

    private List<Enemy> _enemiesHit = new List<Enemy>();
    private float _remainingDuration;

    private void Start()
    {
        _remainingDuration = duration;
        ps.Play();
    }

    private void Update()
    {
        _remainingDuration -= Time.deltaTime;
        if (_remainingDuration <= 0) { Destroy(gameObject); }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent(out Enemy e)) { return; }
        
        if (_enemiesHit.Contains(e)) { return; }
        
        _enemiesHit.Add(e);
        Debug.Log("PlayerAttack: Hit enemy!");
        e.TakeDamage(damage);
        
        if (e.TryGetComponent(out Rigidbody rb))
        {
            rb.AddForce(transform.forward * pushPower, ForceMode.Impulse);    
        }
    }
}
