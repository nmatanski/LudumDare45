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
    }
}
