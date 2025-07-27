using UnityEngine;

namespace Assets.Scripts.Items
{
    [CreateAssetMenu(fileName = "DummyAmmoData", menuName = "Items/AmmoData")]
    public class AmmoData : ItemData
    {
        // Ints for testing purposes
        [field: SerializeField]
        public int Type { get; private set; }
        // Add Special Rules
    }
}
