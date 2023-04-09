using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float flightSpeed = 10f;
    [SerializeField] private int damage = 20;
    
    private Transform _target;

    private void Update()
    {
        if (_target)
        {
            transform.position += (_target.position - transform.position).normalized * flightSpeed * Time.deltaTime;
        }
    }

    public void AssignTarget(Transform target)
    {
        _target = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent(out Enemy e)) return;
        
        Debug.Log("Proj Hit enemy");
        e.TakeDamage(damage);
            
        Destroy(gameObject);
    }
}