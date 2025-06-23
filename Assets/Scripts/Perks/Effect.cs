using Scripts;
using Scripts.Actors;
using System.Collections.Generic;
using UnityEngine;
using static Scripts.Constants;

namespace Scripts.Perks
{
    public interface IPerkEffect
    {
        public abstract void ApplyEffect(GameObject actor);
    }

    [System.Serializable]
    public class PlusFivePerkEffect : IPerkEffect
    {
        [SerializeField]
        private List<SkillName> skillsToAddTo;
        [SerializeField]
        private float toAddToPerk;
        [SerializeField]
        private SkillName selectedPerk;

        public PlusFivePerkEffect() { }

        public void ApplyEffect(GameObject actor)
        {
            if (actor.TryGetComponent<ActorSpecialStats>(out var stats))
            {
                var modifier = new StatModifier(toAddToPerk, true);
                stats.Skills[selectedPerk].AddPlusModifier(modifier);
            }
        }
    }
}
