using Scripts.Perks;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Items
{
    [CreateAssetMenu(fileName = "DummyItemData", menuName = "Items/ItemData")]
    public class ItemData: ScriptableObject
    {
        [field: SerializeField]
        public string Name { get; private set; }
        [field: SerializeField, TextArea]
        public string Description { get; private set; }
        [field: SerializeField]
        public Sprite Icon { get; private set; }
        [field: SerializeField]
        public bool CanStack { get; private set; }

        [field: SerializeField]
        public float Weight { get; private set; }
    }

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
        [field: SerializeField, SubclassSelector]
        public List<IRequirement> Requirements { get; private set; }
    }
}
