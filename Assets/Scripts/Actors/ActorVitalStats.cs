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

        [field: SerializeField]
        public BaseActorStats BaseActorStats { get; private set; }

        public void Awake()
        {
            if(BaseActorStats == null)
            {
                Debug.LogError("BaseActorStats is not assigned in ActorVitalStats. Please assign it in the inspector.");
                return;
            }

            ActorName = DUMMY_STRING; // Default name, must be set later
            Gender = BaseActorStats.Gender;
            Level = 1; // Default level, can be modified later

            // Initialize stats with default values from BaseActorStats
            Strength = new Stat(BaseActorStats.Strength);
            Perception = new Stat(BaseActorStats.Perception);
            Endurance = new Stat(BaseActorStats.Endurance);
            Charisma = new Stat(BaseActorStats.Charisma);
            Intelligence = new Stat(BaseActorStats.Intelligence);
            Agility = new Stat(BaseActorStats.Agility);
            Luck = new Stat(BaseActorStats.Luck);
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
