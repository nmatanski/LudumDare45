using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Stray
{
    public static class ExtensionHelpers
    {
        public static void FadeOutText(TextMeshProUGUI textbox)
        {
            var animator = textbox.GetComponent<Animator>();
            animator.SetTrigger("FadeOut");
        }

        public static void FadeInText(TextMeshProUGUI textbox)
        {
            var animator = textbox.GetComponent<Animator>();
            animator.SetTrigger("FadeIn");
        }
    }
}
