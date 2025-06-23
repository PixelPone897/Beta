using Scripts.Actors;
using UnityEngine;
using static Scripts.Constants;

namespace Scripts.Perks
{
    public interface IRequirement
    {
        public abstract bool CheckRequirement(GameObject actor);
    }

    [System.Serializable]
    public class LevelRequirement : IRequirement
    {
        [SerializeField]
        private int requiredLevel;

        public bool CheckRequirement(GameObject actor)
        {
            if (actor.TryGetComponent<ActorSpecialStats>(out var stats))
            {
                if (stats.Level >= requiredLevel)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }

    [System.Serializable]
    public class SpecialRequirement : IRequirement
    {
        [SerializeField]
        private SpecialName specialToCheck;
        [SerializeField]
        private int specialReq;

        public bool CheckRequirement(GameObject actor)
        {
            if (actor.TryGetComponent<ActorSpecialStats>(out var stats))
            {
                float valueToCheck = 0;
                switch(specialToCheck)
                {
                    case SpecialName.STRENGTH:
                    default:
                        valueToCheck = stats.Strength.BaseValue;
                        break;
                    case SpecialName.PERCEPTION:
                        valueToCheck = stats.Perception.BaseValue;
                        break;
                    case SpecialName.ENDURANCE:
                        valueToCheck = stats.Endurance.BaseValue;
                        break;
                    case SpecialName.CHARISMA:
                        valueToCheck = stats.Charisma.BaseValue;
                        break;
                    case SpecialName.INTELLIGENCE:
                        valueToCheck = stats.Endurance.BaseValue;
                        break;
                    case SpecialName.AGILITY:
                        valueToCheck = stats.Agility.BaseValue;
                        break;
                    case SpecialName.LUCK:
                        valueToCheck = stats.Luck.BaseValue;
                        break;
                }

                if (valueToCheck >= specialReq)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }

    [System.Serializable]
    public class SkillRequirement : IRequirement
    {
        [SerializeField]
        private SkillName skillName;
        [SerializeField]
        private int skillAmount;

        public bool CheckRequirement(GameObject actor)
        {
            if (actor.TryGetComponent<ActorSpecialStats>(out var stats))
            {
                if (stats.Skills[skillName].BaseValue >= skillAmount)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}