namespace Stray
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public sealed class ChoiceButton : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI m_LabelField;
        [SerializeField]
        Button m_Button;

        public void SetData(string description, UnityAction onClick, bool interactable = true)
        {
            m_LabelField.text = description;
            m_Button.onClick.RemoveAllListeners();
            m_Button.onClick.AddListener(onClick);
            m_Button.interactable = interactable;
        }
    }
}