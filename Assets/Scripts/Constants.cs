using System;

namespace Scripts
{
    public static class Constants
    {
        public const string DUMMY_STRING = "DUMMY_STRING";
        public const int RESOURCE_MIN_VALUE = -99999;
        public const int RESOURCE_MAX_VALUE = 99999;

        public enum ActorGender
        {
            MALE,
            FEMALE
        }

        public enum ActorRace
        {
            EARTH,
            PEGASUS,
            UNICORN
        }

        public enum RadiationStatus
        {
            NONE,
            MINOR,
            ADVANCED,
            CRITICAL,
            DEADLY,
            DEAD
        }
    }
}