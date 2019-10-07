namespace Stray
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public sealed class BgmSwitcher : MonoBehaviour
    {
        [SerializeField]
        GameController m_Controller;
        [SerializeField]
        GameState m_State;

        [SerializeField]
        AudioSource[] m_Sources;

        [SerializeField]
        List<Entry> m_Entries;

        [NonSerialized]
        int m_SourceIndex;
        int ActiveIndex
        {
            get { return m_SourceIndex; }
        }
        int InactiveIndex
        {
            get { return 1 - m_SourceIndex; }
        }
        void SwapSources()
        {
            m_SourceIndex = InactiveIndex;
        }
        AudioSource ActiveSource
        {
            get { return m_Sources[ActiveIndex]; }
        }
        AudioSource InactiveSource
        {
            get { return m_Sources[InactiveIndex]; }
        }
        void Awake()
        {
            m_SourceIndex = 0;
        }
        void OnEnable()
        {
            m_Controller.PlaceChanged += ChangeBgm;
            ChangeBgm(m_State.Place);
        }
        void OnDisable()
        {
            m_Controller.PlaceChanged += ChangeBgm;
        }

        void ChangeBgm(IPlace place)
        {
            var entry = m_Entries.Find(e => e.PlaceName == place.Title);
            StartCoroutine(FadeAndSwitchSources(entry.Clip));
        }

        IEnumerator FadeAndSwitchSources(AudioClip clip)
        {
            InactiveSource.clip = clip;
            InactiveSource.Play();
            yield return InactiveSource.FadeAudio(0, 1, 1);
            yield return ActiveSource.FadeAudio(1, 0, 1);
            SwapSources();
        }

        [Serializable]
        public struct Entry
        {
            [SerializeField]
            public string PlaceName;
            [SerializeField]
            public AudioClip Clip;
        }
    }
}