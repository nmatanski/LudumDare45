namespace Stray
{
    using UnityEngine;

    public sealed class GameOverGui : MonoBehaviour
    {
        [SerializeField]
        GameController m_Controller;
        [SerializeField]
        GameState m_State;

        [SerializeField]
        GameObject m_Popup;

        void OnEnable()
        {
            m_Controller.StateChanged += PopupIfDead;
        }
        void OnDisable()
        {
            m_Controller.StateChanged -= PopupIfDead;
        }

        void PopupIfDead()
        {
            if (m_State.Health <= 0) m_Popup.SetActive(true);
        }
    }
}