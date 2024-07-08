using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "CarData", menuName = "ScriptableObject/CarData", order = 1)]
public class CarData : ScriptableObject
{
    [SerializeField] public List<CarItem> CarItems;
}
[System.Serializable]
public class CarItem
{
    public GameObject CarPrefab;
    public float MoveSpeed;
    public float RotationSpeed;
}
