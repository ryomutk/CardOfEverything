using UnityEngine;
using System.Collections.Generic;
using Utility;
using Effects;

namespace Trigger
{
    [RequireComponent(typeof(AudioSource), typeof(RendererGetter))]
    public class VisualEffectQueue : Singleton<VisualEffectQueue>
    {
        static List<IVisualEffect> queue;
        RendererGetter getterMan;
        AudioSource audioSource;

        void Start()
        {
            getterMan = GetComponent<RendererGetter>();
            audioSource = GetComponent<AudioSource>();
        }

        public static void RegisterFX(IVisualEffect effect)
        {
            queue.Add(effect);
        }



    }
}