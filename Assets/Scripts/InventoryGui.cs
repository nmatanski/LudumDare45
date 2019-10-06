namespace Stray
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class InventoryGui : MonoBehaviour
    {
        [SerializeField]
        GameController m_Controller;
        [SerializeField]
        GameState m_State;

        [SerializeField]
        Button[] m_Buttons;

        [NonSerialized]
        Sprite m_EmptySprite;

        void Awake()
        {
            m_EmptySprite = m_Buttons[0].image.sprite;
        }
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
            for (int i = 0; i < m_State.Inventory.Capacity; i++)
            {
                var button = m_Buttons[i];
                button.onClick.RemoveAllListeners();

                var item = m_State.Inventory.GetItem(i);

                bool isEmpty = item == null;
                button.interactable = !isEmpty;
                if (isEmpty)
                {
                    button.image.sprite = m_EmptySprite;
                }
                else
                {
                    button.image.sprite = item.Sprite;
                    int iCopy = i;
                    button.onClick.AddListener(() => OnButtonClicked(iCopy));
                }
            }
        }

        void OnButtonClicked(int index)
        {
            var item = m_State.Inventory.GetItem(index);
            if (m_Controller.UseItem(item))
            {
                // Nothing to do here, the Gui should update automatically
            }
            else
            {
                Debug.LogWarning($"Couldn't use item {item.Name}");
            }
        }
    }
}