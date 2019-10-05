namespace Stray
{
    using System.Collections.Generic;
    using UnityEngine;

    public sealed class Place : MonoBehaviour
    {
        [SerializeField]
        string m_Name;
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        [SerializeField]
        string m_Description;
        public string Description
        {
            get { return m_Description; }
            set { m_Description = value; }
        }

        [SerializeField]
        List<PlaceAction> m_Actions;
        public List<PlaceAction> Actions
        {
            get { return m_Actions; }
            set { m_Actions = value; }
        }
    }

}