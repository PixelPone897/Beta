using UnityEngine;
using static Scripts.Constants;

namespace Scripts.Actors
{
    [RequireComponent(typeof(ActorSpecialStats))]
    public class ActorVitals : MonoBehaviour
    {
        private ActorSpecialStats specialStats;
        public Resource Health { get; private set; }
        public Resource ActionPoints { get; private set; }
        public Resource Rads { get; private set; }
        public RadiationStatus RadsStatus { get; private set; }

        public Stat HealingRate { get; private set; }
        
        public Stat RadiationResistance { get; private set; }

        public void Awake()
        {
            Rads = new Resource();
            RadsStatus = GetRadiationStatus();
        }

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
        }

        // Update is called once per frame
        void Update()
        {

        }
        public RadiationStatus GetRadiationStatus()
        {
            if(Rads.CurrentValue < 200)
            {
                return RadiationStatus.NONE;
            }
            else if(Rads.CurrentValue >= 200 &&  Rads.CurrentValue <= 400)
            {
                return RadiationStatus.MINOR;
            }
            else if (Rads.CurrentValue >= 400 && Rads.CurrentValue <= 600)
            {
                return RadiationStatus.ADVANCED;
            }
            else if (Rads.CurrentValue >= 600 && Rads.CurrentValue <= 800)
            {
                return RadiationStatus.CRITICAL;
            }
            else if (Rads.CurrentValue >= 800 && Rads.CurrentValue <= 1000)
            {
                return RadiationStatus.DEADLY;
            }
            else
            {
                return RadiationStatus.DEAD;
            }
        }
    }
}
