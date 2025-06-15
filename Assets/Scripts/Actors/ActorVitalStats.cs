using UnityEngine;
using static Scripts.Constants;

// Will get refractored into multiple components as system expands
// Values will also be moved to the BaseActorStats ScriptableObject in the future
namespace Scripts.Actors
{
    /// <summary>
    /// Stores an Actor's vital statistics such as name, health, and other attributes.
    /// </summary>
    public class ActorVitalStats : MonoBehaviour
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
 
        [field: SerializeField, Header("Skills")]
        // Should I create another class specifically for Skills
        // that holds row information for each Skill as it appears in the
        // Google Sheet?
        public Stat Unarmed { get; private set; }
        [field: SerializeField]
        public Stat Thrown { get; private set; }
        [field: SerializeField]
        public Stat MagicialEnergyWeapons { get; private set; }
        [field: SerializeField]
        public Stat Melee { get; private set; }
        [field: SerializeField]
        public Stat Firearms { get; private set; }
        [field: SerializeField]
        public Stat Explosives { get; private set; }
        [field: SerializeField]
        public Stat BattleSaddles { get; private set; }
        [field: SerializeField]
        public Stat Alchemy { get; private set; }
        [field: SerializeField]
        public Stat Survivalism { get; private set; }

        [field: SerializeField]
        public Stat Traps { get; private set; }
        [field: SerializeField]
        public Stat Bluff { get; private set; }
        [field: SerializeField]
        public Stat Intimidation { get; private set; }
        [field: SerializeField]
        public Stat Negotiation { get; private set; }
        [field: SerializeField]
        public Stat Seduction { get; private set; }
        [field: SerializeField]
        public Stat Barter { get; private set; }
        [field: SerializeField]
        public Stat Sneak { get; private set; }
        [field: SerializeField]
        public Stat Lockpick { get; private set; }
        [field: SerializeField]
        public Stat SlightOfHoof { get; private set; }
        [field: SerializeField]
        public Stat HackingMatrixTech { get; private set; }
        [field: SerializeField]
        public Stat Chemistry { get; private set; }
        [field: SerializeField]
        public Stat Medicine { get; private set; }
        [field: SerializeField]
        public Stat AcademicsLore { get; private set; }
        [field: SerializeField]
        public Stat RepairMechanics { get; private set; }
        [field: SerializeField]
        public Stat Gambling { get; private set; }
        [field: SerializeField]
        public Stat Athletics { get; private set; }
        [field: SerializeField]
        public Stat Profession { get; private set; }

        public void Awake()
        {
            ExpToNext = Level * (Level + 1) / 2 * 1000;

            // Set default Skill Values based on formulas
            // Need to explicitly set each one to separate new objects as we don't want skills to share Stat objects
            Unarmed = new Stat(Mathf.CeilToInt(Endurance.BaseValue + Agility.BaseValue + (Luck.BaseValue / 2)));
            Thrown = new Stat(Mathf.CeilToInt(Strength.BaseValue + Agility.BaseValue + (Luck.BaseValue / 2)));
            MagicialEnergyWeapons = new Stat(Mathf.CeilToInt((Perception.BaseValue * 2) + (Luck.BaseValue / 2)));
            Melee = new Stat(Mathf.CeilToInt(Strength.BaseValue + Agility.BaseValue + (Luck.BaseValue / 2)));
            Firearms = new Stat(Mathf.CeilToInt(Perception.BaseValue + Agility.BaseValue + (Luck.BaseValue / 2)));
            Explosives = new Stat(Mathf.CeilToInt((Perception.BaseValue * 2) + (Luck.BaseValue / 2)));
            BattleSaddles = new Stat(Mathf.CeilToInt(Endurance.BaseValue + Perception.BaseValue + (Luck.BaseValue / 2)));
            Alchemy = new Stat(Mathf.CeilToInt(Intelligence.BaseValue + Endurance.BaseValue + Perception.BaseValue
                + (Luck.BaseValue / 2)) - 5);
            Survivalism = new Stat(Mathf.CeilToInt(Intelligence.BaseValue + Endurance.BaseValue + Perception.BaseValue
                + (Luck.BaseValue / 2)) - 5);
            Traps = new Stat(Mathf.CeilToInt(Intelligence.BaseValue + Endurance.BaseValue + Perception.BaseValue
                + (Luck.BaseValue / 2)) - 5);
            Bluff = new Stat(Mathf.CeilToInt((Charisma.BaseValue * 2) + (Luck.BaseValue / 2)));
            Intimidation = new Stat(Mathf.CeilToInt((Charisma.BaseValue * 2) + (Luck.BaseValue / 2)));
            Negotiation = new Stat(Mathf.CeilToInt((Charisma.BaseValue * 2) + (Luck.BaseValue / 2)));
            Seduction = new Stat(Mathf.CeilToInt((Charisma.BaseValue * 2) + (Luck.BaseValue / 2)));
            Barter = new Stat(Mathf.CeilToInt((Charisma.BaseValue * 2) + (Luck.BaseValue / 2)));
            Sneak = new Stat(Mathf.CeilToInt((Agility.BaseValue * 2) + (Luck.BaseValue / 2)));
            Lockpick = new Stat(Mathf.CeilToInt(Charisma.BaseValue + Agility.BaseValue + (Luck.BaseValue / 2)));
            SlightOfHoof = new Stat(Mathf.CeilToInt(Charisma.BaseValue + Agility.BaseValue + (Luck.BaseValue / 2)));
            HackingMatrixTech = new Stat(Mathf.CeilToInt((Intelligence.BaseValue * 2) + (Luck.BaseValue / 2)));
            Chemistry = new Stat(Mathf.CeilToInt((Intelligence.BaseValue * 2) + (Luck.BaseValue / 2)));
            Medicine = new Stat(Mathf.CeilToInt((Intelligence.BaseValue * 2) + (Luck.BaseValue / 2)));
            AcademicsLore = new Stat(Mathf.CeilToInt((Intelligence.BaseValue * 2) + (Luck.BaseValue / 2)));
            RepairMechanics = new Stat(Mathf.CeilToInt((Intelligence.BaseValue * 2) + (Luck.BaseValue / 2)));
            Gambling = new Stat(Mathf.CeilToInt(Luck.BaseValue * 2 + 3));
            Athletics = new Stat(Mathf.CeilToInt(Strength.BaseValue + Endurance.BaseValue + Agility.BaseValue));
            Profession = new Stat(Mathf.CeilToInt((Charisma.BaseValue * 2) + (Luck.BaseValue / 2)));
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
