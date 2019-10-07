namespace Stray
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class PlaceGui : MonoBehaviour
    {
        [SerializeField]
        GameController m_Controller;
        [SerializeField]
        GameState m_State;
        [SerializeField]
        TextMeshProUGUI m_NameField;
        [SerializeField]
        TextMeshProUGUI m_DescriptionField;
        [SerializeField]
        RectTransform m_ChoicesContainer;

        [SerializeField]
        Place m_Place;

        [SerializeField]
        ChoiceButton m_ButtonPrefab;
        public ChoiceButton ButtonPrefab
        {
            get { return m_ButtonPrefab; }
            set { m_ButtonPrefab = value; }
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

        public void Execute(IAction action)
        {
            if (!m_Controller.IsActionExecutable(action))
            {
                Debug.LogWarning($"Cannot execute '{action.Description}'");
            }
            else
            {
                m_Controller.Execute(action);
            }
        }

        void UpdateGui()
        {
            var place = m_State.Place;
            DestroyAllChoices();
            if (place == null)
            {
                m_NameField.text = "";
                m_DescriptionField.text = "";
            }
            else
            {
                m_NameField.text = place.Title;
                m_DescriptionField.text = place.Description + "\n";
                // Spawn one button per action
                for (int i = 0; i < place.ActionCount; i++)
                {
                    var action = place.GetAction(i);
                    // If it's not repeatable and it was already used, don't even show
                    if (!m_Controller.IsActionValid(action)) continue;

                    var button = Instantiate(ButtonPrefab, m_ChoicesContainer);
                    button.SetData(
                        action.Description, 
                        onClick: () => Execute(action), 
                        interactable: m_Controller.IsActionExecutable(action) // If we can't afford the action, show disabled
                    );
                }
            }
        }
        void DestroyAllChoices()
        {
            for (int i = 0; i < m_ChoicesContainer.childCount; i++)
            {
                SafeDestroy(m_ChoicesContainer.GetChild(i).gameObject);
            }
        }
        void SafeDestroy(GameObject gameObject)
        {
            if (Application.isPlaying) Destroy(gameObject);
            else DestroyImmediate(gameObject);
        }
    }
}