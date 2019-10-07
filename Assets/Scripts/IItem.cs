using UnityEngine;

namespace Stray
{
    public interface IItem
    {
        string Name { get; }
        Sprite Sprite { get; }
        //int HealthAmountOnUse { get; }
        //int WarmthAmountOnEquip { get; }
        //int WarmthAmountOnDiscard { get; }
    }
}