namespace Stray
{
    public interface IAction
    {
        string Description { get; }
        bool IsActive { get; }
        IPlace TargetPlace { get; }

        IItem AddItem { get; }
        IItem UseItem { get; }
        IItem DiscardItem { get; }

        int ChangeMoney { get; }
    }
}