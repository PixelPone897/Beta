using Assets.Scripts.Items;
using Scripts.Actors;
using UnityEngine;

namespace Assets.Scripts.Actors
{
    [RequireComponent(typeof(ActorVitals))]
    [RequireComponent(typeof(ActorInventory))]
    public class ActorBattle : MonoBehaviour
    {
        public ItemInstance PrimarySlot { get; private set; }
        public ItemInstance SecondarySlot { get; private set; }

        public void Equip(ItemInstance item, bool toPrimary)
        {
            if (toPrimary)
                PrimarySlot = item;
            else
                SecondarySlot = item;
        }

        public void Unequip(bool fromPrimary)
        {
            if (fromPrimary)
                PrimarySlot = null;
            else
                SecondarySlot = null;
        }
    }
}
