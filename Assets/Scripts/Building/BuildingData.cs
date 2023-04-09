using UnityEngine;

[CreateAssetMenu(fileName = "Building", menuName = "OneManArmy/Building", order = 0)]
public class BuildingData : ScriptableObject
{
    [SerializeField] private GameObject model;
}
