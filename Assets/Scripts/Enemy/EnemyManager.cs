using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    [SerializeField] private HealthBar healthBarPrefab;
    [SerializeField] private Canvas canvasWorld;

    private List<Enemy> _enemies = new List<Enemy>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    
    public void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }

    public HealthBar GetNewHealthBar()
    {
        return Instantiate(healthBarPrefab, canvasWorld.transform);
    }
}
