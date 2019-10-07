namespace Stray
{
    using System.Collections.Generic;
    using UnityEngine;

    public sealed class Place : MonoBehaviour, IPlace
    {
        [SerializeField]
        string m_Title;
        public string Title
        {
            get { return m_Title; }
            set { m_Title = value; }
        }

        [TextArea]
        [SerializeField]
        string m_Description;
        public string Description
        {
            get { return m_Description; }
            set { m_Description = value; }
        }

        [SerializeField, HideInInspector]
        List<PlaceAction> m_Actions;

        void OnValidate()
        {
            GetComponents(m_Actions);
        }

        public int ActionCount
        {
            get { return m_Actions.Count; }
        }
        public IAction GetAction(int index)
        {
            return m_Actions[index];
        }

        public IAction AddAction()
        {
            var newAction = gameObject.AddComponent<PlaceAction>();
            m_Actions.Add(newAction);
            return newAction;
        }
        public void RemoveAction(IAction action)
        {
            var placeAction = (PlaceAction)action;
            m_Actions.Remove(placeAction);
            Destroy(placeAction);
        }
    }
}