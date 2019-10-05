namespace Stray
{
    public interface IAction
    {
        string Description { get; }
        bool IsActive { get; }
        IPlace TargetPlace { get; }
    }
}