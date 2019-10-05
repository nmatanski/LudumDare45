namespace Stray
{
    using UnityEngine;

    public sealed class PlaceAction : MonoBehaviour, IAction
    {
        [SerializeField]
        string m_Description;
        public string Description
        {
            get { return m_Description; }
        }

        [SerializeField]
        bool m_IsActive = true;
        public bool IsActive
        {
            get { return m_IsActive; }
            set { m_IsActive = value; }
        }

        [SerializeField]
        Place m_TargetPlace;
        public IPlace TargetPlace
        {
            get { return m_TargetPlace; }
        }

        [SerializeField]
        Item m_AddItem;
        public IItem AddItem
        {
            get { return m_AddItem; }
        }
        [SerializeField]
        Item m_UseItem;
        public IItem UseItem
        {
            get { return m_UseItem; }
        }
        [SerializeField]
        Item m_DiscardItem;
        public IItem DiscardItem
        {
            get { return m_DiscardItem; }
        }
        [SerializeField]
        int m_ChangeMoney;
        public int ChangeMoney
        {
            get { return m_ChangeMoney; }
            set { m_ChangeMoney = value; }
        }
    }
}