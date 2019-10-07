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
            set { m_Description = value; }
        }
        [SerializeField]
        string m_DescriptionAfter;
        public string DescriptionAfter
        {
            get { return m_DescriptionAfter; }
        }

        [SerializeField]
        bool m_IsActive = true;
        public bool IsActive
        {
            get { return m_IsActive; }
            set { m_IsActive = value; }
        }
        [SerializeField]
        bool m_IsRepeatable = true;
        public bool IsRepeatable
        {
            get { return m_IsRepeatable; }
            set { m_IsRepeatable = value; }
        }
        [SerializeField]
        bool m_DestroyAfterExecution = false;
        public bool DestroyAfterExecution
        {
            get { return m_DestroyAfterExecution; }
            set { m_DestroyAfterExecution = value; }
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
            set { m_AddItem = (Item)value; }
        }
        //[SerializeField]
        //Item m_UseItem;
        //public IItem UseItem
        //{
        //    get { return m_UseItem; }
        //}
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