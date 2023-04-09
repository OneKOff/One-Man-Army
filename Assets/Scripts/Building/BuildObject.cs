using System;
using UnityEngine;

public class BuildObject : MonoBehaviour
{
    [SerializeField] private TowerAI buildingToBuild;
    [SerializeField] private LayerMask buildingMask;

    private void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out RaycastHit hit, 1000f, buildingMask))
        {
            Instantiate(buildingToBuild, hit.point, Quaternion.identity);
        }
    }
}
