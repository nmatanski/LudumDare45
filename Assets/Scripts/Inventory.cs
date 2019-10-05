using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Stray
{
    [Serializable]
    public class Inventory : IInventory
    {
        public int Capacity { get { return inventory.Length; } }

        public int ItemCount { get { return inventory.Count(item => item != null); } }

        public bool IsFull { get { return ItemCount == Capacity; } }

        [SerializeField]
        private IItem[] inventory;


        public void Add(IItem item)
        {
            if (IsFull)
            {
                return;
            }

            int index = Array.FindIndex(inventory, currentItem => currentItem == null || ItemCount == 0);
            inventory[index] = item ?? throw new NullReferenceException();
        }

        public void Discard(IItem item)
        {
            if (item == null)
            {
                throw new NullReferenceException();
            }

            inventory[Array.IndexOf(inventory, item)] = null;
        }

        public IItem GetItem(int index)
        {
            return inventory[index];
        }

        public void Use(IItem item)
        {
            throw new NotImplementedException();
        }
    }
}
