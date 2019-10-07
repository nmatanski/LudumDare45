namespace Stray
{
    using System;
    using UnityEngine.Events;

    [Serializable]
    public sealed class PlaceChangedCallback : UnityEvent<IPlace> { }
}