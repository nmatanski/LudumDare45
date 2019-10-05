namespace Stray
{
    using UnityEngine;

    public sealed class PlaceAction : MonoBehaviour
    {
        [SerializeField]
        bool m_OneTime = true;
        public bool OneTime
        {
            get { return m_OneTime; }
            set { m_OneTime = value; }
        }

    }

}