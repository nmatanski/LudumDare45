namespace Stray
{
    using System;
    using UnityEngine.Events;

    [Serializable]
    public sealed class ActionCallback : UnityEvent<IAction> { }
}