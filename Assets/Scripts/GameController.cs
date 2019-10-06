﻿namespace Stray
{
    using UnityEngine;
    using UnityEngine.Events;

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
            AddMoneyInternal(amount);
            OnStateChanged();
            return true;
        }

        public bool AddItem(IItem item)
        {
            if (item == null || m_State.Inventory.IsFull) return false;
            AddItemInternal(item);
            OnStateChanged();
            return true;
        }

        public bool DiscardItem(IItem item)
        {
            if (item == null || !m_State.Inventory.HasItem(item)) return false;
            DiscardItemInternal(item);
            OnStateChanged();
            return true;
        }

        public bool UseItem(IItem item)
        {
            if (item == null || !m_State.Inventory.HasItem(item)) return false;
            UseItemInternal(item);
            OnStateChanged();
            return true;
        }
        public bool BuyItem(int cost, IItem item)
        {
            if (item == null || m_State.Inventory.IsFull) return false;
            if (m_State.Wallet < cost) return false;

            AddMoneyInternal(-cost);
            AddItem(item);

            OnStateChanged();
            return true;
        }
        public bool MoveTo(IPlace place)
        {
            if (place == null) return false;
            MoveToInternal(place);
            OnStateChanged();
            return true;
        }

        public bool IsActionValid(IAction action)
        {
            return (action.IsRepeatable || action.IsActive); // If we already executed this action, can we do it again?
        }
        public bool IsActionExecutable(IAction action)
        {
            bool canExecute =
                (action.ChangeMoney > 0 || m_State.Wallet >= -action.ChangeMoney) && // If we need to remove money, do we have enough?
                (action.AddItem == null || !m_State.Inventory.IsFull) && // If we need to add an item, can we?
                (action.UseItem == null || m_State.Inventory.HasItem(action.UseItem)) && // If we need to use an item, do we have it?
                (action.DiscardItem == null || m_State.Inventory.HasItem(action.DiscardItem)) // If we need to remove an item, do we have it?
                ;
            return canExecute;
        }
        public void Execute(IAction action)
        {
            // All these checks are needed because an action has many optional fields
            if (action.ChangeMoney != 0) AddMoneyInternal(action.ChangeMoney);
            if(action.AddItem != null) AddItemInternal(action.AddItem);
            if (action.UseItem != null) UseItemInternal(action.UseItem);
            if (action.DiscardItem != null) DiscardItemInternal(action.DiscardItem);
            if (action.TargetPlace != null) MoveToInternal(action.TargetPlace);

            if (!action.IsRepeatable) action.IsActive = false;
            OnStateChanged();
        }

        void AddMoneyInternal(int amount)
        {
            m_State.Wallet += amount;
        }
        void AddItemInternal(IItem item)
        {
            m_State.Inventory.Add(item);
            m_State.Cold -= item.WarmthAmountOnEquip;
        }
        void DiscardItemInternal(IItem item)
        {
            m_State.Cold += item.WarmthAmountOnDiscard;
            m_State.Inventory.Remove(item);
        }
        void UseItemInternal(IItem item)
        {
            m_State.Health += item.HealthAmountOnUse;
            m_State.Cold += item.WarmthAmountOnDiscard;
            m_State.Inventory.Remove(item);
        }
        void BuyItemInternal(int cost, IItem item)
        {
            AddMoneyInternal(-cost);
            AddItemInternal(item);
        }

        void MoveToInternal(IPlace place)
        {
            m_State.Place = place;
        }

        [SerializeField]
        UnityEvent m_StateChanged;
        public event UnityAction StateChanged
        {
            add { m_StateChanged.AddListener(value); }
            remove { m_StateChanged.RemoveListener(value); }
        }
        void OnStateChanged()
        {
            m_StateChanged.Invoke();
        }
    }
}