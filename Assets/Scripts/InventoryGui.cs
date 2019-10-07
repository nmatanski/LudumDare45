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
        Image[] m_Images;
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
                var image = m_Images[i];
                //button.onClick.RemoveAllListeners();

                var item = m_State.Inventory.GetItem(i);

                bool isEmpty = item == null;
                //button.interactable = !isEmpty;
                if (isEmpty)
                {
                    image.sprite = null;
                    image.color = new Color(1, 1, 1, 0);
                }
                else
                {
                    image.sprite = item.Sprite;
                    image.color = new Color(1, 1, 1, 1);
                    int iCopy = i;
                    //button.onClick.AddListener(() => OnButtonClicked(iCopy));
                }
            }
        }

        void OnButtonClicked(int index)
        {
            var item = m_State.Inventory.GetItem(index);
            if (m_Controller.DiscardItem(item))
            {
                // Nothing to do here, the Gui should update automatically
            }
            else
            {
                Debug.LogWarning($"Couldn't discard item {item.Name}");
            }
        }
    }
}