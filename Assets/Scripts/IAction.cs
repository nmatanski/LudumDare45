using UnityEngine.Events;

namespace Stray
{
    public interface IAction
    {
        string Description { get; }
        bool IsActive { get; set; }
        bool IsRepeatable { get; }
        bool DestroyAfterExecution { get; set;  }
        IPlace TargetPlace { get; }

        IItem AddItem { get; }
        //IItem UseItem { get; }
        IItem DiscardItem { get; }

        int ChangeHealth { get; }
        int ChangeMoney { get; }
    }
}