using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Stray
{
    public static class TextMeshProExtensions
    {
        public static IEnumerator DisplayTextWithDelay(this TextMeshProUGUI textbox, float delay)
        {
            string text = textbox.text;
            textbox.text = "";
            foreach (var character in text)
            {
                textbox.text += character;
                yield return new WaitForSeconds(delay);
            }
        }

        public static IEnumerator ChangeTextWithFadeOut(this TextMeshProUGUI textbox, string text)
        {
            Effects.FadeOutText(textbox);
            yield return new WaitForSeconds(textbox.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length); //prevents changing the text while the animation is playing
            textbox.text = text;
            Effects.FadeInText(textbox);
        }
    }
}
