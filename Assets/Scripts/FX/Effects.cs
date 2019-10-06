using System.Collections;
using TMPro;
using UnityEngine;

namespace Stray
{
    public static class Effects
    {
        public static IEnumerator UseItemOnActionFX(Sprite itemSprite)
        {
            var foodFX = GameObject.FindGameObjectWithTag("FoodFX");
            foodFX.GetComponent<SpriteRenderer>().sprite = itemSprite;
            foodFX.SetActive(true); //starts the animation once
            yield return new WaitForSeconds(1f);
            foodFX.SetActive(false);
        }

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
