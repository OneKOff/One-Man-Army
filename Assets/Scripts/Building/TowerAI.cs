using System;
using UnityEngine;

public class TowerAI : MonoBehaviour
{
    [SerializeField] private BasicProjectile projectilePrefab;
    [SerializeField] private float targetRadius = 5f;
    [SerializeField] private float shootCooldown = 1f;
    [SerializeField] private Transform shootPoint;

    private Transform _shootingTarget;
    private float _currentCooldown;

    private void Start()
    {
        _currentCooldown = shootCooldown;
    }
    
    private void Update()
    {
        if (_currentCooldown > 0f)
        {
            _currentCooldown -= Time.deltaTime;
        }
        else
        {
            Collider[] colliders = new Collider[10];
            Physics.OverlapSphereNonAlloc(transform.position, targetRadius, colliders);
            
            if (colliders.Length == 0) { return; }
            
            foreach (var col in colliders)
            {
                if (!col || !col.gameObject.TryGetComponent(out Enemy e)) continue;
                
                var projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
                projectile.AssignTarget(e.transform);
                    
                _currentCooldown = shootCooldown;
                    
                break;
            }
            
        }
    }
}
