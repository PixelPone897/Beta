using UnityEngine;

namespace Assets.Scripts.Items
{
    public abstract class ItemData: ScriptableObject
    {
        [field: SerializeField]
        public string Name { get; private set; }
        [field: SerializeField, TextArea]
        public string Description { get; private set; }
        [field: SerializeField]
        public Sprite Icon { get; private set; }
        [field: SerializeField]
        public bool CanStack { get; private set; }
    }
}
