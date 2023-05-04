using UnityEngine;

[CreateAssetMenu(fileName = "ExplosiveProjectileBehaviour", menuName = "OneManArmy/Behaviour/ExplosiveProjectile", order = 0)]
public class ExplosiveProjectileBehaviour : ProjectileBehaviour
{
    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private float explosionRadius;
    [SerializeField] private float knockbackPower;
    
    public override void UpdateHandle()
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
            Explosion();
        }
    }

    public override void OnTriggerEnterHandle(Collider other)
    {
        
    }
    
    private void Explosion()
    {
        var colliders = Physics.OverlapSphere(_projectile.transform.position, explosionRadius);
        
        foreach (var col in colliders)
        {
            switch (_projectileData.TType)
            {
                case ProjectileData.TeamType.Player:
                    if (!col.gameObject.TryGetComponent(out Enemy e)) { return; }
                    e.TakeDamage(_projectileData.Damage);
                    if (col.gameObject.TryGetComponent(out Rigidbody rb))
                    {
                        rb.AddForce((col.transform.position - _projectile.transform.position) * knockbackPower, ForceMode.Impulse);
                    }
                    break;
            
                case ProjectileData.TeamType.Enemy:
                    if (!col.gameObject.TryGetComponent(out PlayerControl p)) { return; }
                    p.TakeDamage(_projectileData.Damage);
                    if (col.gameObject.TryGetComponent(out rb))
                    {
                        rb.AddForce((col.transform.position - _projectile.transform.position) * knockbackPower, ForceMode.Impulse);
                    }
                    break;
            
                case ProjectileData.TeamType.Neutral:
                    if (!col.gameObject.TryGetComponent(out IDamageable iDmg)) { return; }
                    iDmg.TakeDamage(_projectileData.Damage);
                    if (col.gameObject.TryGetComponent(out rb))
                    {
                        rb.AddForce((col.transform.position - _projectile.transform.position) * knockbackPower, ForceMode.Impulse);
                    }
                    break;
            }
        }

        var ps = Instantiate(explosionEffect, _projectile.transform.position, Quaternion.identity);
        ps.Play();
        
        Destroy(_projectile);
    }
}
