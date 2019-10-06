namespace Stray
{
    using TMPro;
    using UnityEngine;

    public sealed class StatsGui : MonoBehaviour
    {
        [SerializeField]
        GameController m_Controller;
        [SerializeField]
        GameState m_State;

        [SerializeField]
        TextMeshProUGUI m_HpField;
        [SerializeField]
        TextMeshProUGUI m_ColdField;
        [SerializeField]
        TextMeshProUGUI m_MoneyField;

        void OnEnable()
        {
            m_Controller.StateChanged += UpdateGui;
            UpdateGui();
        }
        void OnDisable()
        {
            m_Controller.StateChanged -= UpdateGui;
        }

        void UpdateGui()
        {
            m_HpField.text = m_State.Health.ToString();
            m_ColdField.text = m_State.Cold.ToString();
            m_MoneyField.text = m_State.Wallet.ToString();
        }
    }
}