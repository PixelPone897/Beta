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

        public void Awake()
        {
            ExpToNext = Level * (Level + 1) / 2 * 1000;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Debug.Log("Strength" + Strength.GetPermanentValue());
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
