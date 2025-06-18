using UnityEngine;

namespace Scripts.Actors
{
    [RequireComponent(typeof(ActorSpecialStats))]
    public class ActorVitals : MonoBehaviour
    {
        private ActorSpecialStats specialStats;
        public Resource Health { get; private set; }
        public Resource ActionPoints { get; private set; }
        public Resource Rads { get; private set; }

        public Stat HealingRate { get; private set; }
        
        public Stat RadiationResistance {  get; private set; }
        

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            specialStats = GetComponent<ActorSpecialStats>();

            int value = (int)(100 + (specialStats.Endurance.BaseValue * 3) +
                (specialStats.Endurance.BaseValue * (specialStats.Level - 1)));
            Health = new Resource(value);
            value = 1 + Mathf.CeilToInt(specialStats.Endurance.BaseValue / 3);
            HealingRate = new Stat(value);
            value = 45 + 5 * (int)specialStats.Endurance.BaseValue;
            ActionPoints = new Resource(value, 0, value);
            RadiationResistance = new Stat(specialStats.Endurance.BaseValue * 2);
            Rads = new Resource();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
