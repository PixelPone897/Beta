using Assets.Scripts.Items;
using Scripts.Perks;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Actors
{
    /// <summary>
    /// Stores the Perks associated with an Actor.
    /// </summary>
    [RequireComponent(typeof(ActorSpecialStats))]
    public class ActorPerks : MonoBehaviour
    {
        /// <summary>
        /// All Perks equipped by an Actor.
        /// </summary>
        /// <seealso cref="Perk"/>
        [SerializeField]
        private List<Perk> equippedPerks;

        public IReadOnlyList<Perk> EquippedPerks => equippedPerks;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Equips a perk to an Actor.
        /// </summary>
        /// <param name="perkToBeAdded"></param>
        /// <returns>True- Perk was able to be equipped to Actor.
        /// False- Perk was not able to be equipped to Actor.</returns>
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

        /// <summary>
        /// Unequips Perk from an Actor.
        /// </summary>
        /// <param name="perk">Perk that is being removed from Actor.</param>
        /// <returns></returns>
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

