using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    [SerializeField] protected ProjectileData projectileData;
    
    private Transform _target;
    private Vector3 _targetLastPosition;
    
    private void Update()
    {
        if (_target)
        {
            _targetLastPosition = _target.position;
        }
        
        transform.position += (_targetLastPosition - transform.position).normalized * projectileData.ProjectileSpeed * Time.deltaTime;

        if ((transform.position - _targetLastPosition).magnitude < 0.2f)
        {
            Destroy(gameObject);
        }
    }
    
    public void AssignTarget(Transform target)
    {
        _target = target;
    }
    
    protected virtual void OnTriggerEnter(Collider other)
    {
        switch (projectileData.TType)
        {
            case ProjectileData.TeamType.Player:
                if (!other.gameObject.TryGetComponent(out Enemy e)) { return; }
                e.TakeDamage(projectileData.Damage);
                Destroy(gameObject);
                break;
            
            case ProjectileData.TeamType.Enemy:
                if (!other.gameObject.TryGetComponent(out PlayerControl p)) { return; }
                p.TakeDamage(projectileData.Damage);
                Destroy(gameObject);
                break;
            
            case ProjectileData.TeamType.Neutral:
                if (!other.gameObject.TryGetComponent(out IDamageable iDmg)) { return; }
                iDmg.TakeDamage(projectileData.Damage);
                Destroy(gameObject);
                break;
        }
    }
}
