using System;
using System.Collections.Generic;
using System.Text;


namespace DungeonGemstone
{
    class Shop : Item
    {
        public Item[] ItemPool = {
            new Item("długi miecz", "weapon", "Miecz którego klinga jest tak długa jak wysoki jest \"mały biedny kacper\"",13,0,0,0,189),
            new Item("Kubiego miecz lakoniczny", "weapon", "Doskonały sztylet zrobiony ze srebra, kródki, ale poręczny",4,3,20,1.5f,220),
            new Item("Futrzane butki", "boost", "Butki ciepłe stworzone z miękkiego futerka, Ukochane buty Pawła",0,21,0,0,170),
            new Item("pierścień ozdrowieńca", "Amulet","Oddanie bogu to ostatni ratunek, ozdrowiciel przybędzie i ciebie wyleczy...",0,0,0,0,450)//+1 lives left
        };
    }
}
