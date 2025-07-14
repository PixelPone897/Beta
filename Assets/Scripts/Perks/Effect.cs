using Scripts.Actors;
using System.Collections.Generic;
using UnityEngine;
using static Scripts.Constants;

namespace Scripts.Perks
{
    public interface IPerkEffect
    {
        public abstract void ApplyEffect(GameObject actor);
        public abstract void RemoveEffect(GameObject actor);
    }

    [System.Serializable]
    public class PlusFiveSkillEffect : IPerkEffect
    {
        [SerializeField]
        private List<SkillName> skillsToAddTo;
        [SerializeField]
        private float toAddToPerk;
        [SerializeField]
        private SkillName selectedPerk;

        public PlusFiveSkillEffect() { }

        public void ApplyEffect(GameObject actor)
        {
            if (actor.TryGetComponent<ActorSpecialStats>(out var stats))
            {
                var modifier = new StatModifier(toAddToPerk, true);
                stats.Skills[selectedPerk].AddPlusModifier(modifier);
            }
        }

        public void RemoveEffect(GameObject actor)
        {
            if (actor.TryGetComponent<ActorSpecialStats>(out var stats))
            {
                var modifier = new StatModifier(-toAddToPerk, true);
                stats.Skills[selectedPerk].RemovePlusModifier(modifier);
            }
        }
    }

    [System.Serializable]
    public class ModifySpecialEffect: IPerkEffect
    {
        [SerializeField]
        private SpecialName specialName;
        [field: SerializeField]
        public List<StatModifier> PlusModifiers;
        [field: SerializeField]
        public List<StatModifier> MultiplyModifiers;

        public ModifySpecialEffect() { }
        public ModifySpecialEffect(SpecialName specialName)
        {
            this.specialName = specialName;
            PlusModifiers = new List<StatModifier>();
            MultiplyModifiers = new List<StatModifier>();
        }

        public Stat GetSpecialStat(ActorSpecialStats stats)
        {
            Stat specialStat;
            switch (specialName)
            {
                case SpecialName.STRENGTH:
                    specialStat = stats.Strength;
                    break;
                case SpecialName.PERCEPTION:
                    specialStat = stats.Perception;
                    break;
                case SpecialName.ENDURANCE:
                    specialStat = stats.Endurance;
                    break;
                case SpecialName.CHARISMA:
                    specialStat = stats.Charisma;
                    break;
                case SpecialName.INTELLIGENCE:
                    specialStat = stats.Intelligence;
                    break;
                case SpecialName.AGILITY:
                    specialStat = stats.Agility;
                    break;
                case SpecialName.LUCK:
                    specialStat = stats.Luck;
                    break;
                default:
                    specialStat = stats.Strength;
                    break;
            }
            return specialStat;
        }

        public void ApplyEffect(GameObject actor)
        {
            if (actor.TryGetComponent<ActorSpecialStats>(out var stats))
            {

                Stat statToModify = GetSpecialStat(stats);

                foreach (var modifier in PlusModifiers)
                {
                    statToModify.AddPlusModifier(modifier);
                }

                foreach (var modifier in MultiplyModifiers)
                {
                    statToModify.RemoveMultiplyModifier(modifier);
                }
            }
        }

        public void RemoveEffect(GameObject actor)
        {
            if (actor.TryGetComponent<ActorSpecialStats>(out var stats))
            {

                Stat statToModify = GetSpecialStat(stats);

                foreach (var modifier in PlusModifiers)
                {
                    statToModify.RemovePlusModifier(modifier);
                }

                foreach (var modifier in MultiplyModifiers)
                {
                    statToModify.RemoveMultiplyModifier(modifier);
                }
            }
        }
    }
}
