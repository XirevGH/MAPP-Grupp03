using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Game/Items/Weapon Data")]
public class WeaponDataSO : ItemDefinitionSO
{
    [Header("Weapon Specific Stats")]
    [SerializeField] private float baseDamage = 15f;
    [SerializeField] private AudioClip attackSound;

    public float BaseDamage => baseDamage;
    public AudioClip AttackSound => attackSound;

    public override string GetItemType() => "Weapon";
}