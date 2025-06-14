using UnityEngine;
using static Scripts.Constants;

namespace Scripts.Actors
{
    /// <summary>
    /// Stores an Actor's general information and Base SPECIAL values.
    /// </summary>
    [CreateAssetMenu(fileName = "BaseActorStats", menuName = "Actors/BaseActorStats", order = 1)]
    public class BaseActorStats : ScriptableObject
    {
        [field: SerializeField, TextArea, Header("General Info")] 
        public string ActorDescription { get; private set; } = DUMMY_STRING;
        [SerializeField]
        public ActorGender Gender { get; private set; } = ActorGender.MALE;

        [field: SerializeField]
        public ActorRace Race { get; private set; } = ActorRace.EARTH;

        //Special Stats
        [field: SerializeField, Header("SPECIAL Values")]
        public int Strength { get; private set; } = 5;
        [field: SerializeField]
        public int Perception { get; private set; } = 5;
        [field: SerializeField]
        public int Endurance { get; private set; } = 5;
        [field: SerializeField]
        public int Charisma { get; private set; } = 5;
        [field: SerializeField]
        public int Intelligence { get; private set; } = 5;
        [field: SerializeField]
        public int Agility { get; private set; } = 5;
        [field: SerializeField]
        public int Luck { get; private set; } = 5;

        public void Reset()
        {
            Strength = 5;
            Perception = 5;
            Endurance = 5;
            Charisma = 5;
            Intelligence = 5;
            Agility = 5;
            Luck = 5;

            //Debug.Log("On BaseActorStats Reset!");
            PrintSpecialValues();
        }

        private void PrintSpecialValues()
        {
            Debug.Log($"Strength {Strength}");
            Debug.Log($"Perception {Perception}");
            Debug.Log($"Endurance {Endurance}");
            Debug.Log($"Charisma {Charisma}");
            Debug.Log($"Intelligence {Intelligence}");
            Debug.Log($"Agility {Agility}");
            Debug.Log($"Luck {Luck}");
        }
    }
}