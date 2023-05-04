using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileBehaviour", menuName = "OneManArmy/Behaviour/Projectile")]
public class ProjectileBehaviour : ScriptableObject
{
    protected Transform _target;
    protected Vector3 _targetLastPosition;

    protected GameObject _projectile;
    protected ProjectileData _projectileData;

    public virtual void ProvideData(GameObject projectile, ProjectileData projectileData)
    {
        _projectile = projectile;
        _projectileData = projectileData;
    }
    
    public virtual void UpdateHandle()
    {
        if (_target)
        {
            _targetLastPosition = _target.position;
        }

        var position = _projectile.transform.position;
        
        position += (_targetLastPosition - position).normalized * _projectileData.ProjectileSpeed * Time.deltaTime;
        _projectile.transform.position = position;

        if ((_projectile.transform.position - _targetLastPosition).magnitude < 0.2f)
        {
            Destroy(_projectile);
        }
    }
    
    public virtual void AssignTargetHandle(Transform target)
    {
        _target = target;
    }

    public virtual void OnTriggerEnterHandle(Collider other)
    {
        switch (_projectileData.TType)
        {
            case ProjectileData.TeamType.Player:
                if (!other.gameObject.TryGetComponent(out Enemy e)) { return; }
                e.TakeDamage(_projectileData.Damage);
                Destroy(_projectile);
                break;
            
            case ProjectileData.TeamType.Enemy:
                if (!other.gameObject.TryGetComponent(out PlayerControl p)) { return; }
                p.TakeDamage(_projectileData.Damage);
                Destroy(_projectile);
                break;
            
            case ProjectileData.TeamType.Neutral:
                if (!other.gameObject.TryGetComponent(out IDamageable iDmg)) { return; }
                iDmg.TakeDamage(_projectileData.Damage);
                Destroy(_projectile);
                break;
        }
    }
}
