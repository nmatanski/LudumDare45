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
    }
}