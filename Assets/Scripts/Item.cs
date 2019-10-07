using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Stray
{
    [CreateAssetMenu(fileName = "Item")]
    public class Item : ScriptableObject, IItem
    {
        //[SerializeField]
        //private int healthAmountOnUse;
        //public int HealthAmountOnUse { get { return healthAmountOnUse; } }

        //[SerializeField]
        //private int warmthAmountOnEquip;
        //public int WarmthAmountOnEquip { get { return warmthAmountOnEquip; } }

        //[SerializeField]
        //private int warmthAmountOnDiscard;
        //public int WarmthAmountOnDiscard { get { return warmthAmountOnDiscard; } }

        [SerializeField]
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [SerializeField]
        private Sprite sprite;
        public Sprite Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }
    }
}
