using Scripts.Perks;
using System.Collections.Generic;
using UnityEngine;
using static Scripts.Constants;

namespace Assets.Scripts.Items
{
    [CreateAssetMenu(fileName = "DummyArmorData", menuName = "Items/ArmorData")]
    public class ArmorData : ItemData
    {
        [field: SerializeField]
        public int DamageThreshold { get; private set; }
        [field: SerializeField]
        public string Faction {  get; private set; }
        [field: SerializeField]
        public List<LimbName> ValidLimbs { get; private set; }
        [field: SerializeReference,  SubclassSelector]
        public List<IPerkEffect> Effects { get; private set; }
        // Add Special Rules

        public void OnValidate()
        {
            if(ValidLimbs == null ||  ValidLimbs.Count == 0)
            {
                Debug.LogWarning("This armor is not assigned to at least one limb!");
            }
        }
    }
}

