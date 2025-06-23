using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Perks
{
    [CreateAssetMenu(fileName="PerkInfo", menuName="Actors/PerkInfo")]
    public class DefaultPerkInfo : ScriptableObject
    {
        [field: SerializeField]
        public string Name { get; private set; }
        [field: SerializeField, TextArea]
        public string Description { get; private set; }
        [field: SerializeField]
        public int MaxRank { get; private set; }
        [field: SerializeField]
        public Sprite Image { get; set; }
        [field: SerializeReference, SubclassSelector]
        public List<IRequirement> Requirements { get; private set; }
        [field: SerializeReference, SubclassSelector]
        public List<IPerkEffect> Effects { get; private set; }
    }
}