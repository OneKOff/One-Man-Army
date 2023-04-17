using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ResourceMapGeneration : MonoBehaviour
{
    [SerializeField] private List<BasicCollectible> collectibles = new List<BasicCollectible>();
    [SerializeField] private float spawnRadius = 20;
    [SerializeField] private int amount = 10;

    private void Start()
    {
        GenerateResources();
    }

    private void GenerateResources()
    {
        var rangeVector = new Vector3(Random.Range(-spawnRadius, spawnRadius), 0,
            Random.Range(-spawnRadius, spawnRadius));
        
        rangeVector = UtilityForVector.ClampVector(rangeVector, spawnRadius);
        var resPosition = transform.position + rangeVector;
        Instantiate(collectibles[Random.Range(0, collectibles.Count - 1)], resPosition, Quaternion.identity, transform);
    }
}
