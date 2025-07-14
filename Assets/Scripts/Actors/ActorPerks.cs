using Scripts.Perks;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Actors
{
    [RequireComponent(typeof(ActorSpecialStats))]
    public class ActorPerks : MonoBehaviour
    {
        [SerializeField]
        private List<Perk> equippedPerks;

        public IReadOnlyList<Perk> EquippedPerks => equippedPerks;

        public void Awake()
        {

        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public bool EquipPerk(Perk perkToBeAdded)
        {
            if(equippedPerks.Contains(perkToBeAdded))
            {
                return false;
            }
            
            //If values were not set, do so using the default ones provided
            //from the provided ScriptableObject
            if(!perkToBeAdded.IsInitialized)
            {
                perkToBeAdded.InitializeFromDefault();
            }

            if(perkToBeAdded.CheckAllRequirements(gameObject))
            {
                equippedPerks.Add(perkToBeAdded);

                // Apply all effects of this perk to the actor
                foreach (var effect in perkToBeAdded.Effects)
                {
                    effect.ApplyEffect(gameObject);
                }

                Debug.Log($"Equipped perk: {perkToBeAdded.Name}");
                return true;
            }
            return false;
        }

        public bool UnequipPerk(Perk perk)
        {
            if (!equippedPerks.Contains(perk))
            {
                return false;
            }
                
            equippedPerks.Remove(perk);

            Debug.Log($"Unequipped perk: {perk.Name}");
            return true;
        }
    }
}

