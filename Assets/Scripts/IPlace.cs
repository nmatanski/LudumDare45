using System.Collections.Generic;
namespace Stray
{
    public interface IPlace
    {
        string Title { get; }
        string Description { get; }
        int ActionCount { get; }
        IAction GetAction(int index);
        IAction AddAction();
        void RemoveAction(IAction action);
    }
}