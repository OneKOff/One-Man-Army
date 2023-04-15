using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildObject : MonoBehaviour
{
    [SerializeField] private List<GameObject> buildingsToBuild;
    [SerializeField] private LayerMask buildingMask;

    private int selectedBuildingIndex = -1;
    private Camera _cam;
    private Ray _ray;

    private void Start()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            selectedBuildingIndex = 0;
        }
        if (Input.GetKeyDown("2"))
        {
            selectedBuildingIndex = 1;
        }

        _ray = _cam.ScreenPointToRay(Input.mousePosition);
        
        if (selectedBuildingIndex < 0 || selectedBuildingIndex > buildingsToBuild.Count - 1) { return; }
        
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(_ray, out RaycastHit hit, 1000f, buildingMask))
        {
            Instantiate(buildingsToBuild[selectedBuildingIndex], hit.point, Quaternion.identity);
            ResetBuildingIndex();
        }
    }

    public void ChangeBuilding(int index)
    {
        selectedBuildingIndex = index;
    }

    public void ResetBuildingIndex()
    {
        selectedBuildingIndex = -1;
    }
}
