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

        public enum LimbName
        {
            HEAD,
            TORSO,
            LEFT_FORELEG,
            RIGHT_FORELEG,
            LEFT_HINDLEG,
            RIGHT_HINDLEG,
            HORN, // Should be included in separate ActorMagic Component
            LEFT_WING, // Should be included in separate ActorFlight Component
            RIGHT_WING,
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

        public enum SpecialName
        {
            STRENGTH,
            PERCEPTION,
            ENDURANCE,
            CHARISMA,
            INTELLIGENCE,
            AGILITY,
            LUCK
        }

        public enum RollModifier
        {
            VERY_EASY = 3,
            EASY = 2,
            ORDINARY = 1,
            DIFFICULT = -1,
            HARD = -2,
            VERY_HARD = -3
        }

        public enum SkillName
        {
            UNARMED,
            THROWN,
            MEW,
            MELEE,
            FIREARMS,
            EXPLOSIVES,
            BATTLE_SADDLES,
            ALCH_SUR_TRAPS,
            BLUFF_INTIMID,
            NEGOT_SEDUCT,
            BARTER,
            SNEAK,
            LOCKPICK,
            SLIGHT_OF_HOOF,
            HACKING_TECH,
            CHEMISTRY,
            MEDICINE,
            ACADEMICS_LORE,
            REPAIR_MECH,
            GAMBLING,
            ATHLETICS,
            PROFESSION
        }

        public static int GetSpecialRollModifier(RollModifier modifier)
        {
            return (int)modifier;
        }

        public static float GetSkillRollModifier(RollModifier modifier)
        {
            return (float)((int)modifier * 0.1f);
        }
    }
}