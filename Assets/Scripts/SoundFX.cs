using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Stray
{
    public class SoundFX : MonoBehaviour
    {
        [SerializeField]
        private SoundFX nextSoundFX;


        public void PlaySound(AudioClip clip)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
            //Destroy(gameObject);
        }
    }
}
