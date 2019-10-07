namespace Stray
{
    using UnityEngine;

    public sealed class SfxOnInventoryAltered : MonoBehaviour
    {
        [SerializeField]
        GameController m_Controller;

        [SerializeField]
        AudioClip m_Sfx;
        [SerializeField]
        AudioSource m_Source;

        void OnEnable()
        {
            m_Controller.InventoryAltered += PlaySfx;
        }
        void OnDisable()
        {
            m_Controller.InventoryAltered -= PlaySfx;
        }

        void PlaySfx()
        {
            m_Source.PlayOneShot(m_Sfx);
        }
    }
}