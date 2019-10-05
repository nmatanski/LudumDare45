namespace Stray
{
    public interface IInventory
    {
        int Capacity { get; }
        int ItemCount { get; }
        bool IsFull { get; }

        IItem GetItem(int index);

        void Add(IItem item);
        void Use(IItem item);
        void Discard(IItem item);
        bool HasItem(IItem item);
    }
}