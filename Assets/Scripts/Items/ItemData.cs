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
        [field: SerializeField]
        public float Value { get; private set; }
    }
}
