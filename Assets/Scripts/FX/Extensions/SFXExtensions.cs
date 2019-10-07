using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Stray
{
    public static class SFXExtensions
    {
        public static IEnumerator FadeOutAudio(this AudioSource source, float duration)
        {
            float defaultVolume = source.volume;

            while (source.volume > 0)
            {
                source.volume -= defaultVolume * Time.deltaTime / duration;
                yield return new WaitForEndOfFrame();
            }

            source.Stop();
            source.volume = defaultVolume;
        }
    }
}