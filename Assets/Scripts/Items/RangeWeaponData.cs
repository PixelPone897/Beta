using Assets.Scripts.Combat;
using Assets.Scripts.Items;
using NUnit.Framework;
using Scripts.Perks;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DummyRangeWeaponData", menuName = "Items/RangeWeaponData")]
public class RangeWeaponData : ItemData
{
    // Temporarily using ints here for testing purposes
    [field: SerializeField]
    public int Range { get; private set; }
    [field: SerializeField]
    public int Type { get; private set; }
    [field: SerializeField]
    public int MagSize { get; private set; }
    [field: SerializeField]
    public int RateOfFire { get; private set; }
    [field: SerializeField]
    public int AmmoType { get; private set; }
    [field: SerializeField]
    public SpecialRequirement StrengthRequirement { get; private set; }
    // Add Special Rules

    [field: SerializeReference, SubclassSelector]
    public List<CombatAction> combatActions { get; private set; }
}