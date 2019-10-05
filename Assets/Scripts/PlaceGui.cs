namespace Stray
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class PlaceGui : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI m_NameField;
        [SerializeField]
        TextMeshProUGUI m_DescriptionField;
        [SerializeField]
        RectTransform m_ChoicesContainer;

        [SerializeField]
        Place m_Place;
        public IPlace Place
        {
            get { return m_Place; }
            set
            {
                m_Place = (Place)value;
                UpdateGui();
            }
        }

        [SerializeField]
        ChoiceButton m_ButtonPrefab;
        public ChoiceButton ButtonPrefab
        {
            get { return m_ButtonPrefab; }
            set { m_ButtonPrefab = value; }
        }


        void OnEnable()
        {
            UpdateGui();
        }
        public void Execute(IAction action)
        {
            if (!action.IsActive) return;
            if (action.TargetPlace != null)
            {
                Place = action.TargetPlace;
            }
        }

        void UpdateGui()
        {
            DestroyAllChoices();
            if (Place == null)
            {
                m_NameField.text = "";
                m_DescriptionField.text = "";
            }
            else
            {
                m_NameField.text = Place.Title;
                m_DescriptionField.text = Place.Description;
                for (int i = 0; i < Place.ActionCount; i++)
                {
                    var action = Place.GetAction(i);
                    var button = Instantiate(ButtonPrefab, m_ChoicesContainer);
                    button.PlageGui = this;
                    button.Action = action;
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