using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObject/WeaponData", order = 2)]
public class WeaponData : ScriptableObject
{
    [SerializeField] public List<WeaponStats> Weapon;
}
[System.Serializable]
public class WeaponStats
{
    public GameObject Gun;
    public GameObject Bullet;
    public Position position;
    public float AttackSpeed;
    public float Damage;
}
