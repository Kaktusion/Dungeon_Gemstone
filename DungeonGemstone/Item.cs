using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonGemstone
{
    class Item
    {
        public string Itemname { get; set; }
        public string slot { get; set; }
        public string summary { get; set; }
        public int physicalDamage { get; set; }
        public int Protection { get; set; }
        public int criticalChance { get; set; }
        public float criticalModifier { get; set; }
        public int priceAtShop { get; set; }
        public int moreLives { get; set; }
        public Item(string name, string slot, string summary, int pdmg, int pprot, int critChance, float critModifier, int price)
        {
            this.Itemname = name;
            this.slot = slot;
            this.summary = summary;
            this.physicalDamage = pdmg;
            this.Protection = pprot;
            this.criticalChance = critChance;
            this.criticalModifier = critModifier;
            this.priceAtShop = price;
        }
        public Item(string name, string slot, string summary, int pdmg, int pprot, int critChance, float critModifier, int addonationallives,int price)
        {
            this.Itemname = name;
            this.slot = slot;
            this.summary = summary;
            this.physicalDamage = pdmg;
            this.Protection = pprot;
            this.criticalChance = critChance;
            this.criticalModifier = critModifier;
            this.moreLives = addonationallives;
            this.priceAtShop = price;
        }
        public Item() { }
        //Metody

    }
}
