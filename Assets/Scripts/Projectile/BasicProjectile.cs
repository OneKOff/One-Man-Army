using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    [SerializeField] protected ProjectileBehaviour projectileBehaviour;
    [SerializeField] protected ProjectileData projectileData;
    
    private Transform _target;
    private Vector3 _targetLastPosition;

    private void Start()
    {
        projectileBehaviour.ProvideData(gameObject, projectileData);
    }

    private void Update()
    {
        projectileBehaviour.UpdateHandle();
    }
    
    public void AssignTarget(Transform target)
    {
        projectileBehaviour.AssignTargetHandle(target);
    }
    
    protected virtual void OnTriggerEnter(Collider other)
    {
        projectileBehaviour.OnTriggerEnterHandle(other);
    }
}
