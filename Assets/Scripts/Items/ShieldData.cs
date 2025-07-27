using Scripts.Perks;
using UnityEngine;

namespace Assets.Scripts.Items
{
    [CreateAssetMenu(fileName = "DummyShieldData", menuName = "Items/ShieldData")]
    public class ShieldData : ItemData
    {
        [field: SerializeField]
        public int BashDamage { get; private set; }
        [field: SerializeField]
        public SpecialRequirement StrengthRequirement { get; private set; }
        [field: SerializeField]
        public int ApCost { get; private set; }
        [field: SerializeField]
        public float BlockChance { get; private set; }
        [field: SerializeField]
        public int MeleeDamageThreshold { get; private set; }
        [field: SerializeField]
        public int ProjectileDamageThreshold { get; private set; }
        [field: SerializeField]
        public int MagicDamageThreshold { get; private set; }
        // Add Special Rules
    }
}

