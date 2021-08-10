using UnityEngine;
using System;

namespace Effects
{
    public interface IVisualEffect
    {
        event Action onCompleate;
        void Execute(RendererGetter rendererGetter, AudioSource audio);
        bool dontDisturb { get; }
    }
}