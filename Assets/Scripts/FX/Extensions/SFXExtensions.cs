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
        public static IEnumerator FadeAudio(this AudioSource source, float startVolume = 1, float endVolume = 0, float duration = 1)
        {
            float elapsedTime = 0;
            while (elapsedTime < duration)
            {
                source.volume = Mathf.Lerp(startVolume, endVolume, elapsedTime / duration);

                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            source.volume = endVolume;
        }
    }
}