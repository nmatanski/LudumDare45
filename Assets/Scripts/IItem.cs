using UnityEngine;

namespace Stray
{
    public interface IItem
    {
        string Name { get; }

        int HealthAmountOnUse { get; }

        int WarmthAmountOnEquip { get; }

        int WarmthAmountOnDiscard { get; }

        Sprite Sprite { get; }
    }
}