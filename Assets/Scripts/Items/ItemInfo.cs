using System;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public class ItemInfo: MonoBehaviour
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
        public string UniqueID;

        // GUIDs are only on Prefabs, SOs through .meta files and only work in the Editor
        // To make items have their own identifier, need to manually create one

        public void Awake()
        {
            UniqueID = Guid.NewGuid().ToString();
        }
    }
}
