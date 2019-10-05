using System.Collections.Generic;
namespace Stray
{
    public interface IPlace
    {
        string Name { get; }
        string Description { get; }
        List<IAction> Actions { get; }
    } 
}