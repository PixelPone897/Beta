using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Perks
{
    [System.Serializable]
    public class Perk
    {
        [SerializeField]
        private DefaultPerkInfo defaultInfo;

        [field: SerializeField]
        public string Name { get; private set; }
        [field: SerializeField, TextArea]
        public string Description { get; private set; }
        [field: SerializeField]
        public int CurrentRank { get; private set; }
        [field: SerializeField]
        public int MaxRank { get; private set; }
        [field: SerializeField]
        public Sprite Image { get; set; }
        [field: SerializeReference, SubclassSelector]
        public List<IRequirement> Requirements { get; private set; }

        [field: SerializeReference, SubclassSelector]
        public List<IPerkEffect> Effects { get; private set; }

        //Quick helper to check if initialization was done
        public bool IsInitialized => !string.IsNullOrEmpty(Name);
        public bool CanUpgrade => CurrentRank < MaxRank;
        //Static factory method for easy runtime creation
        public static Perk CreatePerk(DefaultPerkInfo info) => new Perk(info);

        public Perk(DefaultPerkInfo info)
        {
            defaultInfo = info;
            InitializeFromDefault();
        }

        //Default constructor required by Unity for deserialization
        public Perk()
        {
            //NOTE!- defaultInfo will be null here when Unity deserializes this class
        }

        // Unity does not call constructor for serialized classes
        // So we have to force it by calling a method
        // Constructor for manual creation (won't be called by Unity during serialization)
        public void InitializeFromDefault()
        {
            if(defaultInfo == null)
            {
                Debug.LogWarning("DefaultPerkInfo not assigned!");
                return;
            }

            Name = defaultInfo.Name;
            Description = defaultInfo.Description;
            MaxRank = defaultInfo.MaxRank;
            Image = defaultInfo.Image;
            Requirements = new List<IRequirement>(defaultInfo.Requirements);
            Effects = new List<IPerkEffect>(defaultInfo.Effects);
        }

        public void Upgrade()
        {
            if (!CanUpgrade)
            {
                Debug.LogWarning($"Perk '{Name}' is already at max rank!");
                return;
            }

            CurrentRank++;
        }

        public bool CheckAllRequirements(GameObject actor)
        {
            foreach(var requirement in Requirements)
            {
                if(!requirement.CheckRequirement(actor))
                {
                    return false;
                }
            }

            return true;
        }
    }
}