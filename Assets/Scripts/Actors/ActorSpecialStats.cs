using System.Collections.Generic;
using UnityEngine;
using static Scripts.Constants;

namespace Scripts.Actors
{
    /// <summary>
    /// Represents the core stats of an Actor.
    /// </summary>
    /// <remarks>
    /// This class will get refractored into multiple components as system expands.
    /// Values might get moved to a "BaseActorStats" like class for prototype pattern
    /// in the future as well.
    /// </remarks>
    public class ActorSpecialStats : MonoBehaviour
    {

        [field: SerializeField, Header("General Info")]
        public string ActorName { get; private set; }
        [field: SerializeField]
        public int Level { get; private set; }
        [field: SerializeField]
        public int Experience { get; private set; }

        public int ExpToNext { get; private set; }
        [field: SerializeField]
        public ActorRace Race { get; private set; }
        [field: SerializeField]
        public int Age { get; private set; }

        [field: SerializeField]
        public ActorGender Gender { get; private set; }

        [field: SerializeField, Header("SPECIAL Stats")]
        public Stat Strength { get; private set; }
        [field: SerializeField]
        public Stat Perception { get; private set; }
        [field: SerializeField]
        public Stat Endurance { get; private set; }
        [field: SerializeField]
        public Stat Charisma { get; private set; }
        [field: SerializeField]
        public Stat Intelligence { get; private set; }
        [field: SerializeField]
        public Stat Agility { get; private set; }
        [field: SerializeField]
        public Stat Luck { get; private set; }

        [field: Header("Skills")]
        public Dictionary<SkillName, Stat> Skills;

        public void Awake()
        {
            ExpToNext = Level * (Level + 1) / 2 * 1000;
            Skills = new Dictionary<SkillName, Stat>();

            // Set default Skill Values based on formulas
            // Need to explicitly set each one to separate new objects as we don't want skills to share Stat objects
            Skills[SkillName.UNARMED] = 
                new Stat(Mathf.CeilToInt(Endurance.BaseValue + Agility.BaseValue + (Luck.BaseValue / 2)));
            Skills[SkillName.THROWN] =
                new Stat(Mathf.CeilToInt(Strength.BaseValue + Agility.BaseValue + (Luck.BaseValue / 2)));
            Skills[SkillName.MEW] =
                new Stat(Mathf.CeilToInt((Perception.BaseValue * 2) + (Luck.BaseValue / 2)));
            Skills[SkillName.MELEE] =
                new Stat(Mathf.CeilToInt(Strength.BaseValue + Agility.BaseValue + (Luck.BaseValue / 2)));
            Skills[SkillName.FIREARMS] =
                new Stat(Mathf.CeilToInt(Perception.BaseValue + Agility.BaseValue + (Luck.BaseValue / 2)));
            Skills[SkillName.EXPLOSIVES] =
                new Stat(Mathf.CeilToInt((Perception.BaseValue * 2) + (Luck.BaseValue / 2)));
            Skills[SkillName.BATTLE_SADDLES] =
                new Stat(Mathf.CeilToInt(Endurance.BaseValue + Perception.BaseValue + (Luck.BaseValue / 2)));
            Skills[SkillName.ALCH_SUR_TRAPS] =
                new Stat(Mathf.CeilToInt(Intelligence.BaseValue + Endurance.BaseValue + Perception.BaseValue
                + (Luck.BaseValue / 2)) - 5);
            Skills[SkillName.BLUFF_INTIMID] =
                new Stat(Mathf.CeilToInt((Charisma.BaseValue * 2) + (Luck.BaseValue / 2)));
            Skills[SkillName.NEGOT_SEDUCT] =
                new Stat(Mathf.CeilToInt((Charisma.BaseValue * 2) + (Luck.BaseValue / 2)));
            Skills[SkillName.BARTER] =
                new Stat(Mathf.CeilToInt((Charisma.BaseValue * 2) + (Luck.BaseValue / 2)));
            Skills[SkillName.SNEAK] =
                new Stat(Mathf.CeilToInt((Agility.BaseValue * 2) + (Luck.BaseValue / 2)));
            Skills[SkillName.LOCKPICK] =
                new Stat(Mathf.CeilToInt(Charisma.BaseValue + Agility.BaseValue + (Luck.BaseValue / 2)));
            Skills[SkillName.SLIGHT_OF_HOOF] =
                new Stat(Mathf.CeilToInt(Charisma.BaseValue + Agility.BaseValue + (Luck.BaseValue / 2)));
            Skills[SkillName.HACKING_TECH] =
                new Stat(Mathf.CeilToInt((Intelligence.BaseValue * 2) + (Luck.BaseValue / 2)));
            Skills[SkillName.CHEMISTRY] =
                new Stat(Mathf.CeilToInt((Intelligence.BaseValue * 2) + (Luck.BaseValue / 2)));
            Skills[SkillName.MEDICINE] =
                new Stat(Mathf.CeilToInt((Intelligence.BaseValue * 2) + (Luck.BaseValue / 2)));
            Skills[SkillName.ACADEMICS_LORE] =
                new Stat(Mathf.CeilToInt((Intelligence.BaseValue * 2) + (Luck.BaseValue / 2)));
            Skills[SkillName.REPAIR_MECH] =
                new Stat(Mathf.CeilToInt((Intelligence.BaseValue * 2) + (Luck.BaseValue / 2)));
            Skills[SkillName.GAMBLING] =
                new Stat(Mathf.CeilToInt(Luck.BaseValue * 2 + 3));
            Skills[SkillName.ATHLETICS] =
                new Stat(Mathf.CeilToInt(Strength.BaseValue + Endurance.BaseValue + Agility.BaseValue));
            Skills[SkillName.PROFESSION] =
                new Stat(Mathf.CeilToInt((Charisma.BaseValue * 2) + (Luck.BaseValue / 2)));
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
