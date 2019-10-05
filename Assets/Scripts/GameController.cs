namespace Stray
{
    using UnityEngine;

    public sealed class GameController : MonoBehaviour
    {
        [SerializeField]
        GameState m_State;

        /// <summary>
        /// If amount is negative, it removes money
        /// </summary>
        public bool AddMoney(int amount)
        {
            // Not enough money
            if (amount < 0 && m_State.Wallet < -amount) return false; 
            m_State.Wallet += amount;
            return true;
        }
        public bool AddItem(IItem item)
        {
            if (item == null || m_State.Inventory.IsFull) return false;
            m_State.Inventory.Add(item);
            m_State.Cold -= item.WarmthAmountOnEquip;
            return true;
        }
        public bool DiscardItem(IItem item)
        {
            if (item == null || !m_State.Inventory.HasItem(item)) return false;

            m_State.Cold += item.WarmthAmountOnDiscard;
            m_State.Inventory.Remove(item);
            return true;
        }
        public bool UseItem(IItem item)
        {
            bool hasItem = DiscardItem(item);
            if(hasItem) m_State.Health += item.HealthAmountOnUse;
            return hasItem;
        }
        public bool BuyItem(int cost, IItem item)
        {
            if (item == null || m_State.Inventory.IsFull) return false;
            AddMoney(-cost);
            AddItem(item);
            return true;
        }
        public bool MoveTo(IPlace place)
        {
            if (place == null) return false;
            m_State.Place = place;
            return true;
        }
        public bool IsActionValid(IAction action)
        {
            bool canExecute =
                action.IsActive &&
                (action.ChangeMoney > 0 || m_State.Wallet >= -action.ChangeMoney) && // Adding money or removing less than we got
                (action.AddItem == null || !m_State.Inventory.IsFull) && // If we need to add an item, can we?
                (action.UseItem == null || m_State.Inventory.HasItem(action.UseItem)) && // If we need to use an item, do we have it?
                (action.DiscardItem == null || m_State.Inventory.HasItem(action.DiscardItem)) // If we need to remove an item, do we have it?
                ;
            return canExecute;
        }
        public void Execute(IAction action)
        {
            AddMoney(action.ChangeMoney);
            AddItem(action.AddItem);
            UseItem(action.UseItem);
            DiscardItem(action.DiscardItem);
            MoveTo(action.TargetPlace);
        }
    }
}