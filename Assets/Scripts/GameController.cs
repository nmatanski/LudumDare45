namespace Stray
{
    using UnityEngine;
    using UnityEngine.Events;

    public sealed class GameController : MonoBehaviour
    {
        [SerializeField]
        GameState m_State;

        void Awake()
        {
            ActionExecuted += AutoDestroyActionsIfNeeded;
        }

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

            var newAction = (PlaceAction)((Place)m_State.Place).AddAction();
            newAction.Description = $"Pick up {item.Name}";
            newAction.AddItem = item;
            newAction.DestroyAfterExecution = true;

            OnStateChanged();
            return true;
        }

        //public bool UseItem(IItem item)
        //{
        //    if (item == null || !m_State.Inventory.HasItem(item)) return false;
        //    UseItemInternal(item);
        //    OnStateChanged();
        //    return true;
        //}
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
                //(action.UseItem == null || m_State.Inventory.HasItem(action.UseItem)) && // If we need to use an item, do we have it?
                (action.DiscardItem == null || m_State.Inventory.HasItem(action.DiscardItem)) // If we need to remove an item, do we have it?
                ;
            return canExecute;
        }
        public void Execute(IAction action)
        {
            // All these checks are needed because an action has many optional fields
            if (action.ChangeMoney != 0) AddMoneyInternal(action.ChangeMoney);
            if (action.DiscardItem != null) DiscardItemInternal(action.DiscardItem);
            if (action.AddItem != null) AddItemInternal(action.AddItem);
            //if (action.UseItem != null) UseItemInternal(action.UseItem);
            if (action.TargetPlace != null)
            {
                MoveToInternal(action.TargetPlace);
                OnPlaceChanged(action.TargetPlace);
            }
            if (action.ChangeHealth != 0) ChangeHealthInternal(action.ChangeHealth);

            if (!string.IsNullOrEmpty(action.DescriptionAfter))
            {
                if (!m_State.Place.Description.EndsWith("\n")) m_State.Place.Description += "\n";
                m_State.Place.Description += action.DescriptionAfter;
            }

            if (!action.IsRepeatable) action.IsActive = false;
            OnExecuted(action);
            OnStateChanged();
        }

        void AddMoneyInternal(int amount)
        {
            m_State.Wallet += amount;
        }
        void AddItemInternal(IItem item)
        {
            m_State.Inventory.Add(item);
            //m_State.Cold -= item.WarmthAmountOnEquip;
        }
        void DiscardItemInternal(IItem item)
        {
            //m_State.Cold += item.WarmthAmountOnDiscard;
            m_State.Inventory.Remove(item);
        }
        //void UseItemInternal(IItem item)
        //{
        //    m_State.Health += item.HealthAmountOnUse;
        //    m_State.Cold += item.WarmthAmountOnDiscard;
        //    m_State.Inventory.Remove(item);
        //}
        void BuyItemInternal(int cost, IItem item)
        {
            AddMoneyInternal(-cost);
            AddItemInternal(item);
        }
        void ChangeHealthInternal(int change)
        {
            m_State.Health += change;
        }

        void MoveToInternal(IPlace place)
        {
            m_State.Place = place;
            m_State.Time += 3;
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

        public void AddPickItemAction(Place place, IItem item)
        {
            var newAction = place.gameObject.AddComponent<PlaceAction>();

        }

        void AutoDestroyActionsIfNeeded(IAction newAction)
        {
            var placeAction = (PlaceAction)newAction;
            if (placeAction.DestroyAfterExecution)
            {
                m_State.Place.RemoveAction(placeAction);
                OnStateChanged();
            }
        }

        [SerializeField]
        ActionCallback m_ActionExecuted;
        public event UnityAction<IAction> ActionExecuted
        {
            add { m_ActionExecuted.AddListener(value); }
            remove { m_ActionExecuted.RemoveListener(value); }
        }
        void OnExecuted(IAction action)
        {
            m_ActionExecuted.Invoke(action);
        }

        [SerializeField]
        PlaceChangedCallback m_PlaceChanged;
        public event UnityAction<IPlace> PlaceChanged
        {
            add { m_PlaceChanged.AddListener(value); }
            remove { m_PlaceChanged.RemoveListener(value); }
        }
        void OnPlaceChanged(IPlace place)
        {
            m_PlaceChanged.Invoke(place);
        }
    }
}