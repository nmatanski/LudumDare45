namespace Stray
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class ChoiceButton : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI m_LabelField;
        [SerializeField]
        Button m_Button;
        [SerializeField]
        PlaceGui m_PlaceGui;
        public PlaceGui PlageGui
        {
            get { return m_PlaceGui; }
            set { m_PlaceGui = value; }
        }
        IAction m_Action;
        public IAction Action
        {
            get { return m_Action; }
            set
            {
                m_Action = value;
                UpdateGui();
            }
        }

        void OnValidate()
        {
            UpdateGui();
        }

        void UpdateGui()
        {
            if (m_Action == null)
            {
                m_LabelField.text = "";
            }
            else
            {
                m_LabelField.text = m_Action.Description;
                m_Button.onClick.RemoveAllListeners();
                m_Button.onClick.AddListener(() => m_PlaceGui.Execute(Action));
            }
        }
    }
}